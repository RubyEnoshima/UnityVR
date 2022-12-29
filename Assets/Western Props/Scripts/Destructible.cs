using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

	public GameObject destroyedVersion;	// Reference to the shattered version of the object
	public AudioClip clip;
	Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody>();
	}

	void Destruir(){
		AudioSource.PlayClipAtPoint(clip,transform.position);
		Instantiate(destroyedVersion, transform.position, transform.rotation);
		Destroy(gameObject);
	}

	void OnMouseDown ()
	{
		Destruir();
	}

	void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag=="Bala" || collision.relativeVelocity.magnitude > rb.mass*9.8f)
            Destruir();
    }

}
