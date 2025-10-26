using UnityEngine;

public class Raiva_status : MonoBehaviour
{
    public float vida = 120f;

    public void LevarDano(float danoRecebido)
    {
        vida -= danoRecebido;
        Debug.Log("Raiva levou dano, vida atual: " + vida);
        if (vida <= 0)
        {
            Morrer();
        }
    }
    void Morrer()
    {
        Debug.Log("Raiva morreu!");
        // Aqui você pode adicionar efeitos de morte, animações, etc.
        Destroy(gameObject);
    }
}
