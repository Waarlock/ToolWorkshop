using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Diagnostics;
using ToolWorkshop.Data;
using ToolWorkshop.Data.Entities;
using ToolWorkshop.Helpers;
using ToolWorkshop.Models;
using Vereyon.Web;

namespace ToolWorkshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IFlashMessage _flashMessage;

        public HomeController(ILogger<HomeController> logger, DataContext context, IUserHelper userHelper, IFlashMessage flashMessage)
        {
            _logger = logger;
            _context = context;
            _userHelper = userHelper;
            _flashMessage = flashMessage;
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
                var movement_Details = _context.Temporal_Movements
                    .Include(tm => tm.Details)
                    .Where(tm => tm.User.Id == user.Id)
                    .SelectMany(tm => tm.Details);

                model.Quantity = movement_Details.Count();
            };

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

            Temporal_Movement temporal_Movement = await _context.Temporal_Movements
                .Include(tm => tm.Details)
                .ThenInclude(d => d.Catalog)
                .Where(tm => tm.User.Id == user.Id)
                .Where(tm => tm.Status == Enums.MovementStatus.OPENED)
                .FirstOrDefaultAsync();

            if(null == temporal_Movement)
            {
                temporal_Movement = new()
                {
                    User = user,
                    Details = new List<Movement_Detail>(){}
                };
            }

            IEnumerable<Catalog> temporalCatalog =  temporal_Movement.Details.Select(dt => dt.Catalog);
            float availableTools = 0;

            try
            {
                availableTools = _context.Catalogs.Count(c => c.Tool.Id == id && c.Status == Enums.CatalogStatus.AVAILABLE);
            }
            catch (NullReferenceException e)
            {
                availableTools = 0;
            }
           
            float requiredTools = 0;
            try
            {
                requiredTools = temporalCatalog != null
                    ? 1F
                    : temporalCatalog.Count(c => c.Tool.Id == id) + 1F;
            }
            catch (NullReferenceException e)
            {
                requiredTools = 1F;
            }


            if (availableTools >= requiredTools)
            {
                Catalog currentCatalog = await _context.Catalogs.FirstOrDefaultAsync(c => c.Tool.Id == (int)id && c.Status == Enums.CatalogStatus.AVAILABLE);
                currentCatalog.Status = Enums.CatalogStatus.PICKED;

                temporal_Movement.Details.Add(
                new Movement_Detail()
                {
                    Catalog = currentCatalog,
                    Remarks = ""
                });
            }

            if (temporal_Movement.Id == 0)
            {
                _context.Temporal_Movements.Add(temporal_Movement);
            }
            else
            {
                _context.Temporal_Movements.Update(temporal_Movement);
            }

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

            Temporal_Movement temporal_Movement = await _context.Temporal_Movements
                .Include(tm => tm.Details)
                .ThenInclude(d => d.Catalog)
                .Where(tm => tm.User.Id == user.Id)
                .Where(tm => tm.Status == Enums.MovementStatus.OPENED)
                .FirstOrDefaultAsync();
            
            if (null == temporal_Movement)
            {
                temporal_Movement = new()
                {
                    User = user,
                    Details = new List<Movement_Detail>() { }
                };
            }
            
            IEnumerable<Catalog> temporalCatalog = temporal_Movement.Details.Select(dt => dt.Catalog);
            float availableTools = 0;

            try
            {
                availableTools = _context.Catalogs.Count(c => c.Tool.Id == tool.Id && c.Status == Enums.CatalogStatus.AVAILABLE);
            }
            catch (NullReferenceException e)
            {
                availableTools = 0;
            }

            float requiredTools = 0;
            try
            {
                requiredTools = temporalCatalog != null
                    ? model.Quantity
                    : temporalCatalog.Count(c => c.Tool.Id == tool.Id) + model.Quantity;
            }
            catch (NullReferenceException e)
            {
                requiredTools = model.Quantity;
            }

            if (availableTools >= requiredTools)
            {
                for (int i = 1; i <= model.Quantity; i++)
                {
                    Catalog currentCatalog = await _context.Catalogs.FirstOrDefaultAsync(c => c.Tool.Id == tool.Id && c.Status == Enums.CatalogStatus.AVAILABLE);
                    currentCatalog.Status = Enums.CatalogStatus.PICKED;

                    temporal_Movement.Details.Add(new Movement_Detail()
                    {
                        Catalog = await _context.Catalogs.FirstOrDefaultAsync(c => c.Tool.Id == tool.Id && c.Status == Enums.CatalogStatus.AVAILABLE),
                        Remarks = model.Remarks == null ? "" : model.Remarks
                    });
                }
            }

            if (temporal_Movement.Id == 0)
            {
                _context.Temporal_Movements.Add(temporal_Movement);
            }
            else
            {
                _context.Temporal_Movements.Update(temporal_Movement);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [Authorize]
        public async Task<IActionResult> ShowCart()
        {
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            List<Temporal_Movement>? temporal_Movements = await _context.Temporal_Movements
                .Include(tm=> tm.Details)
                .ThenInclude(d=> d.Catalog)
                .ThenInclude(c=> c.Tool)
                .ThenInclude(p => p.ToolImages)
                .Where(ts => ts.User.Id == user.Id)
                .ToListAsync();

            var details = temporal_Movements.SelectMany(tm => tm.Details);

            IEnumerable<IGrouping<Tool, Movement_Detail>> detailsGrouped = details.GroupBy(d => d.Catalog.Tool);

            ShowCartViewModel model = new()
            {
                User = user,
                GeneralQuantity = details.Count(),
                Movement_Details = details,
                Movement_Details_Grouped = detailsGrouped
            };

            return View(model);
        }
        public IActionResult Privacy()
        {
            return View();
        }


        public async Task<IActionResult> DecreaseQuantity(int? id)
        {
            if (id == null)
            {
                return NotFound();
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

            Temporal_Movement temporal_Movement = await _context.Temporal_Movements
                .Include(tm => tm.Details)
                .ThenInclude(d => d.Catalog)
                .Where(tm => tm.User.Id == user.Id)
                .Where(tm => tm.Status == Enums.MovementStatus.OPENED)
                .FirstOrDefaultAsync();

            if (null == temporal_Movement)
            {
                return NotFound();
            }

            IEnumerable<Movement_Detail> selectedDetails = temporal_Movement.Details;
            IEnumerable<Catalog> selectedToolUnits = selectedDetails.Select(dt => dt.Catalog).Where(c=> c.ToolId == id);

            if (selectedToolUnits != null &&  selectedToolUnits.Any() && selectedToolUnits.Count() > 1)
            {
                Catalog currentCatalog = selectedToolUnits.FirstOrDefault();
                temporal_Movement.Details.Remove(selectedDetails.FirstOrDefault(d=> d.Catalog == currentCatalog));
                _context.Temporal_Movements.Update(temporal_Movement);

                currentCatalog.Status = Enums.CatalogStatus.AVAILABLE;
                _context.Catalogs.Update(currentCatalog);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(ShowCart));
        }

        public async Task<IActionResult> IncreaseQuantity(int? id)
        {
            if (id == null)
            {
                return NotFound();
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

            Temporal_Movement temporal_Movement = await _context.Temporal_Movements
                .Include(tm => tm.Details)
                .ThenInclude(d => d.Catalog)
                .Where(tm => tm.User.Id == user.Id)
                .Where(tm => tm.Status == Enums.MovementStatus.OPENED)
                .FirstOrDefaultAsync();

            if (null == temporal_Movement)
            {
                return NotFound();
            }

            float availableTools = 0;
            try
            {
                availableTools = _context.Catalogs.Count(c => c.Tool.Id == id && c.Status == Enums.CatalogStatus.AVAILABLE);
            }
            catch (NullReferenceException e)
            {
                availableTools = 0;
            }

            float requiredTools = 1F;

            if (availableTools >= requiredTools)
            {
                Catalog currentCatalog = await _context.Catalogs.FirstOrDefaultAsync(c => c.Tool.Id == (int)id && c.Status == Enums.CatalogStatus.AVAILABLE);
                currentCatalog.Status = Enums.CatalogStatus.PICKED;

                temporal_Movement.Details.Add(
                new Movement_Detail()
                {
                    Catalog = currentCatalog,
                    Remarks = ""
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ShowCart));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Temporal_Movement temporalSale = await _context.Temporal_Movements.FindAsync(id);
            if (temporalSale == null)
            {
                return NotFound();
            }

            _context.Temporal_Movements.Remove(temporalSale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ShowCart));
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
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

            Temporal_Movement temporal_Movement = await _context.Temporal_Movements
                .Include(tm => tm.Details)
                .ThenInclude(d => d.Catalog)
                .Where(tm => tm.User.Id == user.Id)
                .Where(tm => tm.Status == Enums.MovementStatus.OPENED)
                .FirstOrDefaultAsync();

            if (null == temporal_Movement)
            {
                return NotFound();
            }

            var details = _context.Temporal_Movements
                    .Include(tm => tm.Details)
                    .ThenInclude(d => d.Catalog)
                    .Where(tm => tm.User.Id == user.Id)
                    .SelectMany(tm => tm.Details);

            var selectedTool = details
                    .Where(d=> d.Catalog.ToolId == tool.Id );


            EditTemporalMovementViewModel model = new()
            {
                Id = temporal_Movement.Id,
                ToolId = tool.Id,
                Name = tool.Name,
                Description = tool.Description,
                EAN = tool.EAN,
                Quantity = selectedTool.Count(),
                Remarks = details.FirstOrDefault().Remarks
            };

            List<Tool> tools = await _context.Tools
                .Include(p => p.ToolImages)
                .Include(p => p.ToolCategories)
                .OrderBy(p => p.Description)
                .ToListAsync();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditTemporalMovementViewModel model)
        {
            if (id == null)
            {
                return NotFound();
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

            try
            {
                Temporal_Movement temporal_Movement = await _context.Temporal_Movements.FindAsync(id);
                var details = _context.Temporal_Movements
                .Include(tm => tm.Details)
                .ThenInclude(d => d.Catalog)
                .Where(tm => tm.User.Id == user.Id)
                .SelectMany(tm => tm.Details);

                var selectedTool = details
                        .Where(d => d.Catalog.ToolId == tool.Id);

                float currentQuantity = selectedTool.Count();

                if (currentQuantity != model.Quantity)
                {
                    if(currentQuantity > model.Quantity)
                    {
                        float diff = currentQuantity - model.Quantity;
                        for(float i = 0; i < diff; i++)
                        {
                            DecreaseQuantity(id);
                        }
                    }
                    else
                    {
                        float diff = model.Quantity - currentQuantity;
                        for (float i = 0; i < diff; i++)
                        {
                            IncreaseQuantity(id);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                _flashMessage.Info(exception.Message);
                return View(model);
            }

            return RedirectToAction(nameof(ShowCart));
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