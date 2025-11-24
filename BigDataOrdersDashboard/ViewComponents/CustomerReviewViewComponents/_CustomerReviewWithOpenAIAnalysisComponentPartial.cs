using BigDataOrdersDashboard.Context;
using BigDataOrdersDashboard.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BigDataOrdersDashboard.ViewComponents.CustomerReviewViewComponents
{
    public class _CustomerReviewWithOpenAIAnalysisComponentPartial : ViewComponent
    {
        private readonly BigDataOrdersDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey;

        public _CustomerReviewWithOpenAIAnalysisComponentPartial(
            BigDataOrdersDbContext context,
            IHttpClientFactory httpClientFactory,
            IOptions<OpenAISettings> openAiOptions)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _apiKey = openAiOptions.Value.ApiKey;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            id = 657;

            var reviews = await _context.Reviews
                .Where(r => r.CustomerId == id)
                .OrderByDescending(r => r.ReviewDate)
                .Take(10)
                .Select(r => new
                {
                    r.Rating,
                    r.Sentiment,
                    r.ReviewText,
                    r.ReviewDate
                })
                .ToListAsync();

            if (!reviews.Any())
            {
                ViewBag.AnalysisSection1 = "<p>Bu müşterinin yorumu yok.</p>";
                return View();
            }

            var jsonData = JsonSerializer.Serialize(reviews);

            string prompt = $@"
⚠️ Sadece saf HTML üret. Kod bloğu üretme. Markdown verme.

Sen bir müşteri davranış analisti + psikoloji destekli yorum analiz uzmanısın.

Aşağıdaki müşteriye ait son yorumları analiz et ve HTML oluştur.

Kullanacağın başlıklar ve format:

<h4>👤 Müşteri Yorum Profili</h4>
<p><b>Genel tutum:</b> ...</p>
<p><b>Yorum tarzı:</b> ...</p>
<p><b>Ortalama Rating:</b> ...</p>

<h4>📊 Duygu & Ton Analizi</h4>
<p><b>Olumlu:</b> ...</p>
<p><b>Olumsuz:</b> ...</p>
<p><b>Nötr:</b> ...</p>
<p><b>Dil ve ton:</b> ...</p>

<h4>🧠 Karakter Analizi (Review Temelli)</h4>
<ul>
<li>Memnuniyet eşiği: ...</li>
<li>Şikayet hassasiyeti: ...</li>
<li>Beklenti seviyesi: ...</li>
<li>Kişilik tipi: ...</li>
</ul>

<h4>🔥 Şikayet & Övgü Temaları</h4>
<p><b>Şikayet ettiği konular:</b></p>
<ul><li>...</li></ul>

<p><b>Övdüğü konular:</b></p>
<ul><li>...</li></ul>

<h4>📈 Davranış Trendi</h4>
<p><b>Duygu değişimi:</b> ...</p>
<p><b>Memnuniyet eğilim:</b> ...</p>
<p><b>Risk analizi:</b> ...</p>

<h4>🚀 Aksiyon & İletişim Stratejisi</h4>
<ul>
<li>Müşteriye yaklaşım:</li>
<li>Destek iletişim dili:</li>
<li>Müşteri bağlılığını artırma önerisi:</li>
</ul>

Veri: {jsonData}
";


            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _apiKey);

            var body = new
            {
                model = "gpt-4o-mini",
                messages = new[]
                {
                    new { role = "system", content = "You are an expert customer sentiment analyst." },
                    new { role = "user", content = prompt }
                },
                temperature = 0.5
            };

            var requestJson = JsonSerializer.Serialize(body);
            var content = new StringContent(requestJson, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var responseString = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(responseString);

            // ---------- SAFETY: API ERROR ------------
            if (doc.RootElement.TryGetProperty("error", out var error))
            {
                ViewBag.AnalysisSection1 = $"<p>AI Hatası: {error.GetProperty("message").GetString()}</p>";
                return View();
            }

            // ---------- SAFETY: CHOICES ------------
            if (!doc.RootElement.TryGetProperty("choices", out var choices) ||
                choices.GetArrayLength() == 0)
            {
                ViewBag.AnalysisSection1 = "<p>AI hiçbir sonuç döndürmedi.</p>";
                return View();
            }

            var choice = choices[0];

            if (!choice.TryGetProperty("message", out var message) ||
                !message.TryGetProperty("content", out var contentJson))
            {
                ViewBag.AnalysisSection1 = "<p>AI yanıt formatı geçersiz.</p>";
                return View();
            }

            var completion = contentJson.GetString() ?? "";

            // ---------- SAFETY: H4 YOKSA ---------------
            if (!completion.Contains("<h4>"))
            {
                ViewBag.AnalysisSection1 = "<p>AI beklenen formatta HTML döndürmedi.</p>";
                return View();
            }

            // ---------- HTML BÖLÜMLERİ ---------------
            var sections = completion.Split("<h4>");

            string SafeSection(int index)
            {
                return sections.Length > index
                    ? "<h4>" + sections[index]
                    : "<h4><p>Veri bulunamadı</p>";
            }

            ViewBag.AnalysisSection1 = SafeSection(1);
            ViewBag.AnalysisSection2 = SafeSection(2);
            ViewBag.AnalysisSection3 = SafeSection(3);
            ViewBag.AnalysisSection4 = SafeSection(4);
            ViewBag.AnalysisSection5 = SafeSection(5);
            ViewBag.AnalysisSection6 = SafeSection(6);

            return View();
        }
    }
}
