using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Medo_Investida : MonoBehaviour
{
    // interações entre scripts do proprio inimigo
    Medo_ciclo mc;
    // referencia pro player 
    public Transform Player;
    float velocidade = 7.0f;// velocidade do inimigo quando com medo

    // vetores para calculo de movimento
    Vector2 direcao; // direção do movimento
    Vector2 alvo;// possivel ponto final do movimento
    
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
        mc = GetComponent<Medo_ciclo>();
    }
    public IEnumerator Investida(float tempo)
    {
        
        float tempoDecorrido = 0;
        if(Player.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            }
            else
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            }
        velocidade = 7.0f;
        direcao = ( Player.position-transform.position ).normalized;
        alvo = (Vector2)Player.position + direcao;// ponto final do movimento
        while(tempoDecorrido < tempo && Vector2.Distance(transform.position, alvo) > 0.1f)
        {
            tempoDecorrido += Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, alvo, velocidade * Time.deltaTime);
            yield return null;
        }
        
        mc.estadoAtual = Medo_ciclo.Estado.aumentarTamanho;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            
            StopCoroutine("Investida"); // Para a coroutine pelo nome                  
            mc.estadoAtual = Medo_ciclo.Estado.aumentarTamanho;
            
           
            
        }
    }

}
