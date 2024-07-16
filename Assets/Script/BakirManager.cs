using UnityEngine;
using System.Collections.Generic;
using MapGeneratorNamespace;

namespace BakirManagerNamespace
{
public class BakirManager : MonoBehaviour
{
    public GameObject BakirSandik; // Bakır prefab'ı
    private List<Vector3> bakirKonumlari = new List<Vector3>(); // Bakır konumlarını tutacak liste
 
        private BitisKontrol bitisKontrol;

        private void Start()
        {
            // BitisKontrol sınıfından referansı al
            bitisKontrol = FindObjectOfType<BitisKontrol>();
        }


        // Bakır objelerini oluşturacak fonksiyon
        public void CreateBakir(int haritaGenislik, int haritaYukseklik)
    {
      
            // Bakır sayısını rasgele belirle
            int bakirCount = Random.Range(5, 20);

        // Bakır sayısı kadar döngü oluştur
        for (int i = 0; i < bakirCount; i++)
        {
            // Rasgele bir pozisyon belirle
            Vector3 position = new Vector3(Random.Range(1, haritaGenislik-1), Random.Range(1, haritaYukseklik-1), 0f);
            
            // Bakır objesini oluştur
            GameObject bakir = Instantiate(BakirSandik, position, Quaternion.identity);
            
            // Oluşturulan Bakır objesinin konumunu kaydet
            bakirKonumlari.Add(position);
              
            }
            // Orijinal nesneyi görünmez yap
            BakirSandik.SetActive(false);
        }

    // Belirli bir konumdaki Bakır objesini kaldırmak için
    public void DestroyBakir(Vector3 position)
    {
        // Bakır konumlarının olduğu listeden kaldır
        bakirKonumlari.Remove(position);
        
        // Bakır objesini bul ve kaldır
        GameObject bakir = FindBakir(position);
        if (bakir != null)
        {
            Destroy(bakir);
        }
    }

    // Belirli bir konumdaki Bakır objesini bulmak için
    private GameObject FindBakir(Vector3 position)
    {
        // Bakır objelerini bul
        GameObject[] bakirlar = GameObject.FindGameObjectsWithTag("Bakir");
        
        // Bakırlar üzerinde döngü
        foreach (GameObject bakir in bakirlar)
        {
            // Eğer objenin pozisyonu aradığımız pozisyona eşitse, objeyi döndür
            if (bakir.transform.position == position)
            {
                return bakir;
            }
        }
        return null; // Eğer bulunamazsa null döndür
    }
}
}