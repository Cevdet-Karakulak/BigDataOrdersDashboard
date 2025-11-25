using BigDataOrdersDashboard.Context;
using Microsoft.AspNetCore.Mvc;

namespace BigDataOrdersDashboard.Controllers
{
    public class CustomerDetailController : Controller
    {
        private readonly BigDataOrdersDbContext _context;

        public CustomerDetailController(BigDataOrdersDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {
            if (id == 0)
                return RedirectToAction("CustomerList", "Customer");

            ViewBag.CustomerId = id;
            return View();
        }
    }
}
