# Otopark Yönetim Konsol Uygulaması

Bu proje, konsol tabanlı basit bir otopark yönetim uygulamasıdır. Araç giriş/çıkışlarını kaydeder, ücret hesaplar ve log dosyasında saklar.

Öne çıkan özellikler
- Araç türlerine göre ücretlendirme (otomobil, motorsiklet, minibüs, tır)
- Randevulu / randevusuz giriş seçenekleri
- Park yeri yönetimi (kapasite, boş alan bulma, doluluk oranı)
- Log kaydı: `LogKayit.txt` dosyası ile kayıt ve çıkış takibi
- Toplam gelir hesaplama

Proje dosyaları
- `Program.cs` : Tüm uygulama mantığının bulunduğu ana dosya
- `Arac.cs` : Araç sınıfları (açık olarak workspace'te bulunuyor)
- `LogKayit.txt` : Uygulama çalışırken oluşturulan kayıt dosyası (yoksa uygulama oluşturur)

Proje Yapısı
------------
Aşağıda proje klasörünün tipik bir yapısı gösterilmiştir. Dosya/klasör adları sizin çalışma dizininize göre farklılık gösterebilir:

```
ProjeÖdevi/                    # Proje kök dizini
├─ ProjeAdi.csproj             # .NET proje dosyası (ör: Otopark.csproj)
├─ Program.cs                  # Uygulama giriş noktası ve iş mantığı
├─ Arac.cs                     # Araç soyut sınıfı ve türevleri
├─ README.md                   # Bu dosya
├─ LogKayit.txt                # Çalışma sırasında oluşturulan log dosyası
├─ bin/                        # Derleme çıktıları
└─ obj/                        # Derleme ara dosyaları
```

Not: Projeyi daha modüler hale getirmek için `Models`, `Services` veya `Data` gibi klasörler oluşturarak `Arac`, `Otopark`, `Parkyeri`, `Ucretlendirme` gibi sınıfları ayrı dosyalara taşıyabilirsiniz.

Gereksinimler
- .NET 8 SDK
- C# 12 hedefi (Visual Studio 2022+ veya Visual Studio 2026 kullanabilirsiniz)

2. Uygulamayı çalıştırmak için:
   `dotnet run`

Notlar ve geliştirme önerileri
- `Program.cs` içinde uygulama mantığı tek dosyada yoğunlaşıyor; kodu `Arac`, `Otopark`, `Parkyeri`, `Ucretlendirme` gibi ayrı sınıf dosyalarına bölmek bakımı kolaylaştırır.
- `LogKayit.txt` içeriği parse edilirken hataya açık noktalar var; log formatını standartlaştırmak ve hata kontrolleri eklemek tavsiye edilir.
- Tarih/saat parse işlemlerinde kullanıcı hatalarına karşı `DateTime.TryParse` kullanılabilir.

İletişim
- Bu README dosyası proje içinde otomatik olarak eklendi.

Kullanım (Örnek akışlar)
-----------------------
1) Uygulamayı başlatma
   - Proje klasöründe `dotnet run` komutunu çalıştırın.
   - Konsolda uygulama yönergeleri görünür.

2) Randevu ile giriş (örnek)
   - "evet" yazarak randevu yapmak istediğinizi onaylayın.
   - Araç türünü girin: `otomobil`, `motorsiklet`, `minibüs` veya `tır`.
   - Giriş ve çıkış tarih/saatlerini `gg/aa/yyyy ss:dd` formatında girin (örn: `15/02/2025 14:00`).
   - Uygulama ücret hesaplayıp `LogKayit.txt` dosyasına `Randevulu` durumuyla kaydeder.

3) Randevusuz giriş (örnek)
   - "hayır" yazarak randevusuz geldiğinizi belirtin.
   - Araç türünü girin ve giriş/çıkış saatlerini aynı formatta girin.
   - İsterseniz rapor (evet/hayır) sorusuna `evet` yazarak ücret ve saat bilgisini ekranda görebilirsiniz.
   - Kayıt `Randevusuz` durumuyla `LogKayit.txt` dosyasına eklenir.

4) Araç çıkışı
   - Uygulama sizden çıkış yapmak isteyip istemediğinizi sorar. `evet` seçin.
   - Çıkış yapacak aracın `ID` numarasını girin (log dosyasındaki `Id` değerini kullanın).
   - İlgili log satırına `| ÇIKIŞ YAPILDI` eklenir ve doluluk azaltılır.

5) Logları ve toplam geliri görüntüleme
   - Uygulama sonunda kayıtlı tüm loglar ekrana yazdırılır.
   - Ayrıca `LogdanToplamGelir()` fonksiyonu ile `LogKayit.txt` içindeki ücretlerin toplamı hesaplanıp gösterilir.

İpuçları
- `LogKayit.txt` dosya formatı manuel düzenlenirse uygulama hatası oluşabilir; düzenlemeden önce yedekleyin.
- Tarih/saat girişleri hatalıysa uygulama çökebilir; kullanıcı girdileri için `DateTime.TryParse` eklenmesi tavsiye edilir.
