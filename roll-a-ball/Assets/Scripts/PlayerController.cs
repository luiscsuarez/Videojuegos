using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    private Rigidbody rb;

    public Transform particulas;
    public Transform particulasAmarillas;
    public Transform particulasRojas;
    private ParticleSystem systemaParticulas;
    private Vector3 posicion;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        //Para obtener el sistema de particulas y detenerlo 
        systemaParticulas = particulas.GetComponent<ParticleSystem>();
        systemaParticulas.Stop();

        systemaParticulas = particulasAmarillas.GetComponent<ParticleSystem>();
        systemaParticulas.Stop();

        systemaParticulas = particulasRojas.GetComponent<ParticleSystem>();
        systemaParticulas.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            //Localiza el sistema de particulas y lo inicia a reproducir.
            posicion = other.gameObject.transform.position;
            particulas.position = posicion;
            systemaParticulas = particulas.GetComponent<ParticleSystem>();
            systemaParticulas.Play();
           
            //Hace inactivo el objeto (Se oculta)
            other.gameObject.SetActive(false);
            
            //Destruye objeto y lo libera en la memoria
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag ("RecolectableAmarillo"))
        {
            //Localiza el sistema de particulas y lo inicia a reproducir.
            posicion = other.gameObject.transform.position;
            particulasAmarillas.position = posicion;
            systemaParticulas = particulasAmarillas.GetComponent<ParticleSystem>();
            systemaParticulas.Play();
           
            //Hace inactivo el objeto (Se oculta)
            other.gameObject.SetActive(false);
            
            //Destruye objeto y lo libera en la memoria
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag ("RecolectableRojo"))
        {
            //Localiza el sistema de particulas y lo inicia a reproducir.
            posicion = other.gameObject.transform.position;
            particulasRojas.position = posicion;
            systemaParticulas = particulasRojas.GetComponent<ParticleSystem>();
            systemaParticulas.Play();
           
            //Hace inactivo el objeto (Se oculta)
            other.gameObject.SetActive(false);
            
            //Destruye objeto y lo libera en la memoria
            Destroy(other.gameObject);
        }
        else
        {
            
        }
    }
}
