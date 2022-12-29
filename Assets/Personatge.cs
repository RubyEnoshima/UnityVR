using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.XR.CommonUsages;

public class Personatge : MonoBehaviour
{
    List<UnityEngine.XR.InputDevice> inputDevices;
    // Start is called before the first frame update
    void Start()
    {
        inputDevices = new List<UnityEngine.XR.InputDevice>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);
        if(leftHandDevices.Count == 1)
        {
            UnityEngine.XR.InputDevice device = leftHandDevices[0];
            Vector2 triggerValue;
            // device.TryGetFeatureValue(primary2DAxis, out triggerValue);
            // Debug.Log(triggerValue);
            // float x = triggerValue[0];
            // float z = triggerValue[1];
            // if(x>-0.2f && x<0.2f) x = 0;
            // if(z>-0.2f && z<0.2f) z = 0;
            // transform.position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z); 
        }else{
            Debug.Log("No conectado");
        }
    }
}
