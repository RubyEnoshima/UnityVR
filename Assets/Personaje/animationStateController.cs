using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{

    Animator animator;
    int isWalkingHash;
    int isDeadDummie;
    // Start is called before the first frame update
    int isIdlHash;
    int isMoonwalkHash;
    float vidaDummie = 70f;
    public float limite = 20f;
    Vector3 posInicial;
    private Vector3 moveInput;

    public bool Quietecito = false;
    private CapsuleCollider col;
    bool muerto = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isDeadDummie = Animator.StringToHash("isDead");
        isMoonwalkHash = Animator.StringToHash("isMoonWalk");
        isIdlHash = Animator.StringToHash("isIdl");

        if(!Quietecito) animator.SetBool(isWalkingHash, true);
        // position = posInicial = transform.position.z;
        // posFinal = posInicial + limite;

        posInicial = transform.position;
        gameObject.tag = "Personaje";
    }

    // Update is called once per frame
    void Update()
    {
        // moveInput = new Vector3(0f, 0f, position);
        // moveInput = new Vector3(transform.position.x, transform.position.y, position);
        
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isMoonwalk = animator.GetBool(isMoonwalkHash);
        bool isIdl = animator.GetBool(isIdlHash);
        
        if(muerto)
        {
            animator.SetBool(isDeadDummie, true);
        }
        else if(!Quietecito)
        {
            if(Vector3.Distance(posInicial, transform.position) < limite && isWalking){  
                // Dummie1.MovePosition(moveInput);   
                // position = position + 0.01f;
                transform.position += transform.forward * Time.deltaTime * 2f;
                animator.SetBool(isIdlHash, false);
            }
            else if(Vector3.Distance(posInicial, transform.position) >= limite && !isMoonwalk)
            {
                animator.SetBool(isWalkingHash, false);
                animator.SetBool(isMoonwalkHash, true);
            }
            else if(Vector3.Distance(posInicial, transform.position) > 0.02f && isMoonwalk){
                // Dummie1.MovePosition(moveInput);
                // position = position - 0.01f;
                Debug.Log(Vector3.Distance(posInicial, transform.position));
                transform.position -= transform.forward * Time.deltaTime * 2f;
            }
            else if(isMoonwalk)
            {
                animator.SetBool(isMoonwalkHash, false);
                animator.SetBool(isIdlHash, true);
            }
            else if(isIdl){
                animator.SetBool(isWalkingHash, true);
            }
        }
    }

    public void QuitarVida(float dany){
        vidaDummie -= dany;
        if(vidaDummie <= 0){
            muerto = true;
            Globals.pjMatados++;
            col.enabled = false;
        }
    }
}
