using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour {

	public float velocidad=5f;
	public float salto=15f;
    public HUD hud;

	protected Rigidbody2D rigidBody2D;
	protected Animator animator;
	protected Collider2D collider2d;
    public GameObject gameObjectBomba;
    protected AudioSource sonidoSalto;
    protected AudioSource vida,perder;
    protected bool disparar = false;
    private int vidas= 3;

	// Use this for initialization
	void Start () {
	
		rigidBody2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		collider2d = GetComponent<Collider2D>();
        AudioSource[] audioSources = GetComponents<AudioSource>();
        sonidoSalto = audioSources[0];
        vida = audioSources[1];
        perder = audioSources[2];
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxisRaw("Fire1") != 0)
        {
            if (disparar == false)
            {
                // Call your event function here.
                disparar = true;

                Instantiate(gameObjectBomba, transform.position, Quaternion.identity);
            }
        }
        if (Input.GetAxisRaw("Fire1") == 0)
        {
            disparar = false;
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el jugador choca con un objeto llamado "ganar"
        if (other.CompareTag("ganar"))
        {
            Debug.Log("Ganar");
            GanarPartida();
        }
    }

    void FixedUpdate()
	{
		rigidBody2D.velocity = new Vector2(velocidad * Input.GetAxis("Horizontal"), rigidBody2D.velocity.y);
		animator.SetBool("correr", rigidBody2D.velocity.x!=0);

		//roto al personaje para es lo escalo a -1 o lo giro 180 en el eje y
		if (rigidBody2D.velocity.x < 0)
			transform.rotation = Quaternion.Euler(0, 180, 0);
		else if(rigidBody2D.velocity.x > 0)
			transform.rotation = Quaternion.Euler(0, 0,0);

        //saltar arriba
        if (rigidBody2D.velocity.y > 0 && Physics2D.OverlapBox(transform.GetChild(0).position, new Vector2((collider2d.bounds.max.x - collider2d.bounds.min.x) / 2f, 0.01f), 0) == null)
        {
            animator.SetBool("saltar", true);
            sonidoSalto.Play();
        }
        //cayendo
        else if(rigidBody2D.velocity.y<0 && animator.GetBool("saltar"))
        {
            //animator.SetBool("saltar", false);
            animator.SetBool("caer", true);
        }
        //toco tierra
        //con animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "fall" nos aseguramos
        //que siempre se reproduce algo la animación de fall antes de poner el trigger de caer tierra porque 
        //si se activa el triger de caer tierra antes de empezar con la animación de caer ya nos 
        //quedaríamos pillados
        else if (animator.GetBool("caer") && animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "fall" && Physics2D.OverlapBox(transform.GetChild(0).position, new Vector2((collider2d.bounds.max.x - collider2d.bounds.min.x) / 2f, 0.01f), 0) != null)
        {
            animator.SetBool("saltar", false);
            animator.SetBool("caer", false);
            animator.SetTrigger("caerTierra");
        }

        //el overlapbox se hace la mitad del collider para evitar que roce en lateral con una plataforma y me deje saltar de nuevo, (collider2d.bounds.max.x-collider2d.bounds.min.x)/2
        if (Physics2D.OverlapBox(transform.GetChild(0).position, new Vector2((collider2d.bounds.max.x - collider2d.bounds.min.x) / 2f, 0.01f), 0) != null)
        {
            //Debug.Log("SAlto " + Time.time);
            if (Input.GetAxis("Jump")!=0)
            {
				rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, 0);
                rigidBody2D.AddForce(new Vector2(30, salto), ForceMode2D.Impulse);
                Debug.Log("SAlto " + Time.time);
            }
        }
	}
    public void GanarPartida()
    {
        SceneManager.LoadScene("Ganar");
    }
    public void perderVida()
    {
        vidas = vidas - 1;
        if (vidas == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        hud.desactivarVida(vidas);
        vida.Play();
        
    }
   
}
