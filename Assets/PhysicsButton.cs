using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{
    public UnityEvent OnPressed, OnReleased;
    public float threshold = 0.1f;
    public float deadZone = 0.025f;

    bool isPressed = false;
    Vector3 startPos;
    ConfigurableJoint cj;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
        cj = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPressed && GetValue() + threshold >= 1){
            Pressed();
        }else if(isPressed && GetValue() - threshold <= 0){
            Released();
        }
    }

    float GetValue(){
        float value = Vector3.Distance(startPos, transform.localPosition) / cj.linearLimit.limit;
        if(Mathf.Abs(value) < deadZone) value = 0;

        return Mathf.Clamp(value,-1,1);
    }

    void Pressed(){
        isPressed = true;
        OnPressed.Invoke();
        Debug.Log("pulsado");
    }

    void Released(){
        isPressed = false;
        OnReleased.Invoke();
    }
}
