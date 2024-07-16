using UnityEngine;
using System.Collections.Generic;
using MapGeneratorNamespace;

namespace AltinManagerNamespace
{

    public class AltinManager : MonoBehaviour
    {
        public GameObject AltinSandik; // Altın prefab'ı
        private List<Vector3> AltinKonumlari = new List<Vector3>(); // Altın konumlarını tutacak liste
        private List<GameObject> silinecekAltinlar = new List<GameObject>(); // Silinecek altınların listesi


        // Altın objelerini oluşturacak fonksiyon
        public void CreateAltin(int haritaGenislik, int haritaYukseklik)
        {

            // Altın sayısını rasgele belirle
            int AltinCount = Random.Range(5, 20);
            Debug.Log("Sandık Altin");
            // Altın sayısı kadar döngü oluştur
            for (int i = 0; i < AltinCount; i++)
            {
                // Rasgele bir pozisyon belirle
                Vector3 position = new Vector3(Random.Range(1, haritaGenislik - 1), Random.Range(1, haritaYukseklik - 1), 0f);

                // Altın objesini oluştur
                GameObject Altin = Instantiate(AltinSandik, position, Quaternion.identity);

                // Oluşturulan Altın objesinin konumunu kaydet
                AltinKonumlari.Add(position);

                Debug.Log("Sandık Altin2");
                //   bitisKontrol.KontrolEt(1); // Her altın sandığı oluşturulduğunda BitisKontrol'e 1 sandık eklendiğini belirtiyoruz


            }
            // Orijinal nesneyi görünmez yap
            AltinSandik.SetActive(false);
        }

        // Karakterin sandıkla temas ettiğinde silinecek sandığı listeye ekle
        public void AddSilinecekAltin(GameObject Altin)
        {
            silinecekAltinlar.Add(Altin);
        }

        // Silinecek sandıkları kontrol et ve sil
        private void Update()
        {
            foreach (GameObject Altin in silinecekAltinlar)
            {
                AltinKonumlari.Remove(Altin.transform.position);
                Destroy(Altin);
            }
            silinecekAltinlar.Clear(); // Listenin temizlenmesi
        }

        // Belirli bir konumdaki Altın objesini kaldırmak için
        public void DestroyAltin(Vector3 position)
        {
            // Altın konumlarının olduğu listeden kaldır
            AltinKonumlari.Remove(position);

            // Altın objesini bul ve silinecekler listesine ekle
            GameObject Altin = FindAltin(position);
            if (Altin != null)
            {
                AddSilinecekAltin(Altin);
            }
        }

        // Belirli bir konumdaki Altın objesini bulmak için
        private GameObject FindAltin(Vector3 position)
        {
            // Altın objelerini bul
            GameObject[] Altinlar = GameObject.FindGameObjectsWithTag("Altin");

            // Altınlar üzerinde döngü
            foreach (GameObject Altin in Altinlar)
            {
                // Eğer objenin pozisyonu aradığımız pozisyona eşitse, objeyi döndür
                if (Altin.transform.position == position)
                {
                    return Altin;
                }
            }
            return null; // Eğer bulunamazsa null döndür
        }


    }
}