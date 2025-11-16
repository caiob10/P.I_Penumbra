using UnityEngine;

public class Medo_Vida : MonoBehaviour
{
    // [HideInInspector]
    public float vida = 100;
    //private bool visão = true;
    public bool medoMorto = false;

    public void LevarDano(float danoRecebido)
    {
        
        vida -= danoRecebido;
        Debug.Log("medo levou dano, vida atual == " + vida);
        if (vida <= 0)
        {
            morreu();
        }
    }
    public void morreu ()
    {
        if(vida <= 0)
        {
            Debug.Log("medo_morreu");
            medoMorto = true;
            Destroy(gameObject);
        }
    }
}
