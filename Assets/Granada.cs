using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Granada : MonoBehaviour
{
    public float delay = 3f;
    public float radi = 5f;
    public float forca = 500f;
    public GameObject Explosio;
    public AudioClip clip;
    public AudioClip clipDelay;
    public Material rojo;
    bool activado = false;
    Material normal;
    Renderer r;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Renderer>();
        normal = r.material;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("e")) Empezar();
    }

    public void Cogido(SelectEnterEventArgs args){
        AudioSource.PlayClipAtPoint(clipDelay,args.interactorObject.transform.position);
    }

    public void Empezar(){
        if(!activado){
            activado = true;
            Invoke("Explotar",delay);
            InvokeRepeating("Pitar",0f,1f);

        }
    }

    void Pitar(){
        // AudioSource.PlayClipAtPoint(clipDelay,transform.position);
        CambiarMaterial();
        Invoke("CambiarMaterial",0.25f);
    }

    void CambiarMaterial(){
        AudioSource.PlayClipAtPoint(clipDelay,transform.position);
        if(r.material==normal) r.material = rojo;
        else r.material = normal;
    }

    void Explotar(){
        Instantiate(Explosio,transform.position,transform.rotation);

        Collider[] objectes = Physics.OverlapSphere(transform.position,radi);

        foreach(Collider c in objectes){
            if(c.tag=="Destruible"){
                c.GetComponent<Destructible>().Destruir();
            }else if(c.tag=="Personaje"){
                c.GetComponent<animationStateController>().QuitarVida(100000f);
            }
        }

        Collider[] objectesForca = Physics.OverlapSphere(transform.position,radi);
        foreach(Collider c in objectesForca){
            if(c.tag!="Estatic"){
                Rigidbody rb = c.GetComponent<Rigidbody>();
                if(rb!=null){
                    rb.AddExplosionForce(forca,transform.position,radi);
                }

            }
        }

        AudioSource.PlayClipAtPoint(clip,transform.position);

        Globals.patitosDestruidos++;
        Destroy(gameObject);
    }
}
