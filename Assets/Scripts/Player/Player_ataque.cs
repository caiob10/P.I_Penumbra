using UnityEngine;
using System.Collections;
public class Player_ataque : MonoBehaviour
{
    float alcance = 3.5f;// tamanho do raycast
    Vector2 dir; // direção do raycast
    Vector2 origem; // origem do raycast
    public bool podeAtacar = true;// limitar spaw de ataque
    public float tempoEntreAtaques = 1.0f;// permite o ajsute do ataque via inspetor
    public float cooldownTimer;// timer para o cooldown que sera referenciado no UI
    // Update is called once per frame
    Animator ani;
    [SerializeField] private AudioSource ataqueUmSom;
    [SerializeField] private LayerMask enemyLayers;

    
    void Start()
    {
        ani = GetComponent<Animator>();
        cooldownTimer = tempoEntreAtaques;// inicia cheio
    }
    void Update()
    {
        // essa situação if serve para o raycast disparar para onde o player olhar
        // isso atualiza a direção do raicast antes dele ser aplicado no mouse
        if (transform.localScale.x > 0)
        {
            dir = Vector2.right;
        }
        else
        {
            dir = Vector2.left;
        }
        origem = new Vector2(transform.position.x + dir.normalized.x * 1.0f, transform.position.y + 1.0f); // deslocamento de 3 unidades para frente
        // efeito visual do cooldown
        if (!podeAtacar)
        {
            cooldownTimer += Time.deltaTime;
        }
       
        // impedir que dispare durante a leitura e pulo futuramente...
        if (Input.GetMouseButtonDown(0))
        {   
            if (!podeAtacar)
            {
                return;// if reduzido
            } 
            ani.SetTrigger("ataque");// dispara a animação de ataque
            ataqueUmSom.Play();
            RaycastHit2D melee = Physics2D.Raycast(origem, dir, alcance, enemyLayers);// disparo do raycast
            
            
            // os inimigos tem tags diferentes, então é preciso checar cada um
            // isso permite atacar mais de um inimigo por vez, se estiverem juntos
            // como cada personagem é unico, o sitema de vida e dano também é unico
            // então é preciso pegar o script de cada um
            if (melee.collider != null)
            {
                Debug.Log("Collider tag: " + melee.collider.tag);
                Debug.Log("Collider name: " + melee.collider.name);

                if (melee.collider.CompareTag("Medo"))
                {
                    Medo_Vida medo = melee.collider.GetComponent<Medo_Vida>();
                    Debug.Log("medo atingido");
                    //aqui entra a função de dano
                    if (medo != null)
                    {
                        Player_knockBack pk = GetComponent<Player_knockBack>();
                        pk.AlvoKnockBack(melee.collider.gameObject);
                        pk.hitflash(melee.collider.GetComponent<SpriteRenderer>());
                        medo.LevarDano(8.5f);// aplica 7.5 na varivel dano
                    }
                }
                if (melee.collider.CompareTag("Raiva"))
                {
                    Raiva_status raiva = melee.collider.GetComponent<Raiva_status>();
                    Debug.Log("raiva atingido");
                    //aqui entra a função de dano
                    if (raiva != null)
                    {
                        Player_knockBack pk = GetComponent<Player_knockBack>();
                        pk.AlvoKnockBack(melee.collider.gameObject);
                        pk.hitflash(melee.collider.GetComponent<SpriteRenderer>());
                        raiva.LevarDano(9.8f);// aplica 9.8 na varivel dano
                    }
                }
                if (melee.collider.CompareTag("Stalker"))
                {
                    Enemy_Vergonha vergonha = melee.collider.GetComponent<Enemy_Vergonha>();
                    Debug.Log("vergonha atingido");
                    //aqui entra a função de dano
                    if (vergonha != null)
                    {
                        Player_knockBack pk = GetComponent<Player_knockBack>();
                        pk.AlvoKnockBack(melee.collider.gameObject);
                        pk.hitflash(melee.collider.GetComponent<SpriteRenderer>());
                        vergonha.LevarDano(9.8f);// aplica 9.8 na varivel dano
                    }
                }
                if (melee.collider.CompareTag("carente"))
                {
                    Carente_movimento carente = melee.collider.GetComponent<Carente_movimento>();
                    Debug.Log("carente atingido");
                    //aqui entra a função de dano
                    if (carente != null)
                    {
                        Player_knockBack pk = GetComponent<Player_knockBack>();
                        pk.AlvoKnockBack(melee.collider.gameObject);
                        pk.hitflash(melee.collider.GetComponent<SpriteRenderer>());
                        //carente não pode receber dano
                    }
                }
                
            }
            StartCoroutine(proximoAtaque());
        }
    }
    IEnumerator proximoAtaque()
    {
        podeAtacar = false;
        cooldownTimer = 0f;
        yield return new WaitForSeconds(tempoEntreAtaques);
        cooldownTimer = tempoEntreAtaques;// força o slider a encher 100%
        podeAtacar = true;
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        // desenha a linha do raio
        Gizmos.DrawLine(origem, origem + dir * alcance);
        
    }
}
