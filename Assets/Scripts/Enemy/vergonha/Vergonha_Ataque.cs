using UnityEngine;
using System.Collections;
public class Vergonha_Ataque : MonoBehaviour
{
    [Header("Configurações do ataque")]
    public float alcance = 5.5f; // distância do raycast
    public float deslocamento = 3.5f; // deslocamento do ponto de origem
    public float tempoEntreAtaques = 1f; // delay opcional entre ataques

    private Vector2 dir; // direção do ataque
    private Vector2 origem; // origem do raycast

    

    Vergonha_animation vAnimation;
    Vergonha_Knockback vKnockback;
    BoxCollider2D[] colliders;
    void Awake()
    {
        colliders = GetComponents<BoxCollider2D>();
        vAnimation = GetComponent<Vergonha_animation>();
        vKnockback = GetComponent<Vergonha_Knockback>();
    }
    public IEnumerator Atacar()
    {
        foreach (BoxCollider2D col in colliders)
        {
            col.enabled = false;
        }
        // Define direção baseada na escala do inimigo
        if (transform.localScale.x > 0)
        {
            dir = Vector2.left;
        }
            
        else
        {
           dir = Vector2.right;
        }
            

        // Define origem do raycast
        origem = new Vector2(transform.position.x + dir.normalized.x * 1.0f, transform.position.y + deslocamento );
        //vAnimation.PlayChutar();
        // Ativa animação de chute
        
        // Dispara raycast imediatamente (pode ajustar para delay via coroutine se quiser sincronizar com frame da animação)
        RaycastHit2D melee = Physics2D.Raycast(origem, dir, alcance);
        
        if (melee.collider != null)
        {
            if (melee.collider.CompareTag("Player"))
            {
                Transform playerTransform = melee.collider.GetComponent<Transform>();
                Player_Status ps = melee.collider.GetComponent<Player_Status>();
                if (playerTransform != null)
                {
                    Vector2 direcao = ((Vector2)playerTransform.position - (Vector2)transform.position).normalized;// variavel que define a direção localmente
                    Debug.Log("Atingiu o player");
                    vKnockback.StartCoroutine(vKnockback.knockback(direcao, 0.5f));

                    ps.LevarDano(23);// bicuda sinistra
                    // knockback, dano, efeitos, etc.
                }

            }

        }
        foreach (BoxCollider2D col in colliders)
        {
            col.enabled = true;
        }
        yield return null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(origem, origem + dir * alcance);
    }
}
