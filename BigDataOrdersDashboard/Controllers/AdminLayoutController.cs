using Microsoft.AspNetCore.Mvc;

namespace BigDataOrdersDashboard.Controllers
{
    public class AdminLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
