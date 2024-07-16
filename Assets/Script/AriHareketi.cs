using UnityEngine;

public class AriHareketi : MonoBehaviour
{
    public float ariHiz = 1f;
    public float ariHareket = 3f;
    public float ariBeklemeSuresi = 1f; // Arının üstte bekleyeceği süre

    private Vector3 baslangicPozisyonu; // Arının başlangıç pozisyonu
    private bool ariYon = true;
    private bool ariBekleme = false; // Arının bekleyip beklemeyeceğini belirleyen flag
    private float ariBeklemeZamani = 0f; // Arının beklemeye başladığı zaman

    void Start()
    {
        // Başlangıç pozisyonunu kaydet
        baslangicPozisyonu = transform.position;
    }

    void Update()
    {
        // Arının hareketi
        if (!ariBekleme)
        {
            if (ariYon)
            {
                transform.Translate(Vector3.right * ariHiz * Time.deltaTime);
                if (transform.position.x >= baslangicPozisyonu.x + ariHareket)
                {
                    ariYon = false;
                    ariBekleme = true; // Kuş üstte durma pozisyonuna ulaştığında beklemeye al
                    ariBeklemeZamani = Time.time; // Bekleme süresinin başlangıç zamanını kaydet
                }
            }
            else
            {
                transform.Translate(Vector3.left * ariHiz * Time.deltaTime);
                if (transform.position.x <= baslangicPozisyonu.x - ariHareket)
                {
                    ariYon = true;
                    ariBekleme = true; // Kuş aşağı inme pozisyonuna ulaştığında beklemeye al
                    ariBeklemeZamani = Time.time; // Bekleme süresinin başlangıç zamanını kaydet
                }
            }
        }
        else
        {
            // Kuş bekliyorsa
            if (Time.time - ariBeklemeZamani >= ariBeklemeSuresi)
            {
                // Bekleme süresi dolduğunda
                ariBekleme = false; // Bekleme durumunu kapat
            }
        }
    }
}
