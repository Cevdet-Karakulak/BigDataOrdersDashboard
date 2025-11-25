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
  <img src="https://img.shields.io/badge/Chart.js-FD3A5C?style=for-the-badge" />
  <img src="https://img.shields.io/badge/Leaflet%20Map-1C7C54?style=for-the-badge" />
</p>
---

## 📸 Ekran Görüntüleri

### 📊 Dashboard  
![Dashboard](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/Dashboard1.png)
![Dashboard1](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/Dashboard2.png)
![Dashboard1](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/Dashboard3.png)
![Dashboard1](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/Dashboard4.png)
![Dashboard1](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/Dashboard5.png)

### 👤 Müşteri  
![Customer](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/CustomerList.png)
![Customer](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/CustomerDetail1.png)
![Customer](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/CustomerDetail2.png)
![Customer](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/CustomerDetail3.png)
![Customer](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/CustomerDetail4.png)

### 🧠 Müşteri Analitiği  
![CustomerAnalytics](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/CustomerAnalytics.png)
![CustomerAnalytics](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/CustomerAnalytics1.png)

### 🤖 AI – HuggingFace Toxic Review  
![CustomerReviewWithOpenAI](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/CustomerReviewWithOpenAI.png)
![CustomerReviewWithOpenAI](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/CustomerReviewWithOpenAI1.png)
![CustomerReviewWithOpenAI](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/CustomerReviewWithOpenAI3.png)
![CustomerReviewWithOpenAI](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/CustomerReviewWithOpenAI4.png)

### 📝 Kategoriler
![Category](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/Category.png)

### 📦 Ürünler  
![Product](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/Product.png)

### 🧾 Siparişler  
![Orders](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/Orders.png)

### 📝 Yorumlar  
![ReviewList](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/ReviewList.png)
![ReviewList](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/ReviewList1.png)


### 💬 Mesajlaşma 
![MessageList](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/MessageList.png)
![MessageList](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/MessageList1.png)

### 📈 ML.NET Tahminleri  
![TurkeyCitiesForecast](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/TurkeyCitiesForecast.png)
![GermanyCitiesForecast](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/GermanyCitiesForecast.png)
![PaymentMethodForecast](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/PaymentMethodForecast.png)
![ItalyLoyaltyScore](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/ItalyLoyaltyScore.png)
![ItalyLoyaltyScore](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/ItalyLoyaltyScore1.png)
![ItalyLoyaltyScoreWithML](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/ItalyLoyaltyScoreWithML.png)
![ItalyLoyaltyScoreWithML](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/ItalyLoyaltyScoreWithML1.png)

### 📊 Sayısal & Metinsel İstatistikler  
![Statistics](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/Statistics.png)
![TextualStatistics.png](https://raw.githubusercontent.com/Cevdet-Karakulak/BigDataOrdersDashboard/master/BigDataOrdersDashboard/wwwroot/BigDataDashboardSS/TextualStatistics.png)


# 👨‍💻 Geliştirici  
**Cevdet Karakulak**  
🧩 Full Stack Developer 
LinkedIn: https://www.linkedin.com/in/cevdet/

---

# 🪪 Lisans  
Bu proje **MIT Lisansı** ile paylaşılmıştır.
