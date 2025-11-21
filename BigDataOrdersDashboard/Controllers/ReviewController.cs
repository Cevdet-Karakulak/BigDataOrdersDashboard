using BigDataOrdersDashboard.Context;
using BigDataOrdersDashboard.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BigDataOrdersDashboard.Controllers
{
    public class ReviewController : Controller
    {
        private readonly BigDataOrdersDbContext _context;
        public ReviewController(BigDataOrdersDbContext context)
        {
            _context = context;
        }
        public IActionResult ReviewList(int page = 1)
        {
            int pageSize = 12;
            var values = _context.Reviews
                                 .OrderBy(p => p.ReviewId)
                                 .Skip((page - 1) * pageSize)
                                 .Take(pageSize)
                                 .Include(y => y.Product)
                                 .Include(z => z.Customer)
                                 .ToList();

            int totalCount = _context.Reviews.Count();
            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            ViewBag.CurrentPage = page;

            return View(values);

        }

        [HttpGet]
        public IActionResult CreateReview()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateReview(Review review)
        {
            review.ReviewDate= DateTime.Now;
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return RedirectToAction("ReviewList");
        }

        public IActionResult DeleteReview(int id)
        {
            var value = _context.Reviews.Find(id);
            _context.Reviews.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("ReviewList");
        }

        [HttpGet]
        public IActionResult UpdateReview(int id)
        {
            var value = _context.Reviews.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateReview(Review review)
        {
            _context.Reviews.Update(review);
            _context.SaveChanges();
            return RedirectToAction("ReviewList");
        }
    }
}
