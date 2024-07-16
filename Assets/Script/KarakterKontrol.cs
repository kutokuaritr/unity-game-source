using UnityEngine;
using System.Collections.Generic;

public class KarakterKontrol : MonoBehaviour
{
    public float hareketHizi = 0f;
    public float yokEtmeMesafesi = 1f; // Sandıkları yok etme mesafesi
    public GameObject oyunBittiPanel; // Oyun bitti paneli referansı
    public static float toplamMesafe = 0f;
    public Vector2 EngelYeniYon;

    private List<Vector2> yerDegisimleri = new List<Vector2>(); // Karakterin yer değişimlerini tutacak liste
    private Vector2 hedefPozisyon;
    private float minX, maxX, minY, maxY;

    private bool isCollided = false;

    private void Start()
    {
        minX = 0; // Oyun alanının en sol noktası
        maxX = 50; // Oyun alanının en sağ noktası
        minY = 0; // Oyun alanının en alt noktası
        maxY = 50; // Oyun alanının en üst noktası

        YeniHedefBelirle();
    }
    private bool xEksenindeHedefeUlasti = false; // Karakter x ekseninde hedefe ulaştı mı?
    private bool yEksenindeHedefeUlasti = false; // Karakter y ekseninde hedefe ulaştı mı?

    private void Update()
    {

        // Karakterin mevcut konumu
        Vector2 mevcutKonum = new Vector2(transform.position.x, transform.position.y);

        // Karakterin hedef pozisyonu
        Vector2 hedefPozisyon = new Vector2(this.hedefPozisyon.x, this.hedefPozisyon.y);

        if (isCollided == true)
        {
            hedefPozisyon = EngelYeniYon;
            isCollided = false;
            Debug.Log("isc-if: " + isCollided);
        }


        // Eğer karakter hedef pozisyona ulaşmadıysa, hedefe doğru hareket et
        if (!xEksenindeHedefeUlasti)
        {
            // X ekseninde hedefe doğru hareket et
            mevcutKonum.x = Mathf.MoveTowards(mevcutKonum.x, hedefPozisyon.x, hareketHizi * Time.deltaTime);

            // Eğer karakter x ekseninde hedefe ulaştıysa, bu durumu işaretle
            if (Mathf.Approximately(mevcutKonum.x, hedefPozisyon.x))
            {
                xEksenindeHedefeUlasti = true;
            }
        }
        else if (!yEksenindeHedefeUlasti)
        {
            // Y ekseninde hedefe doğru hareket et
            mevcutKonum.y = Mathf.MoveTowards(mevcutKonum.y, hedefPozisyon.y, hareketHizi * Time.deltaTime);

            // Eğer karakter y ekseninde hedefe ulaştıysa, bu durumu işaretle
            if (Mathf.Approximately(mevcutKonum.y, hedefPozisyon.y))
            {
                yEksenindeHedefeUlasti = true;
            }
        }

        // Yeni pozisyonu uygula
        transform.position = mevcutKonum;

        // Eğer karakter hem x hem de y ekseninde hedefe ulaştıysa, yeni hedef belirle
        if (xEksenindeHedefeUlasti && yEksenindeHedefeUlasti)
        {
            YeniHedefBelirle();
            xEksenindeHedefeUlasti = false; // X ekseninde yeni hedefe gitmeye başlayacağız
            yEksenindeHedefeUlasti = false; // Y ekseninde yeni hedefe gitmeye başlayacağız
        }

       

        // Karakterin yer değişimlerini kaydet
        yerDegisimleri.Add(new Vector2(transform.position.x, transform.position.y));
    }



    private void YeniHedefBelirle()
    {
        GameObject enYakinSandik = EnYakinSandigiBul();
        if (enYakinSandik != null)
        {
            hedefPozisyon = enYakinSandik.transform.position;
            hedefPozisyon = enYakinSandik.transform.position;
        }
        else
        {
            OyunuBitir();
        }
    }

    

    private GameObject EnYakinSandigiBul()
    {
        GameObject[] sandiklar = GameObject.FindGameObjectsWithTag("HazineSandik");
        GameObject enYakinSandik = null;
        float enKisaMesafe = Mathf.Infinity;
        Vector2 karakterPozisyon = transform.position;

        foreach (GameObject sandik in sandiklar)
        {
            Vector2 sandikPozisyon = sandik.transform.position;
            float mesafe = Vector2.Distance(karakterPozisyon, sandikPozisyon);

            if (mesafe < enKisaMesafe)
            {
                enYakinSandik = sandik;
                enKisaMesafe = mesafe;
            }
        }

        return enYakinSandik;
    }

    private void OyunuBitir()
    {
        // Yer değişimlerini kullanarak toplam mesafeyi hesapla
        for (int i = 1; i < yerDegisimleri.Count; i++)
        {
            toplamMesafe += Vector2.Distance(yerDegisimleri[i - 1], yerDegisimleri[i]);
        }

        // Toplam mesafeyi PlayerPrefs ile kaydet
        PlayerPrefs.SetFloat("ToplamMesafe", toplamMesafe);

        // Oyun bitti panelini etkinleştir
        oyunBittiPanel.SetActive(true);

        // Oyunu durdur
        Time.timeScale = 0f;
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SabitNesme"))
        {
            EngelYeniYonF(collision.transform);
             isCollided = true;
            Debug.Log("isc-OnCollisionEnter2D " + isCollided);

        }
    }

    private void EngelYeniYonF(Transform Engel)
    {
        EngelYeniYon = new Vector2(Engel.transform.position.x+15, Engel.transform.position.y-15);
    }
}