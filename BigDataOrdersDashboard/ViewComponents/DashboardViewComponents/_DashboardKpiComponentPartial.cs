using BigDataOrdersDashboard.Context;
using Microsoft.AspNetCore.Mvc;

namespace BigDataOrdersDashboard.ViewComponents.DashboardViewComponents
{
    public class _DashboardKpiComponentPartial : ViewComponent
    {
        private readonly BigDataOrdersDbContext _context;

        public _DashboardKpiComponentPartial(BigDataOrdersDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // ============================
            //  KPI 1 – Bugünkü Sipariş Sayısı
            // ============================

            var today = DateTime.Today;
            var yesterday = today.AddDays(-1);

            var todayOrderCount = _context.Orders.Count(x => x.OrderDate.Date == today);
            var yesterdayOrderCount = _context.Orders.Count(x => x.OrderDate.Date == yesterday);

            decimal changeRate = 0;

            if (yesterdayOrderCount == 0)
                changeRate = todayOrderCount > 0 ? 100 : 0;
            else
                changeRate = ((decimal)(todayOrderCount - yesterdayOrderCount) / yesterdayOrderCount) * 100;

            changeRate = Math.Round(changeRate, 2);

            string changeColor;

            if (changeRate > 10) changeColor = "#24ad60";
            else if (changeRate > 0) changeColor = "#2ecc71";
            else if (changeRate == 0) changeColor = "#7f8c8d";
            else if (changeRate > -10) changeColor = "#f39c12";
            else changeColor = "#e74c3c";

            var dailyAverageOrders = _context.Orders
                .GroupBy(x => new { x.OrderDate.Year, x.OrderDate.Month, x.OrderDate.Day })
                .Select(g => g.Count())
                .Average();

            var todayVsAverage = Math.Round((todayOrderCount / dailyAverageOrders) * 100.0, 2);

            string trendingIcon =
                changeRate > 0 ? "zmdi zmdi-trending-up" :
                changeRate < 0 ? "zmdi zmdi-trending-down" :
                "zmdi zmdi-minus";

            ViewBag.TodayOrderCount = todayOrderCount;
            ViewBag.DailyOrderChange = changeRate;
            ViewBag.ChangeRateColor = changeColor;
            ViewBag.TodayVsAverageRatio = todayVsAverage;
            ViewBag.TrendingBadgeColor = changeColor;
            ViewBag.TrendingBadgeIcon = trendingIcon;


            // ============================
            //  KPI 2 – Son 7 Gün - İptal Edilen Sipariş
            // ============================

            var sevenDaysAgo = today.AddDays(-7);

            var totalOrders7Days = _context.Orders.Count(x =>
                x.OrderDate >= sevenDaysAgo && x.OrderDate < today.AddDays(1));

            var cancelledOrders7Days = _context.Orders.Count(x =>
                x.OrderStatus == "İptal Edildi" &&
                x.OrderDate >= sevenDaysAgo &&
                x.OrderDate < today.AddDays(1));

            decimal cancelRate = totalOrders7Days == 0
                ? 0
                : Math.Round(((decimal)cancelledOrders7Days / totalOrders7Days) * 100, 2);

            string cancelColor;
            string cancelText;
            string cancelBadgeIcon;

            if (cancelRate < 3)
            {
                cancelColor = "#2ecc71";
                cancelText = "Düşük İptal Oranı";
                cancelBadgeIcon = "zmdi zmdi-check-circle";
            }
            else if (cancelRate < 7)
            {
                cancelColor = "#f1c40f";
                cancelText = "Orta Seviye İptal";
                cancelBadgeIcon = "zmdi zmdi-alert-circle-o";
            }
            else if (cancelRate < 12)
            {
                cancelColor = "#e67e22";
                cancelText = "Yüksek İptal Oranı";
                cancelBadgeIcon = "zmdi zmdi-alert-triangle";
            }
            else
            {
                cancelColor = "#e74c3c";
                cancelText = "Kritik İptal Oranı";
                cancelBadgeIcon = "zmdi zmdi-alert-polygon";
            }

            ViewBag.CancelledOrders7Days = cancelledOrders7Days;
            ViewBag.CancelRate = cancelRate;
            ViewBag.CancelColor = cancelColor;
            ViewBag.CancelText = cancelText;
            ViewBag.CancelBadgeColor = cancelColor;
            ViewBag.CancelBadgeIcon = cancelBadgeIcon;


            // ============================
            //  KPI 3 – Tamamlanan Siparişler
            // ============================

            var totalOrders = _context.Orders.Count();
            var completedOrders = _context.Orders.Count(x => x.OrderStatus == "Tamamlandı");

            decimal completionRate = totalOrders == 0
                ? 0
                : Math.Round(((decimal)completedOrders / totalOrders) * 100, 2);

            string completionColor;
            string completionText;
            string completionBadgeIcon;

            if (completionRate >= 95)
            {
                completionColor = "#27ae60";
                completionText = "Mükemmel Performans";
                completionBadgeIcon = "zmdi zmdi-star";
            }
            else if (completionRate >= 80)
            {
                completionColor = "#2ecc71";
                completionText = "Çok İyi Performans";
                completionBadgeIcon = "zmdi zmdi-thumb-up";
            }
            else if (completionRate >= 60)
            {
                completionColor = "#f1c40f";
                completionText = "İyileşme Devam Ediyor";
                completionBadgeIcon = "zmdi zmdi-alert-circle-o";
            }
            else
            {
                completionColor = "#e74c3c";
                completionText = "Düşük Tamamlama Oranı";
                completionBadgeIcon = "zmdi zmdi-alert-triangle";
            }

            ViewBag.CompletedOrders = completedOrders;
            ViewBag.CompletionRate = completionRate;
            ViewBag.CompletionColor = completionColor;
            ViewBag.CompletionText = completionText;
            ViewBag.CompletionBadgeColor = completionColor;
            ViewBag.CompletionBadgeIcon = completionBadgeIcon;

            return View();
        }
    }
}
