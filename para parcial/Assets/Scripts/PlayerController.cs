//Link de video: https://youtu.be/klXi8p55UUM

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    private Rigidbody rb;

    public GameObject cuboVerde;
    private bool rota = false;

    public GameObject cubosRojos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rota)
        {
            cuboVerde.transform.Rotate (new Vector3 (60,60,60)*Time.deltaTime);
        }
    }
    //se debe escribir exactamente FixedUpdate ya que es de unity asi y se ejecuta una vez cada frame
    //Se ejecuta antes de Update
    void FixedUpdate(){
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movimiento = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        rb.AddForce (movimiento*speed);
    }

    //Metodo para activar acciones automaticas.
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag ("Recolectable"))
        {
            //Hace inactivo el objeto (Se oculta)
            other.gameObject.SetActive(false);
            //Destruye objeto y lo libera en la memoria
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag ("RecolectableAmarillo"))
        {
            Vector3 reset = cuboVerde.transform.position;
            transform.position = reset;
        }
        if (other.gameObject.CompareTag ("RecolectableNegro"))
        {
            rota = true;
        }
        if (other.gameObject.CompareTag ("RecolectableRojo"))
        {
            Vector3 mover = new Vector3 (0,0.5f,8);
            cuboVerde.transform.position = mover;
        }
        if (other.gameObject.CompareTag ("RecolectableVerde"))
        {
            Vector3 cuenta = new Vector3 (0,1,0);
            
            Vector3 elevar = cubosRojos.transform.position + cuenta;
            cubosRojos.transform.position = elevar;
        }

        else
        {
            
        }
    }
}

//Link de video: https://youtu.be/klXi8p55UUM