using BigDataOrdersDashboard.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BigDataOrdersDashboard.ViewComponents.DashboardViewComponents
{
    public class _DashboardLowStockProductsComponentPartial : ViewComponent
    {
        private readonly BigDataOrdersDbContext _context;
        public _DashboardLowStockProductsComponentPartial(BigDataOrdersDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var values = _context.Products
                .Where(x => x.StockQuantity <= 9)
                .GroupBy(x => x.ProductName)                       // Aynı ismi grupla
                .Select(g => g.OrderBy(p => p.StockQuantity).First()) // En düşük stoklu olanı al
                .Take(15)
                .ToList();

            return View(values);
        }

    }
}
