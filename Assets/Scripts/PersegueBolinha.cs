using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersegueBolinha : MonoBehaviour
{
    [SerializeField]
    private Transform bolinha;
    [SerializeField]
    private Vector3 dist;
    [SerializeField]
    private float lerpVal;
    [SerializeField]
    private Vector3 pos, alvoPos;

    // Start is called before the first frame update
    void Start()
    {
        // Pegando a posição da camera
        dist = bolinha.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate() 
    {
        if(!BolaControlador.gameOver) 
        {
            //Mecanica para a camera perseguir objeto
            PersegueFunc();
        }        
    }

    void PersegueFunc() 
    {
        pos = transform.position; //Pega a posição da camera
        alvoPos = bolinha.position - dist; //Pega a posição do alvo
        pos = Vector3.Lerp(pos, alvoPos, lerpVal);
        transform.position = pos;
    }
}
