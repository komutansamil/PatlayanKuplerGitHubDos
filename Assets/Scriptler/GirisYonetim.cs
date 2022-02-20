using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GirisYonetim : MonoBehaviour
{
    [SerializeField] private GameObject girişPaneli;
    [SerializeField] private GameObject ayarlarPaneli;
    [SerializeField] private GameObject yapımcıPaneli;

    [SerializeField] private ScriptableObjKayit ScriptableObjKayit;

    [SerializeField] private TextMeshProUGUI puanTxt;
    [SerializeField] private TextMeshProUGUI yetenekTxt;
    [SerializeField] private TextMeshProUGUI sesTxt;
    [SerializeField] private AudioSource oyunMüziği;
    [SerializeField] private Slider sldr;
    private bool açıkİse = true;

    // Start is called before the first frame update
    void Start()
    {
        girişPaneli.SetActive(true);
        ayarlarPaneli.SetActive(false);
        yapımcıPaneli.SetActive(false);

        puanTxt.text = "Puan: " + ScriptableObjKayit.puanSayısı.ToString();
        yetenekTxt.text = "Yetenek: " + ScriptableObjKayit.yetenekSayısı.ToString();

        sldr.value = ScriptableObjKayit.sesSeviyesi;
        açıkİse = ScriptableObjKayit.sesAçıkİse;
        if (açıkİse)
        {
            sesTxt.text = "Açık";
            ScriptableObjKayit.sesAçıkİse = true;
            oyunMüziği.Play();
        }
        else
        {
            sesTxt.text = "Kapalı";
            ScriptableObjKayit.sesAçıkİse = false;
            oyunMüziği.Pause();
        }
    }

    // Update is called once per frame
    void Update()
    {
        ScriptableObjKayit.sesSeviyesi = sldr.value;
        oyunMüziği.volume = ScriptableObjKayit.sesSeviyesi;
    }
    public void Başla()
    {
        SceneManager.LoadScene("OyunSahnesi");
    }
    public void Ayarlar()
    {
        girişPaneli.SetActive(false);
        ayarlarPaneli.SetActive(true);
        yapımcıPaneli.SetActive(false);
    }
    public void Yapımcı()
    {
        girişPaneli.SetActive(false);
        ayarlarPaneli.SetActive(false);
        yapımcıPaneli.SetActive(true);
    }
    public void ÇıkışYapımcıButonu()
    {
        girişPaneli.SetActive(true);
        ayarlarPaneli.SetActive(false);
        yapımcıPaneli.SetActive(false);
    }
    public void ÇıkışAyarlarButonu()
    {
        girişPaneli.SetActive(true);
        ayarlarPaneli.SetActive(false);
        yapımcıPaneli.SetActive(false);
    }

    public void SesButonu()
    {
        açıkİse = !açıkİse;
        if (açıkİse)
        {
            sesTxt.text = "Açık";
            ScriptableObjKayit.sesAçıkİse = true;
            oyunMüziği.Play();
        }
        else
        {
            sesTxt.text = "Kapalı";
            ScriptableObjKayit.sesAçıkİse = false;
            oyunMüziği.Pause();
        }
    }
}
