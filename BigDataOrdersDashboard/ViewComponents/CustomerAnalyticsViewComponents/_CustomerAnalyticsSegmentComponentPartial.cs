using BigDataOrdersDashboard.Context;
using Microsoft.AspNetCore.Mvc;

namespace BigDataOrdersDashboard.ViewComponents.CustomerAnalyticsViewComponents
{
    public class _CustomerAnalyticsSegmentComponentPartial:ViewComponent
    {
        private readonly BigDataOrdersDbContext _context;
        public _CustomerAnalyticsSegmentComponentPartial(BigDataOrdersDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
