using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Diagnostics;
using ToolWorkshop.Common;
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
    
        public async Task<IActionResult> Confirm(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                _flashMessage.Danger("No se ha podido reconocer al usuario.");
                return RedirectToAction(nameof(Index));
            }
            Temporal_Movement temporal_Movement = await _context.Temporal_Movements
                 .Include(tm => tm.Details)
                 .ThenInclude(d => d.Catalog)
                 .ThenInclude(c => c.Tool)
                 .Where(tm => tm.User.Id == user.Id)
                 .Where(tm => tm.Status == Enums.MovementStatus.OPENED)
                 .FirstOrDefaultAsync();
            if (temporal_Movement == null)
            {
                _flashMessage.Danger("Lo sentimos ha ocurrido un error");
                return RedirectToAction(nameof(Index));
            }

            Movement movement = new()
            {
                Details = temporal_Movement.Details,
                Start_DateTime = DateTime.UtcNow,
                Status = Enums.MovementStatus.OPENED,
                User = user
            };

            IEnumerable<Movement_Detail> details = temporal_Movement.Details;

            foreach (Movement_Detail d in details)
            {
                Catalog c = d.Catalog;
                c.Status = Enums.CatalogStatus.UNAVAILABLE;
                _context.Catalogs.Update(c);
            }

            _context.Movements.Add(movement);
            _context.Temporal_Movements.Remove(temporal_Movement);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "NameDesc" : "";
            ViewData["StockSortParm"] = sortOrder == "Stock" ? "StockDesc" : "Stock";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;


            IQueryable<Tool> query = _context.Tools
                .Include(p => p.ToolImages)
                .Include(p => p.ToolCategories)
                .ThenInclude(pc => pc.Category);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(p => (p.Name.ToLower().Contains(searchString.ToLower()) ||
                                            p.ToolCategories.Any(pc => pc.Category.Name.ToLower().Contains(searchString.ToLower()))) );
            }
            else
            {
              //  query = query.Where(p => p.Stock > 0);
            }

            switch (sortOrder)
            {
                case "NameDesc":
                    query = query.OrderByDescending(p => p.Name);
                    break;
                case "Stock":
                    query = query.OrderBy(p => p.Stock);
                    break;
                case "StockDesc":
                    query = query.OrderByDescending(p => p.Stock);
                    break;
                default:
                    query = query.OrderBy(p => p.Name);
                    break;
            }

            int pageSize = 8;

            HomeViewModel model = new()
            { 
                Tools = await PaginatedList<Tool>.CreateAsync(query, pageNumber ?? 1, pageSize),

                Categories = await _context.Categories.ToListAsync()
            };

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
                _flashMessage.Danger("Lo sentimos ha ocurrido un error");
                return RedirectToAction(nameof(Index));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            Tool tool = await _context.Tools.FindAsync(id);
            if (tool == null)
            {
                _flashMessage.Danger("Lo sentimos ha ocurrido un error");
                return RedirectToAction(nameof(Index));
            }

            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                _flashMessage.Danger("No se ha podido reconocer al usuario.");
                return RedirectToAction(nameof(Index));
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
            else
            {
                _flashMessage.Info("Lo sentimos en el momento no hay disponibilidad de esta herramienta");
                return RedirectToAction(nameof(Index));
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
            
           
            return RedirectToAction(nameof(Index));
        }


        [Authorize]
        public async Task<IActionResult> ShowCart()
        {
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                _flashMessage.Danger("No se ha podido reconocer al usuario.");
                return RedirectToAction(nameof(Index));
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
                _flashMessage.Danger("Lo sentimos ha ocurrido un error");
                return RedirectToAction(nameof(Index));
            }

            Tool tool = await _context.Tools.FindAsync(id);
            if (tool == null)
            {
                _flashMessage.Danger("Lo sentimos ha ocurrido un error");
                return RedirectToAction(nameof(Index));
            }

            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                _flashMessage.Danger("No se ha podido reconocer al usuario.");
                return RedirectToAction(nameof(Index));
            }

            Temporal_Movement temporal_Movement = await _context.Temporal_Movements
                .Include(tm => tm.Details)
                .ThenInclude(d => d.Catalog)
                .ThenInclude(c=> c.Tool)
                .Where(tm => tm.User.Id == user.Id)
                .Where(tm => tm.Status == Enums.MovementStatus.OPENED)
                .FirstOrDefaultAsync();

            if (null == temporal_Movement)
            {
                _flashMessage.Danger("Lo sentimos ha ocurrido un error");
                return RedirectToAction(nameof(Index));
            }

            IEnumerable<Movement_Detail> selectedDetails = temporal_Movement.Details;
            IEnumerable<Catalog> selectedToolUnits = selectedDetails.Select(dt => dt.Catalog).Where(c=> c.ToolId == id);

            bool result = false;
            if (selectedToolUnits != null &&  selectedToolUnits.Any() && selectedToolUnits.Count() > 1)
            {
                float newQuantity = selectedToolUnits.Count() - 1;
                result = await SetToolQuantity(temporal_Movement, tool, newQuantity);
            }
            
            return result
                ?  RedirectToAction(nameof(ShowCart))
                : RedirectToAction(nameof(ShowCart))
             ;
        }

        
        public async Task<IActionResult> IncreaseQuantity(int? id)
        {
            if (id == null)
            {
                _flashMessage.Danger("Lo sentimos ha ocurrido un error");
                return RedirectToAction(nameof(Index));
            }

            Tool tool = await _context.Tools.FindAsync(id);
            if (tool == null)
            {
                _flashMessage.Danger("Lo sentimos ha ocurrido un error");
                return RedirectToAction(nameof(Index));
            }

            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                _flashMessage.Danger("No se ha podido reconocer al usuario.");
                return RedirectToAction(nameof(Index));
            }

            Temporal_Movement temporal_Movement = await _context.Temporal_Movements
                .Include(tm => tm.Details)
                .ThenInclude(d => d.Catalog)
                .ThenInclude(c => c.Tool)
                .Where(tm => tm.User.Id == user.Id)
                .Where(tm => tm.Status == Enums.MovementStatus.OPENED)
                .FirstOrDefaultAsync();

            if (null == temporal_Movement)
            {
                _flashMessage.Danger("Lo sentimos ha ocurrido un error");
                return RedirectToAction(nameof(Index));
            }

            IEnumerable<Movement_Detail> selectedDetails = temporal_Movement.Details;
            IEnumerable<Catalog> selectedToolUnits = selectedDetails.Select(dt => dt.Catalog).Where(c => c.ToolId == id);

            bool result = false;
            if (selectedToolUnits != null)
            {
                float newQuantity = selectedToolUnits.Count() + 1;
                result = await SetToolQuantity(temporal_Movement, tool, newQuantity);
            }

            return result
                ? RedirectToAction(nameof(ShowCart))
                : RedirectToAction(nameof(ShowCart))
             ;
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                _flashMessage.Danger("Lo sentimos ha ocurrido un error");
                return RedirectToAction(nameof(Index));
            }

            Tool tool = await _context.Tools.FindAsync(id);
            if (tool == null)
            {
                _flashMessage.Danger("Lo sentimos ha ocurrido un error");
                return RedirectToAction(nameof(Index));
            }

            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                _flashMessage.Danger("No se ha podido reconocer al usuario.");
                return RedirectToAction(nameof(Index)); ;
            }

            Temporal_Movement temporal_Movement = await _context.Temporal_Movements
                .Include(tm => tm.Details)
                .ThenInclude(d => d.Catalog)
                .ThenInclude(c => c.Tool)
                .Where(tm => tm.User.Id == user.Id)
                .Where(tm => tm.Status == Enums.MovementStatus.OPENED)
                .FirstOrDefaultAsync();

            if (temporal_Movement == null)
            {
                _flashMessage.Danger("Lo sentimos ha ocurrido un error");
                return RedirectToAction(nameof(Index));
            }

            IEnumerable<Movement_Detail> details = temporal_Movement.Details.Where(d=> d.Catalog.ToolId == tool.Id);

            foreach(Movement_Detail d in details)
            {
                Catalog c = d.Catalog;
                c.Status = Enums.CatalogStatus.AVAILABLE;
                _context.Catalogs.Update(c);
                _context.Movement_Details.Remove(d);
            }
           
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ShowCart));
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                _flashMessage.Danger("Lo sentimos ha ocurrido un error");
                return RedirectToAction(nameof(Index));
            }

            Tool tool = await _context.Tools.FindAsync(id);
            if (tool == null)
            {
                _flashMessage.Danger("Lo sentimos ha ocurrido un error");
                return RedirectToAction(nameof(Index));
            }

            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                _flashMessage.Danger("No se ha podido reconocer al usuario.");
                return RedirectToAction(nameof(Index));
            }

            Temporal_Movement temporal_Movement = await _context.Temporal_Movements
                .Include(tm => tm.Details)
                .ThenInclude(d => d.Catalog)
                .Where(tm => tm.User.Id == user.Id)
                .Where(tm => tm.Status == Enums.MovementStatus.OPENED)
                .FirstOrDefaultAsync();

            if (null == temporal_Movement)
            {
                _flashMessage.Danger("Lo sentimos ha ocurrido un error");
                return RedirectToAction(nameof(Index));
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
                _flashMessage.Danger("Lo sentimos ha ocurrido un error");
                return RedirectToAction(nameof(Index));
            }

            Tool tool = await _context.Tools.FindAsync(id);
            if (tool == null)
            {;
                _flashMessage.Danger("Lo sentimos ha ocurrido un error");
                return RedirectToAction(nameof(Index));
            }

            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                _flashMessage.Danger("No se ha podido reconocer al usuario.");
                return RedirectToAction(nameof(Index));
            }

            try
            {
                Temporal_Movement temporal_Movement = await _context.Temporal_Movements
                    .Include(tm => tm.Details)
                    .ThenInclude(d => d.Catalog)
                    .ThenInclude(c => c.Tool)
                    .Where(tm => tm.User.Id == user.Id)
                    .Where(tm => tm.Status == Enums.MovementStatus.OPENED)
                    .FirstOrDefaultAsync();

                if (null == temporal_Movement)
                {
                    _flashMessage.Danger("Lo sentimos ha ocurrido un error");
                    return RedirectToAction(nameof(Index));
                }

                IEnumerable<Movement_Detail> selectedDetails = temporal_Movement.Details;
                IEnumerable<Catalog> selectedToolUnits = selectedDetails.Select(dt => dt.Catalog).Where(c => c.ToolId == id);

                bool result = false;
                if (selectedToolUnits != null && selectedToolUnits.Any() && selectedToolUnits.Count() > 1)
                {
                    result = await SetToolQuantity(temporal_Movement, tool, model.Quantity);
                }

                return result
                    ? RedirectToAction(nameof(ShowCart))
                    : RedirectToAction(nameof(ShowCart))
                 ;
            }
            catch (Exception exception)
            {
                _flashMessage.Info(exception.Message);
                return View(model);
            }

            return RedirectToAction(nameof(ShowCart));
        }


        private async Task<bool> SetToolQuantity(Temporal_Movement temporal_Movement, Tool tool, float newQuantity, String remarks = "")
        {
            IEnumerable<Catalog> temporalCatalog = temporal_Movement.Details.Select(dt => dt.Catalog);

            float availableTools = _context.Catalogs.Include(c=> c.Tool).Count(c=> c.ToolId == tool.Id && c.Status == Enums.CatalogStatus.AVAILABLE);
           
            float requiredTools = 0;
            try
            {
                float currentRequested = temporalCatalog.Count(c => c.Tool.Id == tool.Id);
                requiredTools = newQuantity - currentRequested;
            }
            catch (NullReferenceException e)
            {
                requiredTools = newQuantity;
            }

            if (requiredTools == 0)
            {
                return true;
            }

            if (availableTools >= requiredTools)
            {
                if (requiredTools > 0)
                {
                    for (int i = 1; i <= requiredTools; i++)
                    {
                        Catalog currentCatalog = await _context.Catalogs.FirstOrDefaultAsync(c => c.Tool.Id == tool.Id && c.Status == Enums.CatalogStatus.AVAILABLE);
                        currentCatalog.Status = Enums.CatalogStatus.PICKED;

                        temporal_Movement.Details.Add(new Movement_Detail()
                        {
                            Catalog = currentCatalog,
                            Remarks = remarks
                        });
                    }
                }

                if (requiredTools < 0)
                {
                    for (int i = -1; i >= requiredTools; i--)
                    {
                        Movement_Detail selectedDetail = temporal_Movement.Details.FirstOrDefault(d=> d.Catalog.ToolId == tool.Id);
                        Catalog currentCatalog  = selectedDetail.Catalog;
                        temporal_Movement.Details.Remove(selectedDetail);
                        currentCatalog.Status = Enums.CatalogStatus.AVAILABLE;

                        _context.Catalogs.Update(currentCatalog);
                    }
                }
            }
            else
            {
                _flashMessage.Info("Lo sentimos en el momento no hay disponibilidad de esta herramienta");
                return false;
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
            return true;
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