using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MapGeneratorNamespace;
using AltinManagerNamespace;
using BakirManagerNamespace;
using MetalManagerNamespace;
using ZumrutManagerNamespace;


namespace UIManagerNamespace
{
    public class UIManager : MonoBehaviour
    {
        public GameObject inputPanel;
        public GameObject SandikCanva;
        public GameObject BaslatOnİzleme;
        public InputField xInputField;
        public InputField yInputField;
        public static UIManager Instance;
        public Button YeniOyunBtn;
        public Button hizlandir;
        public Button yavaslat;
        public GameObject arkaplan;
 
        void Awake()   //MapGenerator'dan else kontrolünde tekrar OnNewGameButtonClicked çalıştırmak için Instance tanımlıyoruz
    {
    Instance = this;
    }



        public void OnNewGameButtonClicked()
        {
            // "Yeni Oyun" butonuna tıklandığında inputPanel'i aktif hale getir
            inputPanel.SetActive(true);
            YeniOyunBtn.gameObject.SetActive(false);
         }

        public void OnStartButtonClicked()
        {
            // "Oluştur" butonuna tıklandığında bu fonksiyon çalışacak
            int x = int.Parse(xInputField.text);
            int y = int.Parse(yInputField.text);

            // Harita oluşturulurken MapGenerator'ı çağır
            MapGeneratorNamespace.MapGenerator.CreateMap(x, y);

        
            // Harita oluşturulduktan sonra inputPanel'i tekrar pasif hale getir
            inputPanel.SetActive(false);
            hizlandir.gameObject.SetActive(true);
            yavaslat.gameObject.SetActive(true);
            arkaplan.SetActive(false);
        
        }


    }

  }
 