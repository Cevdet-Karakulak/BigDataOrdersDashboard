using Microsoft.AspNetCore.Mvc;

namespace BigDataOrdersDashboard.Controllers
{
    public class CustomerReviewController : Controller
    {
        public IActionResult CustomerReviewWithOpenAI(int id)
        {
            if (id == 0)
                return RedirectToAction("CustomerList", "Customer");

            ViewBag.CustomerId = id;
            return View();
        }
    }
}
