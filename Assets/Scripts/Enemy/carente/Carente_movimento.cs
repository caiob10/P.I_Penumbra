using UnityEngine;
using System.Collections;
public class Carente_movimento : MonoBehaviour
{
    public float forcaY = 3f;// altura do pulo
    public float forcaX = 1.5f;// velocidade do salto
    //gravidade
    public float gravidade = 1.0f;//subindo
    public Rigidbody2D rb;
    Carente_deteccao Cdeteccao;
    Animator ani;
    public bool estouNoChao;
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

        if (estouNoChao && Cdeteccao.detectarPlayer())
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
            estouNoChao = false;
        }
    }
   
    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("Player"))
        {
            Player_Status ps = other.GetComponent<Player_Status>();
            if (ps != null)
            {
                ps.LevarDano(3);
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
