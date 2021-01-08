using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morte : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Destruir objeto após um período de tempo
        Destroy(gameObject, 2);
    }
}
