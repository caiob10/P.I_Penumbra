using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Collections;

public class Fire_Manager : MonoBehaviour

{
    //fade paragerar um blur vermelho por posicao
    public float raioVisao = 3.0f;
    private float alphaAlvo = 0; // recebe o alpha por distancia do player(quando criei localmente, causava erro por frame)
    private float alphaAtual = 0;// é a variavel que recebera o alpha
    private Transform Player;
    private Image Panel;
    private  float velocidade = 2.0f;
    public LayerMask layerPlayer;
    
    private Rigidbody2D playerRb;
    Player_Status ps;
    
    // variavel para empurrar o player pra longe da fogueira
    Player_Movement pm;
    float empurrao = 7.0f;
    float travamentoTempo = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
        
        //Panel = GetComponent<Image>(); só pode usar se for um componente do proprio objeto
        if (Panel == null)
        {
            GameObject panelObj = GameObject.FindGameObjectWithTag("Fogueira");

            if (panelObj != null)
            {
                Panel = panelObj.GetComponent<Image>();
                
            }
        }

        if (Player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)//se conseguir pegar o player
            {
                Player = playerObj.transform;//localizacao
                playerRb = playerObj.GetComponent<Rigidbody2D>();//rigidbody2d
                
            }
        }
    }
    void Start()
    {




        Panel.color = new Color(1, 0, 0, 0);//uma garantia que sera vermelho no inicio do jogo


    }

    // Update is called once per frame
    void Update()
    {

        // confirmar se o player ta no circulo 
        //essa é a referencia de posição temporaria
        Collider2D col = Physics2D.OverlapCircle(transform.position, raioVisao, layerPlayer);
        //vai ser verdadeiro se col for diferente de nulo e a player tag estiver no overlapcircle
        bool playerDentro = col != null && col.CompareTag("Player");

        //playerdentro é verdadeiro e o player nao é nulo(isso é uma segurança)
        if (playerDentro && Player != null)
        {
            //como é verdadeiro, pegamos a distancia da fogueira e do player
            float alphaDistancia = Vector2.Distance(transform.position, Player.position);
            Debug.Log("Distancia até player: " + alphaDistancia);
            // primeiro uso de operador ternario(um if resumido)
            alphaAlvo = (alphaDistancia <= raioVisao) ? Mathf.Clamp01(1 - (alphaDistancia / raioVisao)) : 0f;
            float alphaAtual = Panel.color.a;// pegar valor atual do alpha

            float novoAlpha = Mathf.MoveTowards(alphaAtual, alphaAlvo, velocidade * Time.deltaTime); // com o MoveTowards estou levando em consideração a posição A para B * valor
            Panel.color = new Color(1, 0, 0, novoAlpha);
        
            Debug.Log("Alvo calculado: " + alphaAlvo);
            return; 
        }
       
       
        
        

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ps = other.GetComponent<Player_Status>();
            pm = other.GetComponent<Player_Movement>();

            Debug.Log("pegoufogo");
            // -1 de vida
            Debug.Log(ps.vida);
            ps.vida -= 10;
            Debug.Log(ps.vida);
            if (ps.vida <= 0)
            {
                ps.morreu();
            }
            
            //pegar um vetor que sera o player - a fogueira , isso sera a direção
            Vector2 direcao = (other.transform.position - transform.position).normalized;
            
            StartCoroutine (knockback(direcao,travamentoTempo));
        }   
    }

    IEnumerator knockback(Vector2 direcao, float travamentoTempo)
    {
        //zerar o movimento do player vai impedir de adicionar outra força antes de completar o ciclo
        playerRb.linearVelocity = Vector2.zero;
        // aqui dentro vai ser garantido que o personagem continuara sendo repelido 
        //adiciona uma força (lembrando que é a posicao - a posicao do outro)
        playerRb.AddForce(direcao * empurrao, ForceMode2D.Impulse);
        pm.enabled = false;
        yield return new WaitForSeconds(travamentoTempo);
        pm.enabled = true;
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, raioVisao);
    }
    
    
}
