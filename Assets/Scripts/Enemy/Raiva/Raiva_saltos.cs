using UnityEngine;
using System.Collections;
public class Raiva_saltos : MonoBehaviour
{
    public float forcaY = 30f;// altura do pulo
    public float forcaX = 1.5f;// velocidade do salto
    //gravidade
    public float gravidade = 1.0f;//subindo
    public float gravidadeB = 30.0f;//caindo
    public Rigidbody2D rb;
    Raiva_deteccao rd;
    public Camera_Effects cf;// colcoar efeito de tremer
    public bool estouNoChao;

    [SerializeField] private AudioSource ataque;
    //player
    Transform Player;
    // inspetor para monitoramento
    private float Magnitude;// tem que ser igual ao magnitude
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        rd = GetComponent<Raiva_deteccao>();

    }

    // Update is called once per frame
    void Update()
    {

        if (rb.linearVelocityY >= 10)
        {
            quedaLivre();
        }
        
    }
    public void quedaLivre()
    {
        rb.gravityScale = gravidadeB;
    }
    public void saltar()
    {

        if (estouNoChao && rd.detectarPlayer())
        {
            // ultima posição tem que receber player .position do deteccao
            //ultimaPosicao = Player.position;

            Debug.Log("posição player: " + Player.position);
            Vector2 direcao = ((Vector2)Player.position - (Vector2)transform.position).normalized;
            // essa float é a força necessaria pra chegar no player

            direcao.y += forcaY * 0.1f;
            direcao = direcao.normalized;
            // AddForce em vez de velocity
            // Usa forcaX 
            rb.AddForce(direcao * forcaX * rb.mass, ForceMode2D.Impulse);
            ataque.Play();
            estouNoChao = false;
        }




    }

    private void Impacto()
    {

        Rigidbody2D rbPlayer = Player.GetComponent<Rigidbody2D>();
        if (rbPlayer == null) 
        {
            Debug.Log("Player é null no impacto");
            return;
        }
        rbPlayer.linearVelocity = Vector2.zero;
        Vector2 direcao = Vector2.up.normalized;
        Magnitude = 5.0f;
        rbPlayer.AddForce(direcao * Magnitude, ForceMode2D.Impulse);
        // if(Random.Range(0,2)== 0)
        // {
        //     Vector2 direcaoPlayer = (Player.position - transform.position).normalized;
        //     Magnitude = 15.0f;
        //     rbPlayer.AddForce(direcaoPlayer * Magnitude, ForceMode2D.Impulse);
            
        // }
        // else
        // {
        //     // alternativa que faz o movimento aleatorio
        //     if (rbPlayer != null)
        //     {
        //         Vector2 direcao = Random.insideUnitCircle.normalized;
        //         Magnitude = 5.0f;
        //         rbPlayer.AddForce(direcao * Magnitude, ForceMode2D.Impulse);
        //     }
        // }
      
        
    }
    
    // shake ao cair no chao
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Chao"))
        {
            // Se estava no ar e caindo com velocidade significativa
            if (!estouNoChao && rb.linearVelocity.y < -9f)
            {
                cf.shake(0.5f);
                rb.linearVelocity = Vector2.zero;
                Impacto();// aplicando no momento de queda
            }

            estouNoChao = true; // Agora está no chão
        }
        if (other.CompareTag("Player"))
        {
            Player_Status ps = other.GetComponent<Player_Status>();
            if (ps != null)
            {
                ps.LevarDano(7);
            }
            if(!estouNoChao && rb.linearVelocity.y <= 0f)
            {
                //Impacto();// aplicando no momento de queda // gerou conflito com o knockback
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, true);
                //coroutine pra reabiliaar a colisão
                StartCoroutine(Oncollision(other));
            }
            

        }   
        
    }
    
       private void OnCollisionStay2D(Collision2D Collision)
    {
        if (Collision.gameObject.layer == LayerMask.NameToLayer("Chao"))
        {
            estouNoChao = true;
            
            rb.gravityScale = gravidade;
        }

        
    }
    private void OnCollisionExit2D(Collision2D Collision)
    {
        if (Collision.gameObject.layer == LayerMask.NameToLayer("Chao"))
        {
            estouNoChao = false;
        }

    }
    //tentando impedir de ficar em cima do player
    
    IEnumerator Oncollision(Collider2D playerCollider)
    {
        yield return new WaitForSeconds(0.5f);
        
        // Garante que reabilita
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), playerCollider, false);
        
    }

}
