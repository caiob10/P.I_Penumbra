using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Collections;

public class Vida_extra : MonoBehaviour

{
    //fade paragerar um blur vermelho por posicao
    public float raioVisao = 10.0f;
    
    public Transform Player;
    private bool ganhandoVida = false;
   
    public LayerMask layerPlayer;
    public Player_Status ps;
    Coroutine curandoCoroutine;
     void Awake()
    {
        if (Player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)//se conseguir pegar o player
            {
                Player = playerObj.transform;//localizacao
                ps = playerObj.GetComponent<Player_Status>(); 
                
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        if (Player == null)
        {
            Debug.LogError("Player não encontrado em " + gameObject.name);
            return;
        }
        if (ps == null)
        {
            Debug.LogError("Player_Status não encontrado em " + gameObject.name);
            return;
        }
        // confirmar se o player ta no circulo 
        //essa é a referencia de posição temporaria
        Collider2D col = Physics2D.OverlapCircle(transform.position, raioVisao, layerPlayer);
        //vai ser verdadeiro se col for diferente de nulo e a player tag estiver no overlapcircle
        bool playerDentro = col != null && col.CompareTag("Player");

        //playerdentro é verdadeiro e o player nao é nulo(isso é uma segurança)
         if (playerDentro && !ganhandoVida)
        {
            // Player ENTROU no raio - INICIAR cura
            ganhandoVida = true;
            curandoCoroutine = StartCoroutine(ps.AumentarVida());
            Debug.Log("Iniciando cura...");
        }
        else if (!playerDentro && ganhandoVida)
        {
            // Player SAIU do raio - PARAR cura
            ganhandoVida = false;
            if (curandoCoroutine != null)
            {
                StopCoroutine(curandoCoroutine);
                curandoCoroutine = null;
            }
            Debug.Log("Parando cura...");
        }
 
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, raioVisao);
    }
    
    
}
