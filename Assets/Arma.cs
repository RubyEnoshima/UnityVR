using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour
{
    public float dany = 10f;
    public float rang = 100f;
    public float velocitatFoc = 15f;
    public float impacte = 50f;
    AudioSource so;

    public Transform apuntar;

    public ParticleSystem ps;

    float temps = 0f;
    bool pressionat = false;

    // Start is called before the first frame update
    void Start()
    {
        so = GetComponent<AudioSource>();
        gameObject.tag = "Arma";
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("a") || pressionat) Interactuar();
    }

    public void Interactuar(){
        pressionat = true;
        if(Time.time>=temps) {
            temps = Time.time + 1f/velocitatFoc;
            Disparar();
        }
    }

    public void Deixar(){
        pressionat = false;
    }

    void Disparar(){
        // AudioSource.PlayClipAtPoint(so,transform.position);
        so.Play();
        ps.Play();
        RaycastHit hit;
        if(Physics.Raycast(apuntar.position, apuntar.forward, out hit, rang)){
            GameObject objectiu = hit.collider.gameObject;
            // Debug.Log(objectiu.tag);
            if(objectiu.tag=="Destruible"){
                objectiu.GetComponent<Destructible>().Dany(dany);
            }else if(objectiu.tag=="Personaje"){
                objectiu.GetComponent<animationStateController>().QuitarVida(dany);
            }

            if(hit.rigidbody && objectiu.tag!="Estatic" && objectiu.tag!="Pizarra")
                hit.rigidbody.AddForce(-hit.normal * impacte);
        }

        Globals.balasDisparadas++;
        // Debug.DrawRay(apuntar.position,apuntar.forward,Color.red,500f);

    }
}
