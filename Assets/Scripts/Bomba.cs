using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    public float radio=0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void destruir()
    {
        Collider2D[] collider2D =Physics2D.OverlapCircleAll(transform.position, radio);
        
        for(int i=0;i<collider2D.Length;i++)
            if(collider2D[i].gameObject.tag=="Enemigo")
            {
                Debug.Log("Destruir Enemigo");
                EnemigoPirata enemigoPirata = collider2D[i].gameObject.GetComponent<EnemigoPirata>();
                enemigoPirata.explosionBomba();

            }

        Destroy(this.gameObject);
    }
}
