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
    public LineRenderer laser;
    public Transform laserOrigen;

    bool aLaMa = false;
    float temps = 0f;

    // Start is called before the first frame update
    void Start()
    {
        so = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Laser();
        if(Input.GetKey("a")) Interactuar();
        // if(aLaMa){
        //     var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
        //     UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);
        //     if(leftHandDevices.Count == 1)
        //     {
        //         UnityEngine.XR.InputDevice device = leftHandDevices[0];
        //         Vector2 triggerValue;
        //         // device.TryGetFeatureValue(primary2DAxis, out triggerValue);
        //         // Debug.Log(triggerValue);
        //         // float x = triggerValue[0];
        //         // float z = triggerValue[1];
        //         // if(x>-0.2f && x<0.2f) x = 0;
        //         // if(z>-0.2f && z<0.2f) z = 0;
        //         // transform.position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z); 
        //     }
        // }
    }

    void Laser(){
        laser.SetPosition(0,laserOrigen.position);
        RaycastHit hit;
        if(Physics.Raycast(laserOrigen.position,laserOrigen.forward, out hit)){
            if(hit.collider){
                laser.SetPosition(1,hit.point);
            }
        }else laser.SetPosition(1,laserOrigen.forward * rang);
    }

    public void Interactuar(){
        if(Time.time>=temps) {
            temps = Time.time + 1f/velocitatFoc;
            Disparar();
        }
    }

    void Disparar(){
        // AudioSource.PlayClipAtPoint(so,transform.position);
        so.Play();
        RaycastHit hit;
        if(Physics.Raycast(apuntar.position, apuntar.forward, out hit, rang)){
            GameObject objectiu = hit.collider.gameObject;
            Debug.Log(objectiu.tag);
            if(objectiu.tag=="Destruible"){
                objectiu.GetComponent<Destructible>().Dany(dany);
            }

            if(objectiu.tag!="Estatic" && objectiu.tag!="Pizarra")
                hit.rigidbody.AddForce(-hit.normal * impacte);
        }
        // Debug.DrawRay(apuntar.position,apuntar.forward,Color.red,500f);

    }
}
