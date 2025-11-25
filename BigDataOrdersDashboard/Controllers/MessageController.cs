using BigDataOrdersDashboard.Context;
using BigDataOrdersDashboard.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BigDataOrdersDashboard.Controllers
{
    public class MessageController : Controller
    {
        private readonly BigDataOrdersDbContext _context;
        private readonly IConfiguration _configuration;

        public MessageController(BigDataOrdersDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public IActionResult MessageList(int page = 1)
        {
            int pageSize = 12;
            var values = _context.Messages
                                 .OrderBy(p => p.MessageId)
                                 .Skip((page - 1) * pageSize)
                                 .Take(pageSize)
                                 .Include(y => y.Customer)
                                 .ToList();

            int totalCount = _context.Messages.Count();
            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            ViewBag.CurrentPage = page;

            return View(values);
        }

        public IActionResult GetMessageDetail(int id)
        {
            var message = _context.Messages
                .Include(x => x.Customer)
                .FirstOrDefault(x => x.MessageId == id);

            if (message == null)
                return Json(new { success = false });

            return Json(new
            {
                success = true,
                messageId = message.MessageId,
                sender = $"{message.Customer.CustomerName} {message.Customer.CustomerSurname}",
                subject = message.MessageSubject,
                content = message.MessageText,
                date = message.CreatedDate.ToString("dd MMM yyyy HH:mm"),
                sentiment = message.SentimentLabel
            });
        }

        [HttpGet]
        public IActionResult CreateMessage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(Message message)
        {
            // Secrets.json'dan API KEY ve model adresi çekiliyor
            var apiKey = _configuration["HuggingFace:ApiKey"];
            var toxicModelUrl = _configuration["HuggingFace:ModelUrl"];
            var translateModelUrl = "https://router.huggingface.co/hf-inference/models/Helsinki-NLP/opus-mt-tr-en";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            client.DefaultRequestHeaders.Add("User-Agent", "EfeApp/1.0");
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            try
            {
                // 1) Türkçe → İngilizce Çeviri
                var translateRequest = new { inputs = message.MessageText };
                var translateJson = JsonSerializer.Serialize(translateRequest);
                var translateContent = new StringContent(translateJson, Encoding.UTF8, "application/json");

                var translateResponse = await client.PostAsync(translateModelUrl, translateContent);
                var translateString = await translateResponse.Content.ReadAsStringAsync();

                string englishText = message.MessageText;

                if (translateString.TrimStart().StartsWith("["))
                {
                    var doc = JsonDocument.Parse(translateString);
                    englishText = doc.RootElement[0].GetProperty("translation_text").GetString();
                }

                // 2) Toksiklik Analizi
                var toxicRequest = new { inputs = englishText };
                var toxicJson = JsonSerializer.Serialize(toxicRequest);
                var toxicContent = new StringContent(toxicJson, Encoding.UTF8, "application/json");

                var toxicResponse = await client.PostAsync(toxicModelUrl, toxicContent);
                var toxicString = await toxicResponse.Content.ReadAsStringAsync();

                if (toxicString.TrimStart().StartsWith("["))
                {
                    var toxicDoc = JsonDocument.Parse(toxicString);
                    foreach (var item in toxicDoc.RootElement[0].EnumerateArray())
                    {
                        double score = item.GetProperty("score").GetDouble();

                        if (score > 0.5)
                        {
                            message.SentimentLabel = "Toksik İçerik";
                            break;
                        }
                        else
                        {
                            message.SentimentLabel = "Uygun İçerik";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message.SentimentLabel = "Hata Oluştu: " + ex.Message;
            }

            message.CreatedDate = DateTime.Now;
            _context.Messages.Add(message);
            _context.SaveChanges();

            return RedirectToAction("MessageList");
        }

        public IActionResult DeleteMessage(int id)
        {
            var value = _context.Messages.Find(id);
            _context.Messages.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("MessageList");
        }

        [HttpGet]
        public IActionResult UpdateMessage(int id)
        {
            var value = _context.Messages.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateMessage(Message message)
        {
            var existing = _context.Messages.Find(message.MessageId);

            if (existing == null)
                return NotFound();

            // Sadece güncellenebilir alanlar güncellensin
            existing.CustomerId = message.CustomerId;
            existing.MessageSubject = message.MessageSubject;
            existing.MessageText = message.MessageText;

            // SentimentLabel değiştirilmesin (toksiklik kontrolü sadece eklemede var)
            // existing.SentimentLabel olduğu gibi kalsın

            // CreatedDate asla değiştirilmesin (update formunda yok!)
            // existing.CreatedDate aynı kalsın

            _context.SaveChanges();
            return RedirectToAction("MessageList");
        }
    }
}
