using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ToolWorkshop.Data;
using ToolWorkshop.Data.Entities;
using ToolWorkshop.Helpers;
using ToolWorkshop.Models;

namespace ToolWorkshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;


        public HomeController(ILogger<HomeController> logger, DataContext context, IUserHelper userHelper)
        {
            _logger = logger;
            _context = context;
            _userHelper = userHelper;
        }


        public async Task<IActionResult> Index()
        {
            List<Tool> tools = await _context.Tools
                .Include(p => p.ToolImages)
                .Include(p => p.ToolCategories)
                .OrderBy(p => p.Description)
                .ToListAsync();

            HomeViewModel model = new() { Tools = tools };
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user != null)
            {
                model.Quantity = await _context.Temporal_Movements
                    .Where(ts => ts.User.Id == user.Id)
                    .SumAsync(ts => ts.Quantity);
            }

            return View(model);
        }
        public async Task<IActionResult> Add(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            Tool tool = await _context.Tools.FindAsync(id);
            if (tool == null)
            {
                return NotFound();
            }

            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            Temporal_Movement temporal_Movement = new()
            {
                Tool = tool,
                Quantity = 1,
                User = user
            };

            _context.Temporal_Movements.Add(temporal_Movement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tool tool = await _context.Tools
                .Include(p => p.ToolImages)
                .Include(p => p.ToolCategories)
                .ThenInclude(pc => pc.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (tool == null)
            {
                return NotFound();
            }

            string categories = string.Empty;
            foreach (ToolCategory? category in tool.ToolCategories)
            {
                categories += $"{category.Category.Name}, ";
            }
            categories = categories.Substring(0, categories.Length - 2);

            AddToolToCartViewModel model = new()
            {
                Categories = categories,
                Description = tool.Description,
                Id = tool.Id,
                Name = tool.Name,
                EAN = tool.EAN,
                ToolImages = tool.ToolImages,
                Quantity = 1,
                Stock = tool.Stock,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(AddToolToCartViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            Tool tool = await _context.Tools.FindAsync(model.Id);
            if (tool == null)
            {
                return NotFound();
            }

            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            Temporal_Movement temporalSale = new()
            { // TODO: add start and finish TIME
                Tool = tool,
                Quantity = model.Quantity,
                Remarks = model.Remarks,
                User = user
            };

            _context.Temporal_Movements.Add(temporalSale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("error/404")]
        public IActionResult Error404()
        {
            return View();
        }

    }
}