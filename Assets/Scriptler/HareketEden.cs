using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HareketEden : MonoBehaviour
{
    private float ilerlemeHızı = 20f;
    [SerializeField] private ScriptableObjKayit ScriptableObjKayit;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ilerlemeHızı = ScriptableObjKayit.izlerlemeHızı;
        rb.AddForce(transform.forward * ilerlemeHızı * Time.deltaTime, ForceMode.Force);
    }
}
