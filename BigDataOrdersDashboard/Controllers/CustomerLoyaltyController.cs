using BigDataOrdersDashboard.Context;
using BigDataOrdersDashboard.Dtos.LoyaltyDtos;
using BigDataOrdersDashboard.Dtos.LoyaltyMLDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;

namespace BigDataOrdersDashboard.Controllers
{
    public class CustomerLoyaltyController : Controller
    {
        private readonly BigDataOrdersDbContext _context;
        private readonly string _modelPath = "wwwroot/mlmodels/LoyaltyScoreModel.zip";
        public CustomerLoyaltyController(BigDataOrdersDbContext context)
        {
            _context = context;
        }
        public IActionResult ItalyLoyaltyScore()
        {
            var loyaltScores = _context.Customers
                .Include(c => c.Orders)
                .ThenInclude(o => o.Product)
                .Where(c => c.CustomerCountry == "İtalya" && c.CustomerCity == "Parma" || c.CustomerCity == "Bologna" || c.CustomerCity == "Como" || c.CustomerCity == "Siena" || c.CustomerCity == "Verona" || c.CustomerCity == "Bergamo" || c.CustomerCity == "Bari" || c.CustomerCity == "Venedik")
                .Select(c => new
                {
                    CustomerName = c.CustomerName + " " + c.CustomerSurname,
                    TotalOrders = c.Orders.Count(),
                    TotalSpent = c.Orders.Sum(o => o.Quantity * o.Product.UnitPrice),
                    LastOrderDate = c.Orders.Max(o => (DateTime?)o.OrderDate)
                })
                .AsEnumerable()
                .Select(x =>
                {
                    var daySinceLastOrder = (x.LastOrderDate.HasValue)
                        ? (DateTime.Now - x.LastOrderDate.Value).TotalDays
                        : double.MaxValue;

                    double recenyScore = daySinceLastOrder switch
                    {
                        <= 30 => 100,
                        <= 90 => 75,
                        <= 180 => 50,
                        <= 365 => 25,
                        _ => 10
                    };

                    double frequencyScore = x.TotalOrders switch
                    {
                        >= 20 => 100,
                        >= 10 => 80,
                        >= 5 => 60,
                        >= 2 => 40,
                        1 => 20,
                        _ => 10
                    };

                    double monetaryScore = x.TotalSpent switch
                    {
                        >= 5000 => 100,
                        >= 3000 => 80,
                        >= 1000 => 60,
                        >= 500 => 40,
                        >= 100 => 20,
                        _ => 10
                    };

                    double loyaltyScore = (recenyScore * 0.4) + (frequencyScore * 0.3) + (monetaryScore * 0.3);

                    return new LoyaltyScoreDto
                    {
                        CustomerName = x.CustomerName,
                        TotalOrders = x.TotalOrders,
                        TotalSpent = Math.Round(x.TotalSpent, 2),
                        LastOrderDate = x.LastOrderDate,
                        LoyaltyScore = Math.Round(loyaltyScore, 2)
                    };
                }).OrderByDescending(x => x.LoyaltyScore).ToList();
            return View(loyaltScores);
        }
        public IActionResult TurkeyLoyaltyScore()
        {
            var turkeyCities = new[] {
            "Adana","Afyonkarahisar","Ankara","Antalya","Aydın","Balıkesir","Bursa",
            "Çanakkale","Denizli","Edirne","Eskişehir","Gaziantep","Giresun","Hatay",
            "Isparta","İstanbul","İzmir","Kahramanmaraş","Kayseri","Kocaeli","Konya",
            "Manisa","Mersin","Ordu","Rize","Sakarya","Samsun","Şanlıurfa","Tekirdağ",
            "Trabzon"
        };

            var loyaltyScores = _context.Customers
                .Include(c => c.Orders)
                .ThenInclude(o => o.Product)
                .Where(c => c.CustomerCountry == "Türkiye" && turkeyCities.Contains(c.CustomerCity))
                .Select(c => new
                {
                    CustomerName = c.CustomerName + " " + c.CustomerSurname,
                    TotalOrders = c.Orders.Count(),
                    TotalSpent = c.Orders.Sum(o => o.Quantity * o.Product.UnitPrice),
                    LastOrderDate = c.Orders.Max(o => (DateTime?)o.OrderDate)
                })
                .AsEnumerable() // Score hesaplamaları C# tarafında yapılacak
                .Select(x =>
                {
                    var daySinceLastOrder = x.LastOrderDate.HasValue
                        ? (DateTime.Now - x.LastOrderDate.Value).TotalDays
                        : double.MaxValue;

                    double recencyScore = daySinceLastOrder switch
                    {
                        <= 30 => 100,
                        <= 90 => 75,
                        <= 180 => 50,
                        <= 365 => 25,
                        _ => 10
                    };

                    double frequencyScore = x.TotalOrders switch
                    {
                        >= 20 => 100,
                        >= 10 => 80,
                        >= 5 => 60,
                        >= 2 => 40,
                        1 => 20,
                        _ => 10
                    };

                    double monetaryScore = (double)x.TotalSpent switch
                    {
                        >= 5000 => 100,
                        >= 3000 => 80,
                        >= 1000 => 60,
                        >= 500 => 40,
                        >= 100 => 20,
                        _ => 10
                    };

                    double loyaltyScore = (recencyScore * 0.4) + (frequencyScore * 0.3) + (monetaryScore * 0.3);

                    return new LoyaltyScoreDto
                    {
                        CustomerName = x.CustomerName,
                        TotalOrders = x.TotalOrders,
                        TotalSpent = Math.Round(x.TotalSpent, 2),
                        LastOrderDate = x.LastOrderDate,
                        LoyaltyScore = Math.Round(loyaltyScore, 2)
                    };
                })
                .OrderByDescending(x => x.LoyaltyScore)
                .ToList();

            return View(loyaltyScores);
        }

