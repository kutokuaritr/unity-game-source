using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Takip edilecek karakterin referansı
    private GameObject karakter; // Klonlanan karakterin referansı
    public string klonlananKarakterAdi = "karakter(Clone)";

    public void Start()
    {
        // Klonlanan karakteri bul ve referansını al
        karakter = GameObject.Find(klonlananKarakterAdi);

        // Klonlanan karakter varsa
        if (karakter != null)
        {
            // Klonlanan karakteri kameranın hedefi olarak ayarla
            target = karakter.transform;
        }
        else
        {
            Debug.LogWarning("Klonlanan karakter bulunamadı!");
        }
       
    }

    void Update()
    {
        if (target != null)
        {
            // Kamera pozisyonunu karakterin pozisyonuna ayarla
            transform.position = new Vector3(target.position.x, target.position.y, -30);          
        }
    }

}
