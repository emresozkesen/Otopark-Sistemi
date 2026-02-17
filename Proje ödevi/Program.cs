using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.IO;
 interface IFarklıUcretlendirme
 {
     double FarklıUcret(Arac arac);
 }
 class GunlukUcret : IFarklıUcretlendirme
 {
     public double FarklıUcret(Arac arac)
     {
       return 100;
     }
 }

  class AbomanUcret : IFarklıUcretlendirme
  {
       public double FarklıUcret(Arac arac)
       {
       return 50;
       } 
  }

  class FarklıUcretlendirme
  {
     private IFarklıUcretlendirme frkuc;

     public FarklıUcretlendirme(IFarklıUcretlendirme frkuc)
     {
       this.frkuc = frkuc;
     }
     public double UcretHesapla(Arac arac)
     {
       return frkuc.FarklıUcret(arac);
     }

  }
   abstract class Arac
   {
     public int saat { get; set; }
    public abstract double UcretHesapla();
    public abstract double ParkAlanGereksinimi();
   }

  class Otomobil : Arac
  {
    public override double UcretHesapla() => saat * 10;
    public override double ParkAlanGereksinimi() => 2;

  }


   class Motorsiklet : Arac
   {
    public override double UcretHesapla() => saat * 5;
    public override double ParkAlanGereksinimi() => 1;

   }

    class Minibus : Arac
    {
     public override double UcretHesapla() => saat * 20;
     public override double ParkAlanGereksinimi() => 3;
    }
    class Tır : Arac
    {
     public override double UcretHesapla() => saat * 30;
     public override double ParkAlanGereksinimi() => 5;
     }
     class UcretHesaplayıcı
     {
       public double UcretHesapla(Arac arac)
       {
        return arac.UcretHesapla();
       }
       public double ParkAlanHesapla(Arac arac)
       {
        return arac.ParkAlanGereksinimi();
       }

     }

class Otopark
{


    private Parkyeri[] Parkyerleri;
    public static int SonID = 1;
    public Otopark()
    {
        Parkyerleri = new Parkyeri[10];
    }
    public Otopark(int lenght)
    {
        Parkyerleri = new Parkyeri[lenght];
    }
    public int Kapasite
    {
        get => Parkyerleri.Length;
    }
    public Parkyeri this[int index]
    {
        get
        {
            if (index < 0 || index >= Kapasite)
                throw new Exception("Index out of range");
            return Parkyerleri[index];
        }
        set
        {
            if (index < 0 || index >= Kapasite)
                throw new Exception("Index out of range");
            Parkyerleri[index] = value;
        }
    }
    public int BosParkAlanBul()
    {
        for (int i = 0; i < Parkyerleri.Length; i++)
        {
            if (Parkyerleri[i] == null) return i;
        }
        return -1;
    }

    public int DoluParkSayisi()
    {
        int dolu = 0;

        for (int i = 0; i < Parkyerleri.Length; i++)
        {
            if (Parkyerleri[i] != null)
                dolu++;
        }

        return dolu;
    }
    public int Dolulukorani()
    {
        int dolulukorani = (int)((double)DoluParkSayisi() / Kapasite * 100);
        return dolulukorani;
    }

    public void ParkYerleriniVeToplamSatiriOku()
    {
        if (!File.Exists("LogKayit.txt"))
            return;

        string[] loglar = File.ReadAllLines("LogKayit.txt");
        for (int i = 0; i < Kapasite; i++)
            Parkyerleri[i] = null;

        int parkIndex = 0;
        foreach (string logSatir in loglar)
        {
            if (parkIndex >= Kapasite)
                break; 

            if (logSatir.Contains("ÇIKIŞ YAPILDI"))
                continue; 

            Parkyerleri[parkIndex] = new Parkyeri(); 
            parkIndex++;
        }     
    }
}

