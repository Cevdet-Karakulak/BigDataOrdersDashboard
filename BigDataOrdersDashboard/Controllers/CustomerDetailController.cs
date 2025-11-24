using Microsoft.AspNetCore.Mvc;

namespace BigDataOrdersDashboard.Controllers
{
    public class CustomerDetailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
