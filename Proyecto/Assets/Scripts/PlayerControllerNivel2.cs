using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerNivel2 : MonoBehaviour
{

    public float speed;
    private Rigidbody rb;

    public Transform particulas;
    public Transform particulasTrampa;
    private ParticleSystem systemaParticulas;
    private Vector3 posicion;

    private int puntaje = 0;

    public GameObject sonidoRecolecta;
    public GameObject sonidoChoque;
    public GameObject gameOver;
    public GameObject camara;

    private double tiempo = 0.0;

    // Start is called before the first frame update
    void Start()
    {
        int nRandom = Random.Range(0,4);
        //Debug.Log("Valor Random: "+nRandom);

        Vector3 cam = new Vector3 (0,3,0);

        if (nRandom==0)
        {
            Vector3 pos0 = new Vector3 (0,3,-9);
            transform.position = pos0;
            camara.transform.position = cam+pos0;
        }
        if (nRandom==1)
        {
            Vector3 pos1 = new Vector3 (-9.5f,3,-9.5f);
            transform.position = pos1;
            camara.transform.position = cam+pos1;
        }
        if (nRandom==2)
        {
            Vector3 pos2 = new Vector3 (9.5f,3,-9.5f);
            transform.position = pos2;
            camara.transform.position = cam+pos2;
        }
        if (nRandom==3)
        {
            Vector3 pos3 = new Vector3 (-9.5f,3,9.5f);
            transform.position = pos3;
            camara.transform.position = cam+pos3;
        }
        if (nRandom==4)
        {
            Vector3 pos4 = new Vector3 (9.5f,3,9.5f);
            transform.position = pos4;
            camara.transform.position = cam+pos4;
        }

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
