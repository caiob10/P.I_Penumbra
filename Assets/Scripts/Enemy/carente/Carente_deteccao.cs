using UnityEngine;

public class Carente_deteccao : MonoBehaviour
{
    public float raioVisao = 35.0f;
    public LayerMask playerLayer;
    
    public bool detectarPlayer()
    {
        
        Collider2D col = Physics2D.OverlapCircle(transform.position, raioVisao, playerLayer);
        //é o player

        
        bool detectado = col != null && col.CompareTag("Player");
        
        if (col != null)
        {
            Debug.Log("col recebeu : "+ col.gameObject.name);
        }
        
        col = null;// to tentando garantir que a cada chamada, col esteja limpo e sem referencia
        if (col == null)
        {
            Debug.Log("col é null");
        }
        Debug.Log("valor de detectado: "+ detectado);
        return detectado;

    }
   
     void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, raioVisao);
        
    }
}
