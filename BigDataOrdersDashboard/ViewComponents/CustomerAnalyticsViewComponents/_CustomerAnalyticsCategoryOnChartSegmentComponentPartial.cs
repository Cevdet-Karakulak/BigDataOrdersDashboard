using BigDataOrdersDashboard.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BigDataOrdersDashboard.ViewComponents.CustomerAnalyticsViewComponents
{
    public class _CustomerAnalyticsCategoryOnChartSegmentComponentPartial : ViewComponent
    {
        private readonly BigDataOrdersDbContext _context;
        public _CustomerAnalyticsCategoryOnChartSegmentComponentPartial(BigDataOrdersDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            #region Statistics

            var today = DateTime.Today;

            var topCategoriesToday = _context.Orders
                .Include(o => o.Product)
                .ThenInclude(p => p.Category)
                .Where(x => x.OrderDate.Date == today)
                .GroupBy(o => o.Product.Category.CategoryName)
                .Select(g => new
                {
                    CategoryName = g.Key,
                    OrderCount = g.Count()
                })
                .OrderByDescending(x => x.OrderCount)
                .Take(3)
                .ToList();

            if (topCategoriesToday.Count > 0)
            {
                ViewBag.TopCategory1Name = topCategoriesToday[0].CategoryName;
                ViewBag.TopCategory1Count = topCategoriesToday[0].OrderCount;
            }

            if (topCategoriesToday.Count > 1)
            {
                ViewBag.TopCategory2Name = topCategoriesToday[1].CategoryName;
                ViewBag.TopCategory2Count = topCategoriesToday[1].OrderCount;
            }

            if (topCategoriesToday.Count > 2)
            {
                ViewBag.TopCategory3Name = topCategoriesToday[2].CategoryName;
                ViewBag.TopCategory3Count = topCategoriesToday[2].OrderCount;
            }

            #endregion

            #region Charts
            //Tüm Siparişleri Kategori Adı ve Yıla Göre Gruplama İşlemi
            var categoryData = _context.Orders
                .Include(o => o.Product)
                .ThenInclude(p => p.Category)
                .GroupBy(o => new
                {
                    Year = o.OrderDate.Year,
                    CategoryName = o.Product.Category.CategoryName
                })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    CategoryName = g.Key.CategoryName,
                    OrderCount = g.Count()
                })
                .OrderBy(x => x.Year)
                .ToList();


            //Her Bir Kategorinin Toplam Satış Sayısı

            var topCategories = categoryData
                .GroupBy(x => x.CategoryName)
                .Select(g => new
                {
                    CategoryName = g.Key,
                    TotalOrders = g.Sum(x => x.OrderCount)
                })
                .OrderByDescending(x => x.TotalOrders)
                .Take(5)
                .Select(x => x.CategoryName)
                .ToList();

            //Sadece bu 5 kategoriye ait verilerin listesi

            var filteredData = categoryData
                .Where(x => topCategories.Contains(x.CategoryName)).ToList();

            var years = filteredData.Select(x => x.Year).Distinct().OrderBy(x => x).ToList();

            var chartSeries = topCategories.Select(category => new
            {
                name = category,
                data = years.Select(y => filteredData.FirstOrDefault(cd => cd.CategoryName == category && cd.Year == y)?.OrderCount ?? 0).ToList()
            }).ToList();

            ViewBag.Years = years;
            ViewBag.Series = chartSeries;

            #endregion

            return View();
        }
    }
}
