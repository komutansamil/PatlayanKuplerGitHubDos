using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "kayıtDos" ,menuName = "KayıtMenu")]
public class ScriptableObjKayit : ScriptableObject
{
    public string oyuncuİsmi = "oyuncu1";
    public int puanSayısı = 0;
    public int yetenekSayısı = 0;
    public float sesSeviyesi = 0.5f;
    public bool sesAçıkİse= false;
    public float izlerlemeHızı = 0;
}
