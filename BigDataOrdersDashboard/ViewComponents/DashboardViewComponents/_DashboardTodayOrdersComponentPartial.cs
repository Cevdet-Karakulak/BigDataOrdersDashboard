using BigDataOrdersDashboard.Context;
using BigDataOrdersDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BigDataOrdersDashboard.ViewComponents.DashboardViewComponents
{
    public class _DashboardTodayOrdersComponentPartial : ViewComponent
    {
        private readonly BigDataOrdersDbContext _context;
        public _DashboardTodayOrdersComponentPartial(BigDataOrdersDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            var last10Orders = _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Product)
                .Where(o => o.OrderDate >= today && o.OrderDate < tomorrow)
                .OrderByDescending(o => o.OrderDate)
                .Take(5)
                .Select(o => new OrderSummaryViewModel
                {
                    OrderId = o.OrderId,
                    ProductName = o.Product.ProductName,
                    CustomerName = o.Customer.CustomerName + " " + o.Customer.CustomerSurname,
                    Quantity = o.Quantity,
                    PaymentMethod = o.PaymentMethod,
                    OrderStatus = o.OrderStatus,
                    UnitPrice = (double)o.Product.UnitPrice
                })
                .ToList();
            return View(last10Orders);
        }
    }
}
