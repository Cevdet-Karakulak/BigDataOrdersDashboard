using BigDataOrdersDashboard.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BigDataOrdersDashboard.ViewComponents.CustomerAnalyticsViewComponents
{
    public class _CustomerAnalyticsMainStatisticsComponentPartial : ViewComponent
    {
        private readonly BigDataOrdersDbContext _context;

        public _CustomerAnalyticsMainStatisticsComponentPartial(BigDataOrdersDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // Toplam müşteri sayısı
            var totalCustomerCount = _context.Customers.Count();
            ViewBag.TotalCustomerCount = totalCustomerCount;

            // Toplam sipariş
            var totalOrderCount = _context.Orders.Count();

            // Ortalama sipariş (bölme hatasına karşı korumalı)
            var averageOrderPerCustomerCount =
                totalCustomerCount == 0 ? 0 : totalOrderCount / totalCustomerCount;

            ViewBag.AverageOrderPerCustomerCount = averageOrderPerCustomerCount;

            // Son 3 ay aktif müşteri
            var threeMonthsAgo = DateTime.Now.AddMonths(-3);
            var activeCustomerCount = _context.Orders
                .Where(o => o.OrderDate >= threeMonthsAgo)
                .Select(o => o.CustomerId)
                .Distinct()
                .Count();

            ViewBag.ActiveCustomerCount = activeCustomerCount;

            // Son 6 ay pasif müşteri
            var sixMonthsAgo = DateTime.Now.AddMonths(-6);

            var inactiveCustomerCount = _context.Customers
                .Count(c => !_context.Orders
                    .Any(o => o.CustomerId == c.CustomerId && o.OrderDate >= sixMonthsAgo));

            ViewBag.InactiveCustomerCount = inactiveCustomerCount;

            return View();
        }
    }
}
