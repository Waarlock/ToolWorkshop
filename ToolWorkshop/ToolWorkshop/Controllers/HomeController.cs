using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ToolWorkshop.Data;
using ToolWorkshop.Data.Entities;
using ToolWorkshop.Models;

namespace ToolWorkshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Tool>? tools = await _context.Tools
                .Include(t => t.ToolImages)
                .Include(t => t.ToolCategories)
                .OrderBy(t => t.Description)
                .ToListAsync();

            List<ToolsHomeViewModel> toolsHome = new() { new ToolsHomeViewModel() };
            int i = 1;
            foreach (Tool? tool in tools)
            {
                if (i == 1)
                {
                    toolsHome.LastOrDefault().Tool1 = tool;
                }
                if (i == 2)
                {
                    toolsHome.LastOrDefault().Tool2 = tool;
                }
                if (i == 3)
                {
                    toolsHome.LastOrDefault().Tool3 = tool;
                }
                if (i == 4)
                {
                    toolsHome.LastOrDefault().Tool4 = tool;
                    toolsHome.Add(new ToolsHomeViewModel());
                    i = 0;
                }
                i++;
            }

            return View(toolsHome);

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