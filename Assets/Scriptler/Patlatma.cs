using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Patlatma : MonoBehaviour
{
    RaycastHit carpma;
    [SerializeField] private GameObject patlamaEffekti;
    [Range(0, 1000)] [SerializeField] private float patlamaGucu = 200f;
    [SerializeField] private GameObject[] oluşturulmaNoktası;
    [SerializeField] private GameObject yürüyenObje;
    [SerializeField] private float toplamZaman = 1f;
    private float zaman;
    [SerializeField] private float geçenZaman;
    private float geçenToplamZaman;
    [SerializeField] private ScriptableObjKayit ScriptableObjKayit;
    [SerializeField] private TextMeshProUGUI puanTxt;
    [SerializeField] private TextMeshProUGUI yetenekTxt;
    [SerializeField] private TextMeshProUGUI başlamaZamanıTxt;

    [SerializeField] private AudioSource ateşSesi;
    [SerializeField] private AudioSource ateşSesiIska;
    [SerializeField] private AudioSource oyunMüziği;

    private bool zamanBool1 = true;
    private bool zamanBool2 = false;
    private bool zamanBool3 = false;
    private bool zamanBool4 = false;

    private bool sesAçık = false;

    private bool[] boollar;

    private float başlamaZamanı = 3f;
    private float başlatZamanı = 0f;
    private bool başlatBool = false;
    void Start()
    {
        başlamaZamanıTxt.gameObject.SetActive(true);

        boollar = new bool[4];
        boollar[0] = true;

        ScriptableObjKayit.yetenekSayısı = 0;
        ScriptableObjKayit.puanSayısı = 0;

        oyunMüziği.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (ScriptableObjKayit.sesAçıkİse)
        {
            oyunMüziği.volume = ScriptableObjKayit.sesSeviyesi;
            ateşSesi.volume = ScriptableObjKayit.sesSeviyesi;
            ateşSesiIska.volume = ScriptableObjKayit.sesSeviyesi;
        }
        if (!ScriptableObjKayit.sesAçıkİse)
        {
            oyunMüziği.volume = 0f;
            ateşSesi.volume = 0f;
            ateşSesiIska.volume = 0f;
        }

        //if(sesAçık)
        //{
        //    oyunMüziği.Play();
        //    ateşSesi.Play();
        //    ateşSesiIska.Play();
        //}
        //if(!sesAçık)
        //{
        //    oyunMüziği.Pause();
        //    ateşSesi.Pause();
        //    ateşSesiIska.Pause();
        //}

        int başlamaİnt = Mathf.RoundToInt(başlamaZamanı);

        başlamaZamanıTxt.text = başlamaİnt.ToString();
        başlamaZamanı -= Time.deltaTime;
        if (!başlatBool)
        {
            if (başlamaZamanı <= 0f)
            {
                ScriptableObjKayit.izlerlemeHızı = 200f;
                başlamaZamanıTxt.text = "Başla!";
                başlatZamanı += Time.deltaTime;
                if (başlatZamanı >= 2f)
                {
                    başlamaZamanıTxt.gameObject.SetActive(false);
                    başlatBool = true;
                }
            }
            if (başlamaZamanı >= 3f)
            {
                ScriptableObjKayit.izlerlemeHızı = 0f;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray isin = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(isin, out carpma))
            {
                if (ScriptableObjKayit.sesAçıkİse)
                    ateşSesiIska.Play();
                Collider[] patlayanObjeler = Physics.OverlapSphere(carpma.point, 2f);

                Quaternion rotation = Quaternion.LookRotation(carpma.normal);
                GameObject patlamaObj = Instantiate(patlamaEffekti, carpma.point, rotation);

                foreach (var bulunan in patlayanObjeler)
                {
                    Rigidbody govde = bulunan.GetComponent<Rigidbody>();

                    if (govde != null)
                    {
                        govde.AddExplosionForce(patlamaGucu, carpma.point, 4f);
                        Destroy(bulunan.gameObject);
                        ScriptableObjKayit.puanSayısı += 1;
                        ScriptableObjKayit.yetenekSayısı += 1;

                        if (ScriptableObjKayit.sesAçıkİse)
                            ateşSesi.Play();

                        yetenekTxt.text = "Yetenek: " + ScriptableObjKayit.yetenekSayısı.ToString();
                        puanTxt.text = "Puan: " + ScriptableObjKayit.puanSayısı.ToString();
                    }
                }
            }
        }

        geçenZaman += Time.deltaTime;
        if (zamanBool1)
        {
            if (geçenZaman >= 10)
            {
                toplamZaman = 0.8f;
                zamanBool2 = true;
                zamanBool1 = false;
            }
            if (zamanBool2)
            {
                if (geçenZaman >= 20)
                {
                    toplamZaman = 0.5f;
                    zamanBool3 = true;
                    zamanBool2 = false;
                }
                if (zamanBool3)
                {
                    if (geçenZaman >= 30)
                    {
                        toplamZaman = 0.3f;
                        zamanBool4 = true;
                        zamanBool3 = false;
                    }
                    if (zamanBool4)
                    {
                        toplamZaman = 0.1f;
                    }
                }
            }

        }

        zaman += Time.deltaTime;
        if (boollar[0] == true)
        {
            if (zaman >= toplamZaman)
            {
                zaman = 0f;
                GameObject zemin = Instantiate(yürüyenObje, oluşturulmaNoktası[0].transform.position, oluşturulmaNoktası[0].transform.rotation);
                zemin.GetComponent<MeshRenderer>().sharedMaterial.color = Color.red;
                Destroy(zemin, 10f);
                boollar[0] = false;
                boollar[1] = true;
            }
        }

        if (boollar[1] == true)
        {
            if (zaman >= toplamZaman)
            {
                zaman = 0f;
                GameObject zemin = Instantiate(yürüyenObje, oluşturulmaNoktası[1].transform.position, oluşturulmaNoktası[1].transform.rotation);
                zemin.GetComponent<MeshRenderer>().sharedMaterial.color = Color.green;
                Destroy(zemin, 10f);
                boollar[1] = false;
                boollar[2] = true;
            }
        }
        if (boollar[2] == true)
        {
            if (zaman >= toplamZaman)
            {
                zaman = 0f;
                GameObject zemin = Instantiate(yürüyenObje, oluşturulmaNoktası[2].transform.position, oluşturulmaNoktası[2].transform.rotation);
                zemin.GetComponent<MeshRenderer>().sharedMaterial.color = Color.yellow;
                Destroy(zemin, 10f);
                boollar[2] = false;
                boollar[3] = true;
            }
        }
        if (boollar[3] == true)
        {
            if (zaman >= toplamZaman)
            {
                zaman = 0f;
                GameObject zemin = Instantiate(yürüyenObje, oluşturulmaNoktası[3].transform.position, oluşturulmaNoktası[3].transform.rotation);
                zemin.GetComponent<MeshRenderer>().sharedMaterial.color = Color.blue;
                Destroy(zemin, 10f);
                boollar[3] = false;
                boollar[0] = true;
            }
        }
    }

    public void ÇıkışButonu()
    {
        SceneManager.LoadScene("GirişSahnesi");
    }
}
