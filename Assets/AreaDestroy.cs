using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDestroy : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Destroy(collision.gameObject);
        collision.transform.position = new Vector3(0,6,0);
    }
}
