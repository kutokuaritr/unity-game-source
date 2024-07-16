using UnityEngine;
using UnityEngine.UI;

public class OyunKontrol : MonoBehaviour
{
    private bool oyunDurdu = false;
    private float zamanSkalasi = 1f; // Zaman ölçeği (normal hız)

    public GameObject durdurmaCanvas;
    public Button baslatButton;
    public Button hizlandirmaButton;
    public Button yavaslamaButton;
    public Button exit;

    void Start()
    {
        // Başlat düğmesine tıklama olayını bağla
        baslatButton.onClick.AddListener(Baslat);

        // Hızlandırma ve yavaşlatma düğmelerine tıklama olaylarını bağla
        hizlandirmaButton.onClick.AddListener(Hizlandir);
        yavaslamaButton.onClick.AddListener(Yavaslat);

        // Oyun durdurma canvas'ını etkisiz hale getir
        durdurmaCanvas.SetActive(false);

        exit.onClick.AddListener(Exit);
    }

    void Baslat()
{
    DevamEt(); // Oyunu devam ettirme fonksiyonunu çağır
}


    void Update()
    {
        // Oyun duraklatma ve devam ettirme tuşu (ör. Space) veya başlat düğmesine tıklama
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Baslat"))
        {
            if (oyunDurdu)
                DevamEt();
            else
                Duraklat();
        }
    }

    // Oyunu duraklatma fonksiyonu
    void Duraklat()
    {
        Time.timeScale = 0f; // Oyun zamanını durdur
        oyunDurdu = true;
        durdurmaCanvas.SetActive(true); // Duraklatma canvas'ını etkinleştir
    }

    // Oyunu devam ettirme fonksiyonu
    void DevamEt()
    {
        Time.timeScale = zamanSkalasi; // Oyun zamanını devam ettir
        oyunDurdu = false;
        durdurmaCanvas.SetActive(false); // Duraklatma canvas'ını devre dışı bırak
    }

    // Oyunu hızlandırma fonksiyonu
    void Hizlandir()
    {
        if(zamanSkalasi < 32f){
        zamanSkalasi *= 2f; // Zaman ölçeğini iki katına çıkar
        Time.timeScale = zamanSkalasi; // Oyun zamanını hızlandır
        }
    }

    // Oyunu yavaşlatma fonksiyonu
    void Yavaslat()
    {
        if(zamanSkalasi > 1f){
        zamanSkalasi /= 2f; // Zaman ölçeğini yarıya indir
        Time.timeScale = zamanSkalasi; // Oyun zamanını yavaşlat
    }
    }

    void Exit()
    {
           Application.Quit();
    }
}