        public IActionResult ItalyLoyaltyScoreWithML()
        {
            //İtalya'daki Belli Şehirlerin Sipariş Listesi
            var data = _context.Customers
              .Include(c => c.Orders)
              .ThenInclude(o => o.Product)
              .Where(c => c.CustomerCountry == "İtalya" &&
              (c.CustomerCity == "Parma" ||
               c.CustomerCity == "Bologna" ||
               c.CustomerCity == "Como" ||
               c.CustomerCity == "Siena" ||
               c.CustomerCity == "Verona" ||
               c.CustomerCity == "Bergamo" ||
               c.CustomerCity == "Bari" ||
               c.CustomerCity == "Venedik"))
              .AsEnumerable()
              .Select(c =>
              {
                  //Müşterinin Son Sipariş Tarihini Bul
                  var lastOrderDate = c.Orders.Max(o => (DateTime?)o.OrderDate);

                  //Son siparişin üzerinden kaç gün geçtiğini hesaplama 
                  var daySince = lastOrderDate.HasValue ? Math.Round((DateTime.Now - lastOrderDate.Value).TotalDays) : 999;

                  //Rfm Metrikleri
                  double recency = daySince;
                  double frequency = c.Orders.Count();
                  double monetary = (double)Math.Round(c.Orders.Sum(o => o.Quantity * o.Product.UnitPrice), 2);

                  //Loyalty Score ağırlıklı ortalamanın bulunması

                  double loyalty = (RecencyScore(recency) * 0.4) +
                                 (FrequencyScore(frequency) * 0.3) +
                                 (MonetaryScore(monetary) * 0.3);

                  //ML .Net'e gidecek Veri Listesi

                  return new LoyaltyScoreMLDataDto
                  {
                      CustomerName = c.CustomerName + " " + c.CustomerSurname,
                      Recency = (float)recency,
                      Frequency = (float)frequency,
                      Monetary = (float)monetary,
                      LoyaltyScore = (float)loyalty
                  };
              }).ToList();

            //ML İşlemleri

            var mlContext = new MLContext();
            IDataView dataView = mlContext.Data.LoadFromEnumerable(data);                     

            var pipeline = mlContext.Transforms
                    .Concatenate("Features", "Recency", "Frequency", "Monetary")
                    .Append(mlContext.Transforms.NormalizeMinMax("Features")) 
                    .Append(mlContext.Regression.Trainers.Sdca(
                        labelColumnName: "LoyaltyScore",
                        maximumNumberOfIterations: 100));

            //Modeli Eğitme
            var model = pipeline.Fit(dataView);

            //Modeli Kaydet
            mlContext.Model.Save(model, dataView.Schema, _modelPath);

            //Tahmin Metodu
            var predictionEngine = mlContext.Model.CreatePredictionEngine<LoyaltyScoreMLDataDto, LoyaltyScoreMLPredictionDto>(model);

            //Her müşteri için ML .Net Tahmini
            var results = data.Select(x =>
            {
                var prediction = predictionEngine.Predict(new LoyaltyScoreMLDataDto
                {
                    Recency = x.Recency,
                    Frequency = x.Frequency,
                    Monetary = x.Monetary
                });

                return new ResultLoyaltyScoreMLDto
                {
                    CustomerName = x.CustomerName,
                    Recency = x.Recency,
                    Frequency = x.Frequency,
                    Monetary = x.Monetary,
                    ActualLoyaltyScore = Math.Round(x.LoyaltyScore, 2),
                    PredictedLoyaltyScore = Math.Round(prediction.LoyaltyScore, 2)
                };
            }).OrderByDescending(x => x.PredictedLoyaltyScore).ToList();

            return View(results);
        }

        //Yardımcı Skor Metotlarının Hazırlanması

        private static double RecencyScore(double days) => days switch
        {
            <= 30 => 100,
            <= 90 => 75,
            <= 180 => 50,
            <= 365 => 25,
            _ => 10
        };

        private static double FrequencyScore(double orders) => orders switch
        {
            >= 20 => 100,
            >= 10 => 80,
            >= 5 => 60,
            >= 2 => 40,
            1 => 20,
            _ => 10
        };

        private static double MonetaryScore(double spent) => spent switch
        {
            >= 5000 => 100,
            >= 3000 => 80,
            >= 1000 => 60,
            >= 500 => 40,
            >= 100 => 20,
            _ => 10
        };

    }
}

