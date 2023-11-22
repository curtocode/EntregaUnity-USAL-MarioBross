using UnityEngine;
using System.Collections;

public class MovimientoEnemigo : MonoBehaviour
{
    //con esta notación también se puede asignar el 
    //campo en el interfaz gráfico
    [SerializeField]
    protected Transform origen;
    [SerializeField]
    protected Transform destino;


    public float velocidad = 3;
    protected Rigidbody2D rigid;
    protected bool invertir = false;
    protected Vector2 posicionInicial;
    protected Vector2 posicionOrigen;
    protected Vector2 posicionDestino;

    private void Start()
    {
        rigid = GetComponentInChildren<Rigidbody2D>();
        posicionInicial = transform.GetChild(2).transform.position;
        posicionOrigen = origen.transform.position;
        posicionDestino = destino.transform.position;
    }

    //siempre se dibuja la línea
    void OnDrawGizmos()
    {
        if (origen != destino)
        {
            // Draws a blue line from this transform to the target
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(origen.position, destino.position);
        }
    }


    //sólo se dibujan las esferas si el game object está seccionado
    void OnDrawGizmosSelected()
    {
        if (origen != destino)
        {
            // Draws a blue line from this transform to the target
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(origen.position, 0.5f);
            Gizmos.DrawWireSphere(destino.position, 0.5f);
        }
    }


    //sin físicas usar Vector3.MoveTowards
    private void FixedUpdate()
    {
        if (rigid != null)
        { 
        if(!invertir)
        {
            rigid.velocity= new Vector2(transform.position.x<posicionDestino.x? velocidad:-velocidad, rigid.velocity.y);

            Debug.Log("distancia " + Mathf.Abs(transform.position.x - posicionDestino.x));
            if (Mathf.Abs(transform.GetChild(2).transform.position.x-posicionDestino.x) < 0.1)
                invertir = true;
        }
        else
        {
            rigid.velocity = new Vector2(transform.position.x > posicionOrigen.x ? -velocidad : velocidad, rigid.velocity.y);

            if (Mathf.Abs(transform.GetChild(2).transform.position.x - posicionOrigen.x) < 0.1)
                invertir = false;
        }

        //roto al personaje para es lo escalo a -1 o lo giro 180 en el eje y
        if (rigid.velocity.x < 0)
            transform.GetChild(2).transform.rotation = Quaternion.Euler(0, 180, 0);
        else if (rigid.velocity.x > 0)
            transform.GetChild(2).transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}