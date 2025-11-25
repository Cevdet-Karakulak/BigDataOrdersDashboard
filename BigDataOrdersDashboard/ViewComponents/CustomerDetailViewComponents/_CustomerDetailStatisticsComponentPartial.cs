using BigDataOrdersDashboard.Context;
using Microsoft.AspNetCore.Mvc;

namespace BigDataOrdersDashboard.ViewComponents.CustomerDetailViewComponents
{
    public class _CustomerDetailStatisticsComponentPartial:ViewComponent
    {
        private readonly BigDataOrdersDbContext _context;
        public _CustomerDetailStatisticsComponentPartial(BigDataOrdersDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int id)
        {
            ViewBag.TotalOrderCount = _context.Orders.Count(x => x.CustomerId == id);

            ViewBag.CompletedOrderCount = _context.Orders.Count(x => x.CustomerId == id && x.OrderStatus == "Tamamlandı");

            ViewBag.CanceledOrderCount = _context.Orders.Count(x => x.CustomerId == id && x.OrderStatus == "İptal Edildi");

            ViewBag.GetCustomerCountry = _context.Customers
                .Where(x => x.CustomerId == id)
                .Select(y => y.CustomerCountry)
                .FirstOrDefault() ?? "Bilinmiyor";

            ViewBag.GetCustomerCity = _context.Customers
                .Where(x => x.CustomerId == id)
                .Select(y => y.CustomerCity)
                .FirstOrDefault() ?? "Bilinmiyor";

            ViewBag.TotalSpentMoney = _context.Orders
                .Where(x => x.CustomerId == id)
                .Sum(x => x.Quantity * x.Product.UnitPrice);

            return View();
        }



    }
}
