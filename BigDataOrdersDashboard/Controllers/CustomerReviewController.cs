using Microsoft.AspNetCore.Mvc;

namespace BigDataOrdersDashboard.Controllers
{
    public class CustomerReviewController : Controller
    {
        public IActionResult CustomerReviewWithOpenAI()
        {
            return View();
        }
    }
}
