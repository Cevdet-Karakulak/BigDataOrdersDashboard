using BigDataOrdersDashboard.Context;
using BigDataOrdersDashboard.Dtos.ForecastDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;
using System.Globalization;

namespace BigDataOrdersDashboard.Controllers
{
    public class ForecastController : Controller
    {
        private readonly BigDataOrdersDbContext _context;
        private readonly MLContext _mLContext;
        public ForecastController(BigDataOrdersDbContext context, MLContext mLContext)
        {
            _context = context;
            _mLContext = mLContext;
        }
        public IActionResult PaymentMethodForecast()
        {
            //2025 Yılı Verilerinin Çekilmesi
            var startDate = new DateTime(2025, 1, 1);
            var endDate = new DateTime(2025, 12, 31);

            var monthlyPaymentData = _context.Orders
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .AsEnumerable()
                .GroupBy(o => new
                {
                    Month = new DateTime(o.OrderDate.Year, o.OrderDate.Month, 1),
                    o.PaymentMethod
                })
                .Select(g => new
                {
                    Month = g.Key.Month,
                    PaymentMethod = g.Key.PaymentMethod,
                    OrderCount = g.Count()
                })
                .OrderBy(x => x.Month)
                .ToList();

            //Tahmin Sonuçlarını Tutacak Liste
            var forecasts = new List<Object>();

            //Her Ödeme Yöntemi İçin Ayrı Ayrı Model Oluşturulması
            foreach (var method in monthlyPaymentData.Select(x => x.PaymentMethod).Distinct())
            {
                var methodData = monthlyPaymentData
                    .Where(x => x.PaymentMethod == method)
                   .Select((x, index) => new PaymentForecastData
                   {
                       PaymentMethod = method,
                       MonthIndex = index + 1,
                       OrderCount = x.OrderCount
                   }).ToList();

                var dataView = _mLContext.Data.LoadFromEnumerable(methodData);

                //Forecast Modeli
                var pipeline = _mLContext.Forecasting.ForecastBySsa(
                    outputColumnName: "ForecastedValues",
                    inputColumnName: nameof(PaymentForecastData.OrderCount),
                    windowSize: 4,
                    seriesLength: methodData.Count,
                    trainSize: methodData.Count,
                    horizon: 3,
                    confidenceLevel: 0.95f
                    );

                var model = pipeline.Fit(dataView);
                var engine = model.CreateTimeSeriesEngine<PaymentForecastData, PaymentForecastPrediction>(_mLContext);
                var prediction = engine.Predict();

                //2026 Yılı Ocak Şubat Mart Ayı Tahminleri

                for (int i = 0; i < prediction.ForecastedValues.Length; i++)
                {
                    forecasts.Add(new
                    {
                        PaymentMethod = method,
                        Month = new DateTime(2026, i + 1, 1).ToString("yyyy MMMM", new CultureInfo("tr-TR")),
                        ForecastedCount = (int)prediction.ForecastedValues[i]
                    });
                }
            }


            return View(forecasts);
        }

        public IActionResult GermanyCitiesForecast()
        {
            var startDate = new DateTime(2023, 1, 1);
            var endDate = new DateTime(2025, 12, 31);

            var germanyCityData = _context.Orders
                .Include(o => o.Customer)
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate && o.Customer.CustomerCountry == "Almanya")
                .AsEnumerable()
                .GroupBy(o => new
                {
                    o.Customer.CustomerCity,
                    Year = o.OrderDate.Year,
                    Month = o.OrderDate.Month
                })
                .Select(g => new
                {
                    City = g.Key.CustomerCity,
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    DateKey = $"{g.Key.Year}-{g.Key.Month:D2}",
                    OrderCount = g.Count()
                })
                .OrderBy(xP => xP.City)
                .ThenBy(x => x.DateKey)
                .ToList();

            var forecasts = new List<object>();

