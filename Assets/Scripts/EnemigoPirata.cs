using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoPirata : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Obtiene una referencia al componente Jugador
            Jugador jugador = other.gameObject.GetComponent<Jugador>();

            // Asegúrate de que el componente Jugador existe
            if (jugador != null)
            {
                if (!GetComponent<Animator>().GetBool("morir"))// compruebo si el enemigo esta muerto
                {
                    // Llama a la función perderVida del jugador
                    jugador.perderVida();
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    public void explosionBomba()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("morir", true);
    }

    public void destruir()
    {
        Destroy(this.gameObject);
    }
}
