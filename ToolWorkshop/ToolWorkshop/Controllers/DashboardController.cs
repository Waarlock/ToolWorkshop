using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToolWorkshop.Data;
using ToolWorkshop.Helpers;
using ToolWorkshop.Enums;
using Microsoft.EntityFrameworkCore;


namespace ToolWorkshop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public DashboardController(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.UsersCount = _context.Users.Count();
            ViewBag.ToolsCount = _context.Tools.Count();
         //   ViewBag.NewOrdersCount = _context.Sales.Where(o => o.OrderStatus == OrderStatus.Nuevo).Count();
          //  ViewBag.ConfirmedOrdersCount = _context.Sales.Where(o => o.OrderStatus == OrderStatus.Confirmado).Count();

            return View(await _context.Temporal_Movements
                    .Include(u => u.User)
                    .Include(p => p.Tool).ToListAsync());
        }
    }
}
