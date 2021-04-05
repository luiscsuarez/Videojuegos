using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float speed;
    private Rigidbody rb;

    public Transform particulas;
    public Transform particulasTrampa;
    private ParticleSystem systemaParticulas;
    private Vector3 posicion;

    private int puntaje = 0;

    public GameObject plataforma;
    public GameObject sonidoRecolecta;
    public GameObject sonidoPlataformas;
    public GameObject sonidoChoque;
    public GameObject gameOver;

    private double tiempo = 0.0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        //Para obtener el sistema de particulas y detenerlo 
        systemaParticulas = particulas.GetComponent<ParticleSystem>();
        systemaParticulas.Stop();

        systemaParticulas = particulasTrampa.GetComponent<ParticleSystem>();
        systemaParticulas.Stop();

    }

    // Update is called once per frame
    void Update()
    {
        tiempo += Time.deltaTime;

        var high = transform.position;
        //Debug.Log(high.y);
        if (high.y <= 0.5 && high.y >= 0.4 ){
            Destroy(plataforma.gameObject);
            Instantiate(sonidoPlataformas);
        }
        if (high.y <= 0)
        {
            SceneManager.LoadScene("ProyectoLaberintoNivel2");
        }
        else
        {
            
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
            Debug.Log("Tiempo Anterior: "+tiempo.ToString("0.0"+" Segundos"));
            Instantiate(sonidoRecolecta);
            //Localiza el sistema de particulas y lo inicia a reproducir.
            posicion = other.gameObject.transform.position;
            particulas.position = posicion;
            systemaParticulas = particulas.GetComponent<ParticleSystem>();
            systemaParticulas.Play();
           
            //Hace inactivo el objeto (Se oculta)
            other.gameObject.SetActive(false);
            
            //Destruye objeto y lo libera en la memoria
            Destroy(other.gameObject);

            puntaje++;

            Debug.Log("Tu Puntaje es: "+puntaje*10+"\n Has sumado: "+(puntaje/puntaje)*10+" puntos y ganado 1 segundo");
            tiempo--;
            if (tiempo < 0){
                tiempo = 0;
            }
            Debug.Log("Nuevo tiempo: "+tiempo.ToString("0.0"+" Segundos"));
            
        }
        if (other.gameObject.CompareTag ("Trap"))
        {
            Instantiate(gameOver);
            posicion = other.gameObject.transform.position;
            particulasTrampa.position = posicion;
            systemaParticulas = particulasTrampa.GetComponent<ParticleSystem>();
            systemaParticulas.Play();
            gameObject.SetActive(false);
            Debug.Log("Tu Puntaje es: "+puntaje*10+"\n Has sumado: "+(puntaje/puntaje)*10+" puntos");
            Debug.Log("Game Over");
        }
        else
        {
            
        }
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Wall")){
            Instantiate (sonidoChoque);
        }
    }

}
