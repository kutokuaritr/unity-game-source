using UnityEngine;
using UnityEngine.UI;

public class BitisKontrol : MonoBehaviour
{
    public GameObject oyunBittiPanel; // Oyun bitti paneli referansı
    public Text toplamHareketText; // Toplam hareket sayısı metni için referans
    public Text toplamSandikSkoruText; // Toplam sandık skoru metni için referans
    public Button BitisYeniOyunBtn; // Bitiş panelindeki yeni oyun butonu referansı
    public UIManagerNamespace.UIManager uiManager;

    private void Start()
    {
        // Yeni oyun butonuna tıklanma olayını dinle
        BitisYeniOyunBtn.onClick.AddListener(BitisYeniOyunClic);

        // Toplam mesafeyi al
        float toplamMesafe = PlayerPrefs.GetFloat("ToplamMesafe", 0f);

        float roundedMesafe = Mathf.Round(toplamMesafe);
        toplamHareketText.text = "Karakterin Gittiği Toplam Mesafe: " + roundedMesafe.ToString() + " Birim";

    }

    public void BitisYeniOyunClic()
    {
        Application.Quit();
    }
}