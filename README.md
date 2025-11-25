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

Proje,  Murat Yücedağ hocanın Udemy platformundaki **Büyük Veri Analitiği & Veri Görselleştirme ve Tahminleme** eğitimindeki veri modeline göre geliştirilmiştir.

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

- Uygun İçerik
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
Tüm raporlar saf HTML formatında GPT-4o-mini tarafından oluşturulur.
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

## ⚙️ Kullanılan Teknolojiler

<p align="center">
  <img src="https://img.shields.io/badge/.NET%209.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/MSSQL-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white" />
  <img src="https://img.shields.io/badge/ML.NET-FF6B6B?style=for-the-badge" />
  <img src="https://img.shields.io/badge/HuggingFace%20ToxicBERT-FFCC00?style=for-the-badge&logo=huggingface&logoColor=white" />
  <img src="https://img.shields.io/badge/SignalR-0A66C2?style=for-the-badge" />
  <img src="https://img.shields.io/badge/Chart.js-FD3A5C?style=for-the-badge" />
  <img src="https://img.shields.io/badge/Leaflet%20Map-1C7C54?style=for-the-badge" />
</p>
---
---

## 📸 Ekran Görüntüleri

Aşağıda proje kapsamında paylaştığınız **tüm ekran görüntüleri** GitHub’da problemsiz çalışacak şekilde relative path formatında eklenmiştir.  
Resimler `wwwroot/BigDataDashboardSS/` altında bulunmalıdır.

---

## Dashboard
![Dashboard1](./BigDataDashboardSS/Dashboard1.png)
![Dashboard2](./BigDataDashboardSS/Dashboard2.png)
![Dashboard3](./BigDataDashboardSS/Dashboard3.png)
![Dashboard4](./BigDataDashboardSS/Dashboard4.png)
![Dashboard5](./BigDataDashboardSS/Dashboard5.png)

---

## Customer Analytics
![CustomerAnalytics](./BigDataDashboardSS/CustomerAnalytics.png)
![CustomerAnalytics1](./BigDataDashboardSS/CustomerAnalytics1.png)

---

## Customer Detail (AI Analysis)
![CustomerDetail](./BigDataDashboardSS/CustomerDetail.png)
![CustomerDetail1](./BigDataDashboardSS/CustomerDetail1.png)
![CustomerDetail2](./BigDataDashboardSS/CustomerDetail2.png)
![CustomerDetail3](./BigDataDashboardSS/CustomerDetail3.png)
![CustomerDetail4](./BigDataDashboardSS/CustomerDetail4.png)

---

## Customer Review – OpenAI Analysis
![CustomerReview1](./BigDataDashboardSS/CustomerReviewWithOpenAI.png)
![CustomerReview2](./BigDataDashboardSS/CustomerReviewWithOpenAI1.png)
![CustomerReview3](./BigDataDashboardSS/CustomerReviewWithOpenAI2.png)
![CustomerReview4](./BigDataDashboardSS/CustomerReviewWithOpenAI3.png)
![CustomerReview5](./BigDataDashboardSS/CustomerReviewWithOpenAI4.png)

---

## Orders
![Orders](./BigDataDashboardSS/Orders.png)

---

## Category
![Category](./BigDataDashboardSS/Category.png)

---

## Product
![Product](./BigDataDashboardSS/Product.png)

---

## Message List
![MessageList](./BigDataDashboardSS/MessageList.png)
![MessageList1](./BigDataDashboardSS/MessageList1.png)

---

## Reviews
![ReviewList](./BigDataDashboardSS/ReviewList.png)
![ReviewList1](./BigDataDashboardSS/ReviewList1.png)

---

## Payment Method Forecast
![PaymentMethodForecast](./BigDataDashboardSS/PaymentMethodForecast.png)

---

## ML.NET Tahminleri
![TurkeyCitiesForecast](./BigDataDashboardSS/TurkeyCitiesForecast.png)
![GermanyCitiesForecast](./BigDataDashboardSS/GermanyCitiesForecast.png)
![ItalyLoyaltyScore](./BigDataDashboardSS/ItalyLoyaltyScore.png)
![ItalyLoyaltyScore1](./BigDataDashboardSS/ItalyLoyaltyScore1.png)
![ItalyLoyaltyScoreWithML](./BigDataDashboardSS/ItalyLoyaltyScoreWithML.png)
![ItalyLoyaltyScoreWithML1](./BigDataDashboardSS/ItalyLoyaltyScoreWithML1.png)

---

## Statistics
![Statistics](./BigDataDashboardSS/Statistics.png)
![TextualStatistics](./BigDataDashboardSS/TextualStatistics.png)

# 👨‍💻 Geliştirici  
**Cevdet Karakulak**  
🧩 Full Stack Developer 
LinkedIn: https://www.linkedin.com/in/cevdet/

---

# 🪪 Lisans  
Bu proje **MIT Lisansı** ile paylaşılmıştır.
