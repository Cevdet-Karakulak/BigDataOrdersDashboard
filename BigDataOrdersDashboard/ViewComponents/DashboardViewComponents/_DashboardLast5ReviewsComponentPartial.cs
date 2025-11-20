using BigDataOrdersDashboard.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BigDataOrdersDashboard.ViewComponents.DashboardViewComponents
{
    public class _DashboardLast5ReviewsComponentPartial : ViewComponent
    {
        private readonly BigDataOrdersDbContext _context;
        public _DashboardLast5ReviewsComponentPartial(BigDataOrdersDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            //var values = _context.Reviews.OrderByDescending(x => x.ReviewId).Include(y => y.Customer).Include(z => z.Product).Take(5).ToList();
            return View(/*values*/);
        }
    }
}
