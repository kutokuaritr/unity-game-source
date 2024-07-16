using UnityEngine;
using System.Collections.Generic;
using MapGeneratorNamespace;

namespace ZumrutManagerNamespace{

public class ZumrutManager : MonoBehaviour
{
    public GameObject ZumrutSandik; // Altın prefab'ı
    private List<Vector3> zumrutKonumlari = new List<Vector3>(); // Altın konumlarını tutacak liste
    private List<GameObject> silinecekZumrutlar = new List<GameObject>(); // Silinecek altınların listesi


        // Altın objelerini oluşturacak fonksiyon
        public void CreateZumrut(int haritaGenislik, int haritaYukseklik)
    {
            
            // Altın sayısını rasgele belirle
            int zumrutCount = Random.Range(5, 20);
            Debug.Log("Sandık zümrüt");
            // Altın sayısı kadar döngü oluştur
            for (int i = 0; i < zumrutCount; i++)
        {
            // Rasgele bir pozisyon belirle
            Vector3 position = new Vector3(Random.Range(1, haritaGenislik-1), Random.Range(1, haritaYukseklik-1), 0f);
            
            // Altın objesini oluştur
            GameObject zumrut = Instantiate(ZumrutSandik, position, Quaternion.identity);
            
            // Oluşturulan Altın objesinin konumunu kaydet
            zumrutKonumlari.Add(position);

                Debug.Log("Sandık zümrüt2");
                //   bitisKontrol.KontrolEt(1); // Her altın sandığı oluşturulduğunda BitisKontrol'e 1 sandık eklendiğini belirtiyoruz

               
            }
            // Orijinal nesneyi görünmez yap
            ZumrutSandik.SetActive(false);
        }

    // Karakterin sandıkla temas ettiğinde silinecek sandığı listeye ekle
    public void AddSilinecekZumrut(GameObject zumrut)
    {
        silinecekZumrutlar.Add(zumrut);
    }

    // Silinecek sandıkları kontrol et ve sil
    private void Update()
    {
        foreach (GameObject zumrut in silinecekZumrutlar)
        {
            zumrutKonumlari.Remove(zumrut.transform.position);
            Destroy(zumrut);
        }
        silinecekZumrutlar.Clear(); // Listenin temizlenmesi
    }

    // Belirli bir konumdaki Altın objesini kaldırmak için
    public void DestroyZumrut(Vector3 position)
    {
        // Altın konumlarının olduğu listeden kaldır
        zumrutKonumlari.Remove(position);
        
        // Altın objesini bul ve silinecekler listesine ekle
        GameObject zumrut = FindZumrut(position);
        if (zumrut != null)
        {
            AddSilinecekZumrut(zumrut);
        }
    }

    // Belirli bir konumdaki Altın objesini bulmak için
    private GameObject FindZumrut(Vector3 position)
    {
        // Altın objelerini bul
        GameObject[] zumrutlar = GameObject.FindGameObjectsWithTag("Zumrut");
        
        // Altınlar üzerinde döngü
        foreach (GameObject zumrut in zumrutlar)
        {
            // Eğer objenin pozisyonu aradığımız pozisyona eşitse, objeyi döndür
            if (zumrut.transform.position == position)
            {
                return zumrut;
            }
        }
        return null; // Eğer bulunamazsa null döndür
    }

    
}
}