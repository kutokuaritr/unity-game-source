using UnityEngine;
using System.Collections.Generic;
using MapGeneratorNamespace;

namespace MetalManagerNamespace
{
public class MetalManager : MonoBehaviour
{
    public GameObject MetalSandik; // Gümüş prefab'ı
    private List<Vector3> metalKonumlari = new List<Vector3>(); // Gümüş konumlarını tutacak liste

        private BitisKontrol bitisKontrol;

        private void Start()
        {
            // BitisKontrol sınıfından referansı al
            bitisKontrol = FindObjectOfType<BitisKontrol>();
        }


        // Gümüş objelerini oluşturacak fonksiyon
        public void CreateMetal(int haritaGenislik, int haritaYukseklik)
    {

            // Gümüş sayısını rasgele belirle
            int metalCount = Random.Range(5, 20);

        // Gümüş sayısı kadar döngü oluştur
        for (int i = 0; i < metalCount; i++)
        {
            // Rasgele bir pozisyon belirle
            Vector3 position = new Vector3(Random.Range(1, haritaGenislik-1), Random.Range(1, haritaYukseklik-1), 0f);
            
            // Gümüş objesini oluştur
            GameObject metal = Instantiate(MetalSandik, position, Quaternion.identity);
            
            // Oluşturulan Gümüş objesinin konumunu kaydet
            metalKonumlari.Add(position);
               
            }
            // Orijinal nesneyi görünmez yap
            MetalSandik.SetActive(false);
        }

    // Belirli bir konumdaki Gümüş objesini kaldırmak için
    public void DestroyMetal(Vector3 position)
    {
        // Gümüş konumlarının olduğu listeden kaldır
        metalKonumlari.Remove(position);
        
        // Gümüş objesini bul ve kaldır
        GameObject metal = FindMetal(position);
        if (metal != null)
        {
            Destroy(metal);
        }
    }

    // Belirli bir konumdaki Gümüş objesini bulmak için
    private GameObject FindMetal(Vector3 position)
    {
        // Gümüş objelerini bul
        GameObject[] metallar = GameObject.FindGameObjectsWithTag("Metal");
        
        // Gümüşler üzerinde döngü
        foreach (GameObject metal in metallar)
        {
            // Eğer objenin pozisyonu aradığımız pozisyona eşitse, objeyi döndür
            if (metal.transform.position == position)
            {
                return metal;
            }
        }
        return null; // Eğer bulunamazsa null döndür
    }
}
}