using UnityEngine;

public class Carente_movimento : MonoBehaviour
{
public float velocidade = 10.0f;
    public float altura = 10.0f;
    public float raioVisao = 5.0f;
    Rigidbody2D rb;
    public LayerMask playerLayer;
    Vector2 posicaoAtual;
    Vector2 posicaoPlayer;
    Transform Player;
    // sistema para animar o pulo
    public bool noChao;
    public bool caindo;
    float velY;
    Animator ani;
    bool noChaoAnterior; // para detectar aterrissagem

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        //olha em direção ao player
        if (Player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        //==============================================================
        //queda
        velY = rb.linearVelocityY;
        caindo = velY < -0.1f;

        // detecta se acabou de tocar o chão (aterrissagem)
        if (!noChaoAnterior && noChao)
        {
            ani.SetTrigger("Aterrissar");
        }

        // atualiza parâmetros do Animator
        ani.SetBool("Nochao", noChao);
        ani.SetBool("Caindo", caindo);

        // sistema para ajustar pulo
        if (!noChao && !caindo)
        {
            ani.SetTrigger("Pulando");
        }
        
        //=========================================================================

        // detecta o player dentro do raio de visão
        Collider2D col = Physics2D.OverlapCircle(transform.position, raioVisao, playerLayer);
        bool playerDentro = col != null && col.CompareTag("Player");
        
        // Só persegue o player se não estiver na animação de aterrissagem
        bool podePerseguir = !ani.GetCurrentAnimatorStateInfo(0).IsName("encostouNoChao");

        if (playerDentro && podePerseguir)
        {
            posicaoAtual = transform.position;
            posicaoPlayer = Player.position;

            // Ponto alto do salto (meio do caminho + altura)
            Vector2 pontoAlto = (posicaoAtual + posicaoPlayer) / 2;
            pontoAlto.y += altura;

            // Move em arco
            transform.position = Vector2.MoveTowards(transform.position, pontoAlto, velocidade * Time.deltaTime);
        }

        // atualiza histórico do chão
        noChaoAnterior = noChao;
    }
    
    void OnCollisionStay2D(Collision2D Collision)
    {
        if (Collision.gameObject.CompareTag("Plataforma") || Collision.gameObject.layer == LayerMask.NameToLayer("Chao"))
        {
            noChao = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Plataforma") || collision.gameObject.layer == LayerMask.NameToLayer("Chao"))
        {
            noChao = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, raioVisao);
    }
}
