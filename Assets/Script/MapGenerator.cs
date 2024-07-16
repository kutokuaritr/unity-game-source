using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace MapGeneratorNamespace
{
    public class MapGenerator : MonoBehaviour
    {
        public static MapGenerator Instance;
        public GameObject sandikPrefab;
        public int sandikSayisi;
        public GameObject[] sandiklar;
        public int haritaGenislik = 50;
        public int haritaYukseklik = 50;
        private List<Vector3> usedPositions = new List<Vector3>();

        void Awake()
        {
            Instance = this;
        }

        public static void CreateMap(int x, int y)
        {
            float tileSize = 1f; // Kutu boyutu
            float yOffset = 32f; // Küplerin y eksenindeki başlangıç yüksekliği
            float yOffsetBulut = -40f; // Küplerin y eksenindeki başlangıç yüksekliği

            // Prefab'ları bulma            
            GameObject KisZemin = GameObject.Find("KisZemini");
            GameObject YazZemin = GameObject.Find("YazZemin");
            GameObject karakter = GameObject.Find("karakter");
            GameObject YazAgaciPrefab = GameObject.Find("YazAgaci");
            GameObject KisAgaciPrefab = GameObject.Find("KisAgaci");
            GameObject YazTasPrefab = GameObject.Find("YazTas");
            GameObject KisTasPrefab = GameObject.Find("KisTas");
            GameObject YazDagPrefab = GameObject.Find("YazDag");
            GameObject KisDagPrefab = GameObject.Find("KisDag");
            GameObject YazDuvarPrefab = GameObject.Find("YazDuvar");
            GameObject KisDuvarPrefab = GameObject.Find("KisDuvar");
            GameObject KusAgaciPrefab = GameObject.Find("Kus");
            GameObject AriAgaciPrefab = GameObject.Find("Ari");
            GameObject Bulut = GameObject.Find("Bulut");


            if (KisZemin != null && YazZemin != null && karakter != null && YazAgaciPrefab != null && KisAgaciPrefab != null &&
                YazTasPrefab != null && KisTasPrefab != null && YazDagPrefab != null && KisDagPrefab != null && YazDuvarPrefab != null && KisDuvarPrefab != null
                 && KusAgaciPrefab != null && AriAgaciPrefab != null && Bulut != null)
            {

                if ((x >= 10 && x <= 1500) && (y >= 10 && y <= 1500))
                {

                    // Rasgele bir karakter pozisyonu belirle
                    int characterPosX = Random.Range(0, x);
                    int characterPosY = Random.Range(0, y);
                    Vector3 characterPosition = new Vector3(characterPosX * tileSize, characterPosY * tileSize + yOffset, 0f);

                    // Orijinal karakter nesnesini saklamak için değişken oluştur
                    GameObject originalCharacter = karakter;

                    // Karakteri belirlenen pozisyonda oluştur ve orijinal karakteri sakla
                    GameObject character = GameObject.Instantiate(karakter, characterPosition, Quaternion.identity);
                    originalCharacter.SetActive(false); // Orijinal karakteri devre dışı bırak



                    // Harita oluştur
                    CreateMapTiles(x, y, tileSize, yOffset, KisZemin, YazZemin);
                    CreateMapTilesBulut(x, y, tileSize, yOffsetBulut, Bulut);

                    Debug.Log("Harita oluşturuldu. X: " + x + ", Y: " + y);

                    // Altın, zumrut, metal ve bakır üretimini gerçekleştir
                    AltinManagerNamespace.AltinManager altinManager = FindObjectOfType<AltinManagerNamespace.AltinManager>();
                    ZumrutManagerNamespace.ZumrutManager zumrutManager = FindObjectOfType<ZumrutManagerNamespace.ZumrutManager>();
                    MetalManagerNamespace.MetalManager metalManager = FindObjectOfType<MetalManagerNamespace.MetalManager>();
                    BakirManagerNamespace.BakirManager bakirManager = FindObjectOfType<BakirManagerNamespace.BakirManager>();
                    Debug.Log("Sandık Map");
                    if (altinManager != null && zumrutManager != null && metalManager != null && bakirManager != null)
                    {
                        // Altın, zumrut, metal ve bakır üretimini gerçekleştir
                        //   int minSandik = 5; // Altın sayısını istediğiniz gibi belirleyin
                        //   int maxSandik = 20; // Altın sayısını istediğiniz gibi belirleyin
                        altinManager.CreateAltin(x, y);
                        zumrutManager.CreateZumrut(x, y);
                        metalManager.CreateMetal(x, y);
                        bakirManager.CreateBakir(x, y);
                        Debug.Log("Sandık Map2");

                    }
                    else
                    {
                        Debug.LogWarning("Sandıklar bulunamadı!");
                    }

                    // Sabit engelleri oluştur
                    int minKusCount = 1; // Hareketli engel olan minKusCount burada tanımlanıyor
                    int maxKusCount = 2;
                    int minAriCount = 1;
                    int maxAriCount = 2;
                    int minKisAgaciCount = 2;
                    int maxKisAgaciCount = 5;
                    int minYazAgaciCount = 2;
                    int maxYazAgaciCount = 5;
                    int minKisTasCount = 2;
                    int maxKisTasCount = 5;
                    int minYazTasCount = 2;
                    int maxYazTasCount = 5;
                    int minYazDagCount = 2;
                    int maxYazDagCount = 5;
                    int minKisDagCount = 2;
                    int maxKisDagCount = 5;
                    int minKisDuvarCount = 2;
                    int maxKisDuvarCount = 5;
                    int minYazDuvarCount = 2;
                    int maxYazDuvarCount = 5;

                    // Hareketli engelleri oluştur
                    CreateMovingObstacles(minKusCount, maxKusCount, KusAgaciPrefab, x, y, tileSize, yOffset);
                    CreateMovingObstacles(minAriCount, maxAriCount, AriAgaciPrefab, x, y, tileSize, yOffset);

                    // Sabit engelleri oluştur
                    CreateStaticObstacles(minKisAgaciCount, maxKisAgaciCount, KisAgaciPrefab, 2, x / 2, 2, y - 2, x, y, tileSize, yOffset);
                    CreateStaticObstacles(minYazAgaciCount, maxYazAgaciCount, YazAgaciPrefab, x / 2, x - 2, 2, y - 2, x, y, tileSize, yOffset);
                    CreateStaticObstacles(minKisTasCount, maxKisTasCount, KisTasPrefab, 2, x / 2, 2, y - 2, x, y, tileSize, yOffset);
                    CreateStaticObstacles(minYazTasCount, maxYazTasCount, YazTasPrefab, x / 2, x - 2, 2, y - 2, x, y, tileSize, yOffset);
                    CreateStaticObstacles(minKisDagCount, maxYazDagCount, KisDagPrefab, 15, x / 2, 15, y - 15, x, y, tileSize, yOffset);
                    CreateStaticObstacles(minYazDagCount, maxYazDagCount, YazDagPrefab, x / 2, x - 15, 15, y - 15, x, y, tileSize, yOffset);
                    CreateStaticObstacles(minKisDuvarCount, maxKisDuvarCount, KisDuvarPrefab, 10, x / 2, 1, y - 1, x, y, tileSize, yOffset);
                    CreateStaticObstacles(minYazDuvarCount, maxYazDuvarCount, YazDuvarPrefab, x / 2, x - 10, 1, y - 1, x, y, tileSize, yOffset);
                }
                else
                {
                    Debug.LogWarning("Oyun Sahnesi En Az 10*10, En Fazla 1500*1500 olmalıdır.");
                    UIManagerNamespace.UIManager.Instance.OnNewGameButtonClicked();
                }
            }
            else
            {
                Debug.LogWarning("Prefab'lardan biri veya her ikisi bulunamadı!");
            }
        }

        public static void CreateMovingObstacles(int minCount, int maxCount, GameObject obstaclePrefab, int x, int y, float tileSize, float yOffset)
        {
            for (int i = 0; i < Random.Range(minCount, maxCount); i++)
            {
                int posX = Random.Range(2, x - 2);
                int posY = Random.Range(7, y - 7);
                Vector3 position = new Vector3(posX * tileSize, posY * tileSize + yOffset, 0f);
                GameObject obstacle = Instantiate(obstaclePrefab, position, Quaternion.identity);
                Debug.LogWarning(obstaclePrefab.name + " oluşturuldu! Map");
            }
        }

        public static void CreateStaticObstacles(int minCount, int maxCount, GameObject obstaclePrefab, int minX, int maxX, int minY, int maxY, int x, int y, float tileSize, float yOffset)
        {
            for (int i = 0; i < Random.Range(minCount, maxCount); i++)
            {
                int posX = Random.Range(minX, maxX);
                int posY = Random.Range(minY, maxY);
                Vector3 position = new Vector3(posX * tileSize, posY * tileSize + yOffset, 0f);

               bool positionOccupied = false;
                Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 15f);
                foreach (Collider2D collider in colliders)
                {
                    if (collider.CompareTag("SabitNesme"))
                    {
                        positionOccupied = true;
                        break;
                    }
                }

                
                if (positionOccupied)
                {
                    continue;
                }

                
                GameObject obstacle = Instantiate(obstaclePrefab, position, Quaternion.identity);
                obstacle.tag = "SabitNesme"; 
                Debug.LogWarning(obstaclePrefab.name + " oluşturuldu! Map");
            }
        }


        private static void CreateMapTiles(int x, int y, float tileSize, float yOffset, GameObject kisZemini, GameObject YazZemin)
        {
            for (int i = 0; i < x; i+=50)
            {
                for (int j = 0; j < y; j+=50)
                {
                    Vector3 position = new Vector3(i * tileSize + yOffset, j * tileSize + yOffset, 1f); // Yeni kutunun pozisyonu
                    GameObject cube;

                    // İlk yarısı KisZemin prefab'ını kullan
                    if (i < x / 2)
                    {
                        cube = GameObject.Instantiate(kisZemini, position, Quaternion.identity);
                    }
                    // Diğer yarısı YazZemin prefab'ını kullan
                    else
                    {
                        cube = GameObject.Instantiate(YazZemin, position, Quaternion.identity);
                    }
                }
            }
        }

        private static void CreateMapTilesBulut(int x, int y, float tileSize, float yOffsetBulut, GameObject Bulut)
        {
            for (int i = 0; i < x+100; i+=3)
            {
                for (int j = 0; j < y+100; j+=3)
                {
                    Vector3 position = new Vector3(i * tileSize + yOffsetBulut, j * tileSize + yOffsetBulut, 1f); // Yeni kutunun pozisyonu
                    GameObject cube;

                    cube = GameObject.Instantiate(Bulut, position, Quaternion.identity);

                   
                }
            }
        }
    }
}