public class Parkyeri
  {
     public int ID;
  }

 class Program
 {
    static void Main()
    {

       Otopark db = new Otopark(10);
       db.ParkYerleriniVeToplamSatiriOku();
       int Otomobildoluluk = 4;
       int Motorsikletdoluluk = 3;
       int Minibüsdoluluk = 2;
       int Tırdoluluk = 1;
       int toplamDoluluk = 0;
        
       if (File.Exists("LogKayit.txt"))
       {
           string[] loglar = File.ReadAllLines("LogKayit.txt");
           foreach (var log in loglar)
           {
              if (!log.Contains("ÇIKIŞ YAPILDI")) toplamDoluluk++;
           }
       }

       Console.WriteLine("--------------------------------");
       Console.WriteLine("  Otoparkımıza Hoşgeldiniz !!!! ");
       Console.WriteLine("--------------------------------");
       Console.WriteLine(" Randevu yapmak ister misiniz ? ");
       string randevu = Console.ReadLine();
       if (randevu.ToLower() == "evet")
       {
           Console.WriteLine("Hangi tur araç ile geldiniz?(motorsiklet/tır/otomobil/minibüs)");
           string cevap = Console.ReadLine();

           Console.WriteLine("Giriş saatini tarih ve saat olarak girer misiniz(örn:15/02/2025 14:00)?");
           DateTime girisSaati = DateTime.Parse(Console.ReadLine());

           Console.WriteLine("Çıkış saatini tarih ve saat olarak girer misiniz(örn:15/02/2025 14:00)?");
           DateTime cıkışSaati = DateTime.Parse(Console.ReadLine());

           TimeSpan durdugusaat = cıkışSaati - girisSaati;
           int toplamSaat = (int)durdugusaat.TotalHours;

           Arac arac;
           FarklıUcretlendirme ucretlendirme = null;
            switch (cevap.ToLower())
            {
               case "otomobil":
                   Otomobildoluluk--;
                   arac = new Otomobil();
                   break;
               case "motorsiklet":
                   Motorsikletdoluluk--;
                   arac = new Motorsiklet();
                   break;
               case "minibüs":
                   Minibüsdoluluk--;
                   arac = new Minibus();
                   ucretlendirme = new FarklıUcretlendirme(new GunlukUcret());
                   break;
               case "tır":
                   Tırdoluluk--;
                   arac = new Tır();
                   ucretlendirme = new FarklıUcretlendirme(new AbomanUcret());
                   break;
               default:
                   Console.WriteLine("Geçersiz araç türü!");
                   return;
            }


           int bosPark = db.BosParkAlanBul();
              if (bosPark == -1)
              {
               Console.WriteLine("Üzgünüz, park alanı dolu!");
               return;
              }
               else
               {
                 if (File.Exists("LogKayit.txt"))
                 {
                   Otopark.SonID = File.ReadAllLines("LogKayit.txt").Length + 1;
                 }
                  else
                 {
                   Otopark.SonID = 1;
                 }

               db[bosPark] = new Parkyeri { ID = Otopark.SonID };
               toplamDoluluk++;
               Console.WriteLine($"{cevap} aracınız {bosPark + 1}. park alanına yerleştirildi. (ID: {Otopark.SonID})");
               Otopark.SonID++;
           }

           arac.saat = toplamSaat;
           double ucret;

               if (ucretlendirme != null)
               ucret = ucretlendirme.UcretHesapla(arac); 

               else
               ucret = new UcretHesaplayıcı().UcretHesapla(arac); 


           Console.WriteLine($"Id:{db[bosPark].ID} ;  giriş saati:{girisSaati}; çıkış saati:{cıkışSaati};ücret :{ucret};");
           Console.WriteLine("\n Rezervasyonunuz başarıyla alındı.");


           File.AppendAllText("LogKayit.txt",
            $"Id :{db[bosPark].ID}| Tarih: {DateTime.Now} | Tür: {cevap} | Giriş: {girisSaati} | Çıkış: {cıkışSaati} " +
            $"| Ücret: {ucret} | Durum: Randevulu\n");



       }

           Console.WriteLine("Randevunuz var mıydı (evet/hayır) ?");
           string soru = Console.ReadLine();

       if (soru.ToLower() == "hayır")
       {
           Console.WriteLine("Hangi tur araç ile geldiniz?(motorsiklet/tır/otomobil/minibüs)");
           string geldigiarac = Console.ReadLine();

           Console.WriteLine("Giriş saatini tarih ve saat olarak girer misiniz(örn:15/02/2025 14:00) ?");
           DateTime girissaati = DateTime.Parse(Console.ReadLine());
           Console.WriteLine(girissaati);

           Console.WriteLine("Çıkış saatini tarih ve saat olarak girer misiniz(örn:15/02/2025 14:00)?");
           DateTime cıkışsaati = DateTime.Parse(Console.ReadLine());
           Console.WriteLine(cıkışsaati);

           TimeSpan durdugusaat = cıkışsaati - girissaati;
           int toplamsaat = (int)durdugusaat.TotalHours;

           Console.WriteLine("Rapor ister misiniz ?");
           string isteme = Console.ReadLine();

           Arac arac;
            switch (geldigiarac.ToLower())
            {
               case "otomobil":
                   Otomobildoluluk--;
                   arac = new Otomobil();
                   break;
               case "motorsiklet":
                   Motorsikletdoluluk--;
                   arac = new Motorsiklet();
                   break;
               case "minibüs":
                   Minibüsdoluluk--;
                   arac = new Minibus();
                   break;
               case "tır":
                   Tırdoluluk--;
                   arac = new Tır();
                   break;
               default:
                   Console.WriteLine("Geçersiz araç türü!");
                   return;
            }

           int bosPark;

             if (File.Exists("LogKayit.txt"))
             {
                 bosPark = File.ReadAllLines("LogKayit.txt").Length;
             }
             else
             {
                 bosPark = 0;
             }
            
            if (bosPark == -1)
              {
               Console.WriteLine("Üzgünüz, park alanı dolu!");
               return;
              }
                     
              else
              {
                if (File.Exists("LogKayit.txt"))
                {
                   Otopark.SonID = File.ReadAllLines("LogKayit.txt").Length + 1;
                }
                else
                {
                   Otopark.SonID = 1;
                }


                db[bosPark] = new Parkyeri { ID = Otopark.SonID };
                toplamDoluluk++;
                int dolulukYuzdesi = (toplamDoluluk * 100) / db.Kapasite;
                Console.WriteLine("Doluluk oranı: %" + dolulukYuzdesi);
                Console.WriteLine($"{geldigiarac} aracı {bosPark + 1}. park alanına yerleştirildi. (ID: {Otopark.SonID})");
                Otopark.SonID++;


              }

               arac.saat = toplamsaat;
               UcretHesaplayıcı ucretHesaplayici = new UcretHesaplayıcı();
               double ucret = ucretHesaplayici.UcretHesapla(arac);
               int dolulukyüzdesi = db.Dolulukorani();

           if (isteme.ToLower() == "evet")
           {
               Console.WriteLine("Bu aracın ID'si: " + db[bosPark].ID);
               Console.WriteLine(girissaati);
               Console.WriteLine(cıkışsaati);
               Console.WriteLine(durdugusaat);
               Console.WriteLine("ücret :" + ucret);

           }

             File.AppendAllText("LogKayit.txt",
             $"Id:{db[bosPark].ID}|Tarih: {DateTime.Now} | Tür: {geldigiarac} | Giriş: {girissaati} | Çıkış: {cıkışsaati} |" +
             $" Ücret: {ucret} | Durum: Randevusuz\n");




       }

           else if (soru.ToLower() == "evet")
           {
           Console.WriteLine("-----------------------------");
           Console.WriteLine(" Otoparkamıza Hoşgeldiniz !!!");
           Console.WriteLine("-----------------------------");
           Console.WriteLine("ID nizi söyler misiniz(örn:3)?");
              int dbId = int.Parse(Console.ReadLine());
            }
              Console.WriteLine("Araç çıkışı yapmak ister misiniz? (evet/hayır)");
              string cikis = Console.ReadLine();

       if (cikis.ToLower() == "evet")
       {
            Console.WriteLine("Çıkış yapacak aracın ID'sini giriniz:");
            int cikisId = int.Parse(Console.ReadLine());
           if (File.Exists("LogKayit.txt"))
           {
               string[] loglar = File.ReadAllLines("LogKayit.txt");
               bool bulundu = false;

               for (int i = 0; i < loglar.Length; i++)
               {
                   string[] parts = loglar[i].Split('|');
                   if (parts.Length > 0)
                   {
                       string idPart = parts[0]; 
                       int logId = int.Parse(idPart.Split(':')[1].Trim());

                       if (logId == cikisId)
                       {
                           Console.WriteLine("Araç bilgileri:");
                           Console.WriteLine(loglar[i]);

                           loglar[i] += " | ÇIKIŞ YAPILDI";
                           toplamDoluluk--;
                           bulundu = true;
                           break;
                       }
                   }
               }

               File.WriteAllLines("LogKayit.txt", loglar);
               if (bulundu)
                   Console.WriteLine("Araç çıkışı başarıyla yapıldı.");
               else
                   Console.WriteLine("Bu ID'ye ait kayıt bulunamadı.");
           }
           else
           {
               Console.WriteLine("Log dosyası bulunamadı.");
           }
       }

          Console.WriteLine("\n--- Kayıtlı Loglar ---");
          if (File.Exists("LogKayit.txt"))
          {
           string[] loglar = File.ReadAllLines("LogKayit.txt");
           foreach (var log in loglar)
           Console.WriteLine(log);
          }
          else
          {
             Console.WriteLine("Henüz kayıtlı log bulunmuyor.");
          }

             Console.WriteLine("Toplam gelir :" + LogdanToplamGelir());
    }

       static double LogdanToplamGelir()
       {
          double toplam = 0;

         if (!File.Exists("LogKayit.txt"))
            return 0;

         string[] loglar = File.ReadAllLines("LogKayit.txt");

        foreach (string log in loglar)
         {
           if (log.Contains("Ücret:"))
           {
               string[] parcalar = log.Split('|');
               foreach (string parca in parcalar)
               {
                   if (parca.Contains("Ücret:"))
                   {
  
                     toplam += double.Parse(parca.Replace("Ücret:", "").Trim());
                   }
               }
           }
         }
       return toplam;
       }

 }










