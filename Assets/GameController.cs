using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameController : MonoBehaviour
{
    public GameObject Patito;
    public GameObject Pistola;
    public GameObject Rifle;
    public GameObject Caja;
    public GameObject Botella1;
    public GameObject Botella2;

    int aparecido = 0;
    public TextMeshPro[] textos;
    public AudioClip pato;
    public AudioClip bala;
    public AudioClip roto;
    public Transform finalPos;
    Transform[] posiciones;
    Granada patitoGranada;

    bool confetiEncendido = false;
    public ParticleSystem c1, c2;

    // Start is called before the first frame update
    void Start()
    {      
        posiciones = finalPos.GetComponentsInChildren<Transform>();  
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Resetear(){
        SceneManager.LoadScene("Main");
        Globals.vecesReseteadas++;
    }

    public void SpawnearPatito(Transform pos){
        GameObject go = Instantiate(Patito,pos.position,Quaternion.identity);
        patitoGranada = go.GetComponent<Granada>();
    }

    public void SpawnearPistola(Transform pos){
        Instantiate(Pistola,pos.position,Quaternion.identity);

    }

    public void SpawnearRifle(Transform pos){
        Instantiate(Rifle,pos.position,Quaternion.identity);
    }

    public void SpawnearCaja(Transform pos){
        Instantiate(Caja,pos.position,Quaternion.identity);

    }
    
    public void SpawnearBotella(Transform pos){
        if(Random.Range(0f,1f)>=0.5f)
            Instantiate(Botella1,pos.position,Quaternion.identity);
        else
            Instantiate(Botella2,pos.position,Quaternion.identity);
    }

    public void Finalizar(){
        InvokeRepeating("Aparecer",0f,1f);
    }

    void Aparecer(){
        if(aparecido<5){
            switch(aparecido){
                case 0:
                    textos[aparecido].text = Globals.patitosDestruidos.ToString();
                    AudioSource.PlayClipAtPoint(pato,transform.position);
                    break;
                case 1:
                    textos[aparecido].text = Globals.pjMatados.ToString();
                    break;
                case 2:
                    textos[aparecido].text = Globals.balasDisparadas.ToString();
                    AudioSource.PlayClipAtPoint(bala,transform.position);

                    break;
                case 3:
                    textos[aparecido].text = Globals.cosasRotas.ToString();
                    AudioSource.PlayClipAtPoint(roto,transform.position);

                    break;
                case 4:
                    textos[aparecido].text = Globals.vecesReseteadas.ToString();
                    break;
                
            }
            textos[aparecido].gameObject.SetActive(true);
            aparecido++;
        }else{
            // ACABAR
            for(int i=0;i<posiciones.Length;i++){
                SpawnearPatito(posiciones[i]);
                patitoGranada.Empezar();
            }
            CancelInvoke();
            Invoke("Acabar",4.5f);
        }
    }

    void Acabar(){
        Globals.Resetear();
        SceneManager.LoadScene("Start");
    }

    public void Empezar(){
        SceneManager.LoadScene("Main");
    }

    public void Confeti(){
        confetiEncendido = !confetiEncendido;
        if(confetiEncendido){
            c1.Play();
            c2.Play();
        }else{
            c1.Stop();
            c2.Stop();
        }
    }
}
