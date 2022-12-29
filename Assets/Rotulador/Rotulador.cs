using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Rotulador : MonoBehaviour
{
    [SerializeField]
    private Transform punta;
    [SerializeField]
    private int tamany = 5;
    [SerializeField]
    private Color color;

    private Renderer renderer;
    private Color[] colors;
    private float altura;
    private RaycastHit tocat;
    private Pizarra Pizarra = null;
    private Vector2 posTocat;
    private bool tocatUltimFrame;
    private Vector2 ultimaPos;
    private Quaternion ultimaRot;

    // Start is called before the first frame update
    void Start()
    {
        renderer = punta.GetComponent<Renderer>();
        renderer.material.color = color;
        // Fa un cuadrat de tamany^2
        colors = Enumerable.Repeat(renderer.material.color, tamany*tamany).ToArray();
        altura = punta.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        Dibuixar();
    }

    private void Dibuixar(){
        // Mirem si estem tocant una pisarra
        if(Physics.Raycast(punta.position, transform.up, out tocat, altura)){
            if(tocat.transform.CompareTag("Pizarra")){
                if(Pizarra == null){
                    Pizarra = tocat.transform.GetComponent<Pizarra>();
                }

                posTocat = new Vector2(tocat.textureCoord.x,tocat.textureCoord.y);

                int x = (int)(posTocat.x * Pizarra.textureSize.x - (tamany/2));
                int y = (int)(posTocat.y * Pizarra.textureSize.y - (tamany/2));

                // Si som a fora de la pisarra no fem res
                if(y < 0 || y > Pizarra.textureSize.y || x < 0 || x > Pizarra.textureSize.x) return;

                if(tocatUltimFrame){
                    if(ultimaPos == new Vector2(x,y)) return;
                    
                    Pizarra.texture.SetPixels(x, y, tamany, tamany, colors);

                    for(float f = 0.01f; f < 1f; f+=0.05f){
                        int lerpX = (int)Mathf.Lerp(ultimaPos.x, x, f);
                        int lerpY = (int)Mathf.Lerp(ultimaPos.y, y, f);
                        Pizarra.texture.SetPixels(lerpX, lerpY, tamany, tamany, colors);

                    }

                    // Prevenim que el rotu es vagi
                    transform.rotation = ultimaRot;

                    Pizarra.texture.Apply();
                }

                ultimaPos = new Vector2(x,y);
                ultimaRot = transform.rotation;
                tocatUltimFrame = true;
                return;
            }
        }

        Pizarra = null;
        tocatUltimFrame = false;
    }

}
