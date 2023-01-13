using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandPresencePhysics : MonoBehaviour
{
    public Transform target;
    public Renderer nonPhysicalHand;
    Renderer thisHand;
    float distance = 0.05f;
    public LineRenderer line;
    Rigidbody rb;
    Collider[] colliders;

    void EnableCollidersAfterDelay(){
        foreach(Collider c in colliders){
            c.enabled = true;
        }
    }

    public void EnableColliders(SelectExitEventArgs args){
        Invoke("EnableCollidersAfterDelay",0.5f);
        if(args.interactableObject.transform.tag=="Arma"){
            thisHand.enabled = true;
            // line.enabled = true;
        }
    }

    public void DisableColliders(SelectEnterEventArgs args){
        foreach(Collider c in colliders){
            c.enabled = false;
        }
        if(args.interactableObject.transform.tag=="Arma"){
            thisHand.enabled = false;
            // line.enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        thisHand = GetComponentInChildren<Renderer>();
    }

    void Update(){
        float d = Vector3.Distance(transform.position, target.position);
        nonPhysicalHand.enabled = d>=distance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = (target.position - transform.position) / Time.fixedDeltaTime;

        Quaternion rotationDifference = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);
        Vector3 rotationDifferenceInDegree = angleInDegree * rotationAxis;
        rb.angularVelocity = (rotationDifferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime);
    }
}
