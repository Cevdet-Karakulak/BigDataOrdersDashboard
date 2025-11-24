using BigDataOrdersDashboard.Context;
using Microsoft.AspNetCore.Mvc;

namespace BigDataOrdersDashboard.ViewComponents.CustomerDetailViewComponents
{
    public class _CustomerDetailMainCoverTableComponentPartial : ViewComponent
    {
        private readonly BigDataOrdersDbContext _context;
        public _CustomerDetailMainCoverTableComponentPartial(BigDataOrdersDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var value = _context.Customers.Where(x=>x.CustomerId==8).FirstOrDefault();
            return View(value);
        }
    }
}
