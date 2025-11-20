using Microsoft.Extensions.Caching.Memory;
using System.Xml.Linq;

namespace BigDataOrdersDashboard.Services
{
    public class CurrencyService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _memoryCache;

        public CurrencyService(HttpClient httpClient, IMemoryCache memoryCache)
        {
            _httpClient = httpClient;
            _memoryCache = memoryCache;
        }

        public async Task<decimal> GetEuroRateAsync()
        {
            const string cacheKey = "EuroRate";

            // ⭐ Cache'de varsa direkt dön
            if (_memoryCache.TryGetValue(cacheKey, out decimal cachedRate))
            {
                return cachedRate;
            }

            // ⭐ Api'den çek
            string url = "https://www.tcmb.gov.tr/kurlar/today.xml";
            string xmlString = await _httpClient.GetStringAsync(url);

            var doc = XDocument.Parse(xmlString);
            var euroNode = doc.Descendants("Currency")
                              .FirstOrDefault(x => x.Attribute("Kod")?.Value == "EUR");

            string sellingValue = euroNode.Element("BanknoteSelling")?.Value;
            decimal euroRate = decimal.Parse(sellingValue.Replace(".", ","));

            // ⭐ Cache'e koy (12 saat sakla)
            _memoryCache.Set(cacheKey, euroRate, TimeSpan.FromHours(12));

            return euroRate;
        }
        public async Task<(decimal rate, DateTime time)> GetEuroRateWithTimeAsync()
        {
            const string cacheKey = "EuroRateWithTime";

            if (_memoryCache.TryGetValue(cacheKey, out (decimal rate, DateTime time) cached))
            {
                return cached;
            }

            string url = "https://www.tcmb.gov.tr/kurlar/today.xml";
            string xmlString = await _httpClient.GetStringAsync(url);

            var doc = XDocument.Parse(xmlString);
            var euroNode = doc.Descendants("Currency")
                              .FirstOrDefault(x => x.Attribute("Kod")?.Value == "EUR");

            string sellingValue = euroNode.Element("BanknoteSelling")?.Value;

            decimal euroRate = decimal.Parse(sellingValue.Replace(".", ","));
            DateTime fetchTime = DateTime.Now;

            _memoryCache.Set(cacheKey, (euroRate, fetchTime), TimeSpan.FromHours(12));

            return (euroRate, fetchTime);
        }
    }
}
