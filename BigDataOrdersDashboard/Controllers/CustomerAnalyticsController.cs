using Microsoft.AspNetCore.Mvc;

namespace BigDataOrdersDashboard.Controllers
{
    public class CustomerAnalyticsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
