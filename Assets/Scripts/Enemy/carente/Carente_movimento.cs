using UnityEngine;
using System.Collections;
public class Carente_movimento : MonoBehaviour
{
    float forcaY = 2.5f;// altura do pulo
    float forcaX = 1.8f;// velocidade 
    //gravidade
    public float gravidade = 1.0f;//subindo
    public Rigidbody2D rb;
    Carente_deteccao Cdeteccao;
    Animator ani;
    public bool estouNoChao;
    public bool possoAndar = false;
    //player
    Transform Player;
    
    // inspetor para monitoramento  
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Cdeteccao = GetComponent<Carente_deteccao>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
   
    
    public void saltar()
    {
        if (possoAndar)
        {
            if (Player == null)
            {
                
                Debug.Log("Player é null quando tentou checar ladoAlado");
                return;
            }
            if (estouNoChao && Cdeteccao.detectarPlayer())
            {
                // virar para a direção do player
                if (Player.position.x > transform.position.x)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

                }
                else
                {
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

                }
                // ultima posição tem que receber player .position do deteccao
                //ultimaPosicao = Player.position;

                Debug.Log("posição player: " + Player.position);
                Vector2 direcao = ((Vector2)Player.position - (Vector2)transform.position).normalized;
                // essa float é a força necessaria pra chegar no player
                direcao = direcao.normalized;
                // AddForce em vez de velocity
                // Usa forcaX
                //rb.AddForce(new Vector2(forcaX * direcao.x, forcaY) * rb.mass, ForceMode2D.Impulse);
                rb.linearVelocity = new Vector2(forcaX * direcao.x, forcaY);
                estouNoChao = false;
                ladoAlado();
            }
        
        }
    }
    public bool ladoAlado()
    {
       

        float distanciaX = Mathf.Abs(Player.position.x - transform.position.x);
        if(distanciaX <= 2.5f)
        {
            Debug.Log("Lado a lado com o player");
            return true;
        } 
        else
        {
            return false;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("Player"))
        {
            //carente não pode caudar dano 
            // Player_Status ps = other.GetComponent<Player_Status>();
            // if (ps != null)
            // {
            //     ps.LevarDano(3);
            // }
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
