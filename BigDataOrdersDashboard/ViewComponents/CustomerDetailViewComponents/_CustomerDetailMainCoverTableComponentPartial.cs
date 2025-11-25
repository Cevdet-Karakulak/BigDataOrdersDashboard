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

        public IViewComponentResult Invoke(int id)
        {
            var value = _context.Customers.FirstOrDefault(x => x.CustomerId == id);
            return View(value);
        }
    }
}
