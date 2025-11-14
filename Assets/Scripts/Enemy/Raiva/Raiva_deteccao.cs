using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
public class Raiva_deteccao : MonoBehaviour
{
    public float raioVisao = 30.0f;
    public LayerMask playerLayer;
    public Transform Player;
    void Awake()    
    {
        if (Player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                Player = playerObj.transform;
            }
        }
        
    }
    public bool detectarPlayer()
    {   
        if(Player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
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
        //Debug.Log("valor de detectado: "+ detectado);
        return detectado;

    }
   
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, raioVisao);
        
    }
}
