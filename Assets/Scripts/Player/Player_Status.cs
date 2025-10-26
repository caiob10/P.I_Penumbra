using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Player_Status : MonoBehaviour
{
    [HideInInspector]public float vida = 100;

    Ui_Manager um;
  
    //private bool visão = true;

    public void LevarDano(float danoRecebido)
    {
        GameObject Umanager = GameObject.FindGameObjectWithTag("Umanager");
        if (Umanager != null)
        {
            um = Umanager.GetComponent<Ui_Manager>();
        }
        um.StartCoroutine(um.flashBlackLife());// vai piscar
        StartCoroutine(DiminuirVida(danoRecebido));
        Debug.Log("player levou dano, vida atual == " + vida);
        if (vida <= 0)
        {

            morreu();
            
        }
    }

    public IEnumerator DiminuirVida(float danoTotal)
    {
        float vidaAlvo = vida - danoTotal;
        while (vida > vidaAlvo)
        {

            vida -= 1f; // Diminui 1 por frame
            //nao ta certo , tenho que refazer
            //mas funciona de alguma forma...
            yield return null;
        }

        vida = vidaAlvo; // Valor da vida
        AtualizarCoresVida();
    }
    public IEnumerator AumentarVida()
    {
        // float vidaMaxima = 100f;
        // float velocidade = 0.5f;

        // while (ganhandoVida && vida < vidaMaxima) // <-- só aumenta enquanto o player estiver no raio
        // {
        //     vida = Mathf.Lerp(vida, vidaMaxima, velocidade * Time.deltaTime);

        //     if (vida > vidaMaxima) vida = vidaMaxima;

        //     AtualizarCoresVida();
        //     yield return null;
        // }
        // o de cima era a segunda opcao, carrega a vida cheia de uma vez porem gradualmente e precisa destruir o "spaw de vida pra garantir que nao tem vida infinita"
         float vidaMaxima = 100f;
    
        //  CORREÇÃO: Condição de parada clara
        while (vida < vidaMaxima)
        {
            vida += 1f * 4.5f * Time.deltaTime;
            
            // Limita a vida ao máximo
            if (vida > vidaMaxima) 
                vida = vidaMaxima;
            
            AtualizarCoresVida();
            yield return null; //  Dá uma pausa por frame
        }
        
        Debug.Log("Vida cheia! Cura concluída.");
    }
    public void AtualizarCoresVida()
    {
        if (vida >= 61)
        {
            um.vidaVerde();
        }
        if (vida <= 60)
        {
            um.vidalaranja();
        }
        if (vida <= 30)
        {
            um.StartCoroutine(um.flashVida());
        }
    }
    public void morreu ()
    {
        if(vida <= 0)
        {
            SceneManager.LoadScene("Game_over");
            Debug.Log("morreu");
        }
    }
}
