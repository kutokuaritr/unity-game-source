using UnityEngine;
using UnityEngine.UI;


public class SandikYokEt : MonoBehaviour
{
    public Text YokEdilenSandikSkoru; // Metin elemanı için referans
    public Text YokEdilenSandikBilgisi; // Metin elemanı için referans

    public float yokEtmeMesafesi = 1f; // Sandıkları yok etme mesafesi

    private int zumrutSandikSayisi = 0;
    private int metalSandikSayisi = 0;
    private int altinSandikSayisi = 0;
    private int bakirSandikSayisi = 0;

    private void Update()
    {
        // Ekrana yazdırmak için sandık objelerini bulalım
        GameObject[] sandiklar = GameObject.FindGameObjectsWithTag("HazineSandik");

        // Sandıklar dizisini döngüye alıyoruz
        for (int i = 0; i < sandiklar.Length; i++)
        {
            // Karakter ile sandık arasındaki mesafeyi kontrol et
            if (Vector3.Distance(transform.position, sandiklar[i].transform.position) < yokEtmeMesafesi)
            {
                // Sandık yok edilmeden önce log oluştur
                Debug.Log("Sandık Yok Edildi: " + sandiklar[i].name + " Koordinatlar: " + sandiklar[i].transform.position);

                // Sandığı yok et
                Destroy(sandiklar[i]);

                // Sandık türüne göre skoru güncelle
                if (sandiklar[i].name.Contains("Zumrut"))
                {
                    zumrutSandikSayisi++;
                }
                else if (sandiklar[i].name.Contains("Metal"))
                {
                    metalSandikSayisi++;
                }
                else if (sandiklar[i].name.Contains("Altin"))
                {
                    altinSandikSayisi++;
                }
                else if (sandiklar[i].name.Contains("Bakir"))
                {
                    bakirSandikSayisi++;
                }

                string sandikAdi = sandiklar[i].name;
                Debug.Log("Önceki sandık adı: " + sandikAdi);
                sandikAdi = sandikAdi.Replace("(Clone)", "").Trim();
                Debug.Log("Düzeltilmiş sandık adı: " + sandikAdi);
                ShowNotification("Sandık yok edildi! " + sandikAdi + " Koordinatlar: " + " x: " + sandiklar[i].transform.position.x + " y: " + sandiklar[i].transform.position.y);

                UpdateNotificationText();


            }
        }
    }

    private void UpdateNotificationText()
    {
        // Metin elemanına sandık türlerine göre skoru yaz
        YokEdilenSandikSkoru.text = "Toplanan Sandıklar\n" +
                                  " Altın Sandık: " + altinSandikSayisi + "\n" +
                                 " Gümüş Sandık: " + metalSandikSayisi + "\n" +
                                 " Zümrüt Sandık: " + zumrutSandikSayisi + "\n" +
                                 " Bakır Sandık: " + bakirSandikSayisi;
    }

    private void ShowNotification(string message)
    {
        // Metni güncelle
        YokEdilenSandikBilgisi.text = message;

        // 3 saniye sonra metni temizle
        Invoke("ClearNotification", 3f);
    }

    private void ClearNotification()
    {
        // Metni temizle
        YokEdilenSandikBilgisi.text = "";
    }
}