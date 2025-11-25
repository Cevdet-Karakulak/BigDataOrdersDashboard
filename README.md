# 📊 BigDataOrdersDashboard  
### **Büyük Veri Analitiği, Veri Görselleştirme ve ML.NET ile Tahminleme Platformu (ASP.NET Core 9 + MSSQL + AI + NLP + ML.NET)**

## 🚀 Proje Hakkında

**BigDataOrdersDashboard**, gerçek bir e-ticaret şirketinin veri analitiği ve yapay zekâ süreçlerini uçtan uca modellemek için geliştirilmiş bir **E-Commerce Data Analytics & Forecasting Platform** projesidir.

Bu proje kapsamında analiz edilen veri seti:

- **500.000+ sipariş**
- **1500+ müşteri**
- **18.000+ ürün**
- **78 şehir**
- **13 ülke**

Proje, Udemy platformundaki **Büyük Veri Analitiği & Veri Görselleştirme ve Tahminleme** eğitimindeki veri modeline göre geliştirilmiştir.

---

# 🧠 Öne Çıkan Özellikler

## 1️⃣ ML.NET Tahmin Modelleri (7 Adet)
Projede toplam **7 farklı ML.NET tahmin modeli** bulunmaktadır:

| Model | Açıklama |
|-------|----------|
| **PaymentMethodForecast** | 2026 için ödeme yöntemi dağılımı tahmini |
| **GermanyCitiesForecast** | Almanya şehirlerine göre sipariş tahmini |
| **TurkeyCitiesForecast** | Türkiye şehir bazlı sipariş tahmini |
| **ItalyLoyaltyScore** | İtalya müşteri sadakat skoru |
| **TurkeyLoyaltyScore** | Türkiye müşteri sadakat skoru |
| **ItalyLoyaltyScoreWithML** | ML tabanlı sadakat tahmin modeli |
| **CustomerReviewWithOpenAI** | AI destekli yorum analizi |

---

## 2️⃣ HuggingFace AI Entegrasyonu (ToxicBERT)
Müşteri yorumları:

1. İngilizceye çevrilir,  
2. HuggingFace **ToxicBERT** modelinden geçirilir,  
3. Şu kategorilerde sınıflandırılır:

- Temiz / Uygun
- Nötr
- Olumsuz
- **Toksik içerik**

Sonuçlar arayüzde renk kodlaması ile gösterilir.

---

## 3️⃣ OpenAI GPT-4o-mini ile Müşteri Analitiği
Her müşteri için GPT tabanlı dinamik analiz oluşturulur:

- Müşteri profili  
- Ürün tercihleri  
- Sadakat eğilimi  
- Harcama analizi  
- Zaman bazlı davranış  
- Kampanya / pazarlama önerileri  
- Risk analizi  

---

## 4️⃣ NLP + GPT ile Yorum Bazlı Analiz

- Genel tutum  
- Duygu & ton analizi  
- Karakter analizi  
- Şikâyet & övgü temaları  
- Davranış trendi  
- Aksiyon & iletişim önerileri  

Tamamen otomatik AI raporu çıkar.

---

## 5️⃣ Dashboard Özellikleri

- Son 6 aylık sipariş grafikleri  
- Günlük sipariş takibi  
- Ülke & şehir bazlı dağılımlar  
- Ödeme yöntemleri istatistikleri  
- Stok kritik ürünler  
- Son yorumlar  
- Müşteri hareketleri  

---

## 6️⃣ Leaflet ile Coğrafi Analiz

- Ülke & şehir bazlı sipariş yoğunluğu  
- Ortalama sipariş tutarı  
- En popüler kategori  
- Yoğunluğa göre renk ölçeklendirme  

---

## 7️⃣ Loglama Sistemi

- Kullanıcı işlemleri  
- IP – Tarih – Entity – İşlem türü  
- Log ekranında yönetilebilir listeleme  

---

# 🏗️ Kullanılan Teknolojiler

| Teknoloji | Açıklama |
|-----------|----------|
| **ASP.NET Core 9 MVC** | Backend |
| **MSSQL** | Veritabanı |
| **Entity Framework Core** | ORM |
| **ML.NET** | Tahminleme |
| **OpenAI GPT-4o-mini** | Analiz & Raporlama |
| **HuggingFace ToxicBERT** | NLP – Yorum analizi |
| **Chart.js** | Grafikler |
| **Leaflet.js** | Harita |
| **SignalR** | Gerçek zamanlı iletişim |
| **Bootstrap 5** | UI |

---

# 👨‍💻 Geliştirici  
**Cevdet Karakulak**  
GitHub: https://github.com/Cevdet-Karakulak  
LinkedIn: https://www.linkedin.com/in/cevdet/

---

# 🪪 Lisans  
Bu proje **MIT Lisansı** ile paylaşılmıştır.
