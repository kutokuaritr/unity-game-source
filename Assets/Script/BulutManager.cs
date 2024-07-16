using UnityEngine;

public class BulutManager : MonoBehaviour
{
    public float BulutyokEtmeMesafesi = 3f; // Bulutları yok etme mesafesi

    private void Update()
    {
        // Bulut objelerini bulalım
        GameObject[] bulutlar = GameObject.FindGameObjectsWithTag("Bulut");

        // Bulutlar dizisini döngüye alıyoruz
        foreach (GameObject bulut in bulutlar)
        {
            // Karakter ile bulut arasındaki mesafeyi kontrol et
            if (Vector3.Distance(transform.position, bulut.transform.position) < BulutyokEtmeMesafesi)
            {
                // Bulutu yok et
                Destroy(bulut);
            }
        }
    }
}