            foreach (var city in germanyCityData.Select(x => x.City).Distinct())
            {
                var cityData = germanyCityData
                    .Where(x => x.City == city)
                    .Select((x, index) => new GermanyCitiesForecastData
                    {
                        City = city,
                        MonthIndex = index + 1,
                        OrderCount = x.OrderCount
                    }).ToList();

                if (cityData.Count < 4)
                    continue;

                var dataView = _mLContext.Data.LoadFromEnumerable(cityData);

                var pipeline = _mLContext.Forecasting.ForecastBySsa(
                    outputColumnName: "ForecastedValues",
                    inputColumnName: nameof(GermanyCitiesForecastData.OrderCount),
                    windowSize: 12,
                    seriesLength: cityData.Count,
                    trainSize: cityData.Count,
                    horizon: 12,
                    confidenceLevel: 0.95f
                    );

                var model = pipeline.Fit(dataView);
                var engine = model.CreateTimeSeriesEngine<GermanyCitiesForecastData, GermanyCitiesForecastPrediction>(_mLContext);

                var prediction = engine.Predict();

                var yearlyForecast = (int)prediction.ForecastedValues.Sum();

                var year2024Count = germanyCityData
                    .Where(x => x.City == city && x.Year == 2024)
                    .Sum(x => x.OrderCount);

                var year2025Count = germanyCityData
                    .Where(x => x.City == city && x.Year == 2025)
                    .Sum(x => x.OrderCount);

                var diff = yearlyForecast - year2025Count;
                double? growthRate = year2025Count > 0
                    ? (diff / (double)year2025Count) * 100.0
                    : (double?)null;

                forecasts.Add(new
                {
                    City = city,
                    Year2024 = year2024Count,
                    Year2025 = year2025Count,
                    Year = "2026",
                    ForecastedCount = yearlyForecast,
                    DiffTo2025 = diff,      
                    GrowthRate = growthRate    
                });
            }

            return View(forecasts);
        }
        public IActionResult TurkeyCitiesForecast()
        {
            var startDate = new DateTime(2023, 1, 1);
            var endDate = new DateTime(2025, 12, 31);

            var turkeyCityData = _context.Orders
                .Include(o => o.Customer)
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate && o.Customer.CustomerCountry == "Türkiye")
                .AsEnumerable()
                .GroupBy(o => new
                {
                    o.Customer.CustomerCity,
                    Year = o.OrderDate.Year,
                    Month = o.OrderDate.Month
                })
                .Select(g => new
                {
                    City = g.Key.CustomerCity,
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    DateKey = $"{g.Key.Year}-{g.Key.Month:D2}",
                    OrderCount = g.Count()
                })
                .OrderBy(xP => xP.City)
                .ThenBy(x => x.DateKey)
                .ToList();

            var forecasts = new List<object>();

            foreach (var city in turkeyCityData.Select(x => x.City).Distinct())
            {
                var cityData = turkeyCityData
                    .Where(x => x.City == city)
                    .Select((x, index) => new TurkeyCitiesForecastData
                    {
                        City = city,
                        MonthIndex = index + 1,
                        OrderCount = x.OrderCount
                    }).ToList();

                if (cityData.Count < 4)
                    continue;

                var dataView = _mLContext.Data.LoadFromEnumerable(cityData);

                var pipeline = _mLContext.Forecasting.ForecastBySsa(
                    outputColumnName: "ForecastedValues",
                    inputColumnName: nameof(TurkeyCitiesForecastData.OrderCount),
                    windowSize: 12,
                    seriesLength: cityData.Count,
                    trainSize: cityData.Count,
                    horizon: 12,
                    confidenceLevel: 0.95f
                    );

                var model = pipeline.Fit(dataView);
                var engine = model.CreateTimeSeriesEngine<TurkeyCitiesForecastData, TurkeyCitiesForecastPrediction>(_mLContext);

                var prediction = engine.Predict();

                var yearlyForecast = (int)prediction.ForecastedValues.Sum();

                var year2024Count = turkeyCityData
                    .Where(x => x.City == city && x.Year == 2024)
                    .Sum(x => x.OrderCount);

                var year2025Count = turkeyCityData
                    .Where(x => x.City == city && x.Year == 2025)
                    .Sum(x => x.OrderCount);

                var diff = yearlyForecast - year2025Count;
                double? growthRate = year2025Count > 0
                    ? (diff / (double)year2025Count) * 100.0
                    : (double?)null;

                forecasts.Add(new
                {
                    City = city,
                    Year2024 = year2024Count,
                    Year2025 = year2025Count,
                    Year = "2026",
                    ForecastedCount = yearlyForecast,
                    DiffTo2025 = diff,
                    GrowthRate = growthRate
                });
            }

            return View(forecasts);
        }
    }
}
