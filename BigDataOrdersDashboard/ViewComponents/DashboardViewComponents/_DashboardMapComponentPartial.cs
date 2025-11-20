using BigDataOrdersDashboard.Context;
using BigDataOrdersDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace BigDataOrdersDashboard.ViewComponents.DashboardViewComponents
{
    public class _DashboardMapComponentPartial : ViewComponent
    {
        private readonly BigDataOrdersDbContext _context;
        public _DashboardMapComponentPartial(BigDataOrdersDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                        command.CommandText = @"SELECT 
                                t1.CustomerCountry AS Country,
                                t1.Total2023,
                                t2.Total2024,
                                CAST(((t2.Total2024 - t1.Total2023) * 100.0 / t1.Total2023) AS DECIMAL(5,2)) AS ChangeRate
                            FROM
                            (
                                SELECT 
                                    c.CustomerCountry, 
                                    COUNT(*) AS Total2023
                                FROM Orders o
                                INNER JOIN Customers c ON o.CustomerId = c.CustomerId
                                WHERE o.OrderDate >= '2023-01-01' AND o.OrderDate < '2024-01-01'
                                GROUP BY c.CustomerCountry
                            ) AS t1
                            INNER JOIN
                            (
                                SELECT 
                                    c.CustomerCountry, 
                                    COUNT(*) AS Total2024
                                FROM Orders o
                                INNER JOIN Customers c ON o.CustomerId = c.CustomerId
                                WHERE o.OrderDate >= '2024-01-01' AND o.OrderDate < '2025-01-01'
                                GROUP BY c.CustomerCountry
                            ) AS t2
                            ON t1.CustomerCountry = t2.CustomerCountry;";

                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    var result = new List<CountryReportDto>();
                    while (reader.Read())
                    {
                        var countryName = reader.GetString(0);
                        result.Add(new CountryReportDto
                        {
                            Country =countryName,
                            Total2023 = reader.GetInt32(1),
                            Total2024 = reader.GetInt32(2),
                            ChangeRate = reader.GetDecimal(3),
                            Latitude = CountryCoordinates.GetLat(countryName),
                            Longitude = CountryCoordinates.GetLon(countryName)
                        });
                    }
                    return View(result);
                }
            }
        }
    }
}
