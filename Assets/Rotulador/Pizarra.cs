using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizarra : MonoBehaviour
{
    public Texture2D texture;
    public Vector2 textureSize = new Vector2(2048,2048);
    Renderer r;
    void Start()
    {
        r = GetComponent<Renderer>();
        Limpiar();
    }

    public void Limpiar(){
        texture = new Texture2D((int)textureSize.x,(int)textureSize.y);
        r.material.mainTexture = texture;
    }

    
}
