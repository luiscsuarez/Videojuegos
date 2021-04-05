using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuracionSonido : MonoBehaviour
{
    public float duracionSonido;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, duracionSonido);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
