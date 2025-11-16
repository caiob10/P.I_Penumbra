using UnityEngine;
using System.Collections;

public class Vergonha_Correr : MonoBehaviour
{
    [Header("Referências para player")]
    public Transform Player;
    public float distanciaMaxima = 1.1f;
    public float velocidade = 5f;

    // Componentes
    private Player_Movement playerM;
    private Vergonha_ciclos vCiclos;
    private Vergonha_animation vAnimation;
    private Collider2D colliderCorpo;
    private Collider2D playerCollider;

    // Controle interno
    [HideInInspector] public bool estaCorrendo;
    [HideInInspector] public bool terminouCorrida;
    [SerializeField] private AudioSource ataque;


    void Awake()
    {
        // Configura Player e collider
        if (Player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                Player = playerObj.transform;
            }
                    
        }

        if (Player != null)
        {
            playerM = Player.GetComponent<Player_Movement>();
            playerCollider = Player.GetComponent<Collider2D>();
        }

        // Componentes do inimigo
        vAnimation = GetComponent<Vergonha_animation>();
        vCiclos = GetComponent<Vergonha_ciclos>();
        colliderCorpo = GetComponent<Collider2D>();

     
    }

    public IEnumerator Correr(float tempo)
    {
        // caso Player não exista
        if (Player == null || playerCollider == null)
        {
            yield break;
        }
            

        estaCorrendo = true;
        terminouCorrida = false;

        // Inicia animação de corrida
        vAnimation.SetCorrer(1f);
        ataque.Play();
        Vector2 pontoAlvo = (Vector2)Player.position - ((Vector2)Player.position - (Vector2)transform.position).normalized * distanciaMaxima;

        float tempoDecorrido = 0f;
        while (!colliderCorpo.bounds.Intersects(playerCollider.bounds) && Vector2.Distance(transform.position, pontoAlvo) > 0.1f)
        {
            tempoDecorrido += Time.deltaTime;
            // se nao der tempo, ele para e reinicia
            if (tempoDecorrido >= tempo)
            {
                Debug.Log("⏱️ Tempo acabou — reiniciando ciclo...");
                vCiclos.executandoEstado = false;
                vCiclos.estadoAtual = Vergonha_ciclos.Estado.Reiniciar;

                break;
            }
            pontoAlvo = (Vector2)Player.position - ((Vector2)Player.position - (Vector2)transform.position).normalized * distanciaMaxima;

            // Movimento
            Vector2 direcao = ((Vector2)Player.position - (Vector2)transform.position).normalized;
            //transform.position = Vector2.MoveTowards(transform.position,(Vector2)Player.position, velocidade * Time.deltaTime);
            transform.position = Vector2.MoveTowards(transform.position, pontoAlvo, velocidade * Time.deltaTime);
            // Flip do sprite (originalmente olhando para a esquerda)
            if (Player.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            }
            else
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            }

            yield return null;
        }

        // Finaliza corrida
        vAnimation.SetCorrer(0f);
        estaCorrendo = false;
        terminouCorrida = true;

        Debug.Log("Corrida finalizada!");
    }

    public void PararCorrida()
    {
        StopAllCoroutines();
        vAnimation.SetCorrer(0f);
        estaCorrendo = false;
        terminouCorrida = true;
    }
    public IEnumerator Fugir(float tempo)
    {
        float tempoDecorrido = 0;
        while(tempo>=tempoDecorrido)
        {
            tempoDecorrido += Time.deltaTime;
            Vector2 direcao = ((Vector2)transform.position - (Vector2)Player.position).normalized;
            // essa float é a força necessaria pra chegar no player
            Vector2 pontoSeguro = (Vector2)Player.position + direcao  * 5.0f;// ponto final do movimento
            transform.position = Vector2.MoveTowards(transform.position, pontoSeguro, velocidade * Time.deltaTime);
            yield return null;
        }
        
    }
}
