using UnityEngine;

public class KusHareketi : MonoBehaviour
{
    public float kusHiz = 1f;
    public float kusYukseklik = 5f;
    public float kusBeklemeSuresi = 1f; // Kuşun üstte bekleyeceği süre

    private Vector3 baslangicPozisyonu; // Kuşun başlangıç pozisyonu
    private bool kusYukari = true;
    private bool kusBekleme = false; // Kuşun bekleyip beklemeyeceğini belirleyen flag
    private float kusBeklemeZamani = 0f; // Kuşun beklemeye başladığı zaman

    void Start()
    {
        // Başlangıç pozisyonunu kaydet
        baslangicPozisyonu = transform.position;
    }

    void Update()
    {
        // Kuşun hareketi
        if (!kusBekleme)
        {
            if (kusYukari)
            {
                transform.Translate(Vector3.up * kusHiz * Time.deltaTime);
                if (transform.position.y >= baslangicPozisyonu.y + kusYukseklik)
                {
                    kusYukari = false;
                    kusBekleme = true; // Kuş üstte durma pozisyonuna ulaştığında beklemeye al
                    kusBeklemeZamani = Time.time; // Bekleme süresinin başlangıç zamanını kaydet
                }
            }
            else
            {
                transform.Translate(Vector3.down * kusHiz * Time.deltaTime);
                if (transform.position.y <= baslangicPozisyonu.y - kusYukseklik)
                {
                    kusYukari = true;
                    kusBekleme = true; // Kuş aşağı inme pozisyonuna ulaştığında beklemeye al
                    kusBeklemeZamani = Time.time; // Bekleme süresinin başlangıç zamanını kaydet
                }
            }
        }
        else
        {
            // Kuş bekliyorsa
            if (Time.time - kusBeklemeZamani >= kusBeklemeSuresi)
            {
                // Bekleme süresi dolduğunda
                kusBekleme = false; // Bekleme durumunu kapat
            }
        }
    }
}
