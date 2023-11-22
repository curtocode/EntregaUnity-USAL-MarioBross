using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    
    public GameObject[] vidas;
    // Update is called once per frame
    void Update()
    {
        
    }
    public void desactivarVida(int indice)
    {

        vidas[indice].SetActive(false);
    }
    public void activarVida(int indice)
    {
        vidas[indice].SetActive(true);
    }
}
