using UnityEngine;
using System.Collections;
public class Carente_knockback : MonoBehaviour
{
    float empurrao = 6.0f;

    float tempo = 0.5f; // tempo que o player ficara travado
    public Transform Player;
    Player_Movement pm;
    public Rigidbody2D playerRb; // referencia para o rigidbody do player
    Animator ani;

     void Awake()
    {

        if (Player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)//se conseguir pegar o player
            {
                Player = playerObj.transform;//localizacao
                playerRb = playerObj.GetComponent<Rigidbody2D>();//rigidbody2d

            }
        }
        ani = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ani.SetTrigger("cutucar");
            pm = other.GetComponent<Player_Movement>();

            Debug.Log("empurrado pelo carente");
            //pegar um vetor que sera o player - a fogueira , isso sera a direção
            Vector2 direcao = (other.transform.position - transform.position).normalized;// variavel que define a direção localmente
            
            StartCoroutine (knockback(direcao, tempo));
        }   
    }

    IEnumerator knockback(Vector2 direcao, float tempo)
    {
        
        //zerar o movimento do player vai impedir de adicionar outra força antes de completar o ciclo
        playerRb.linearVelocity = Vector2.zero;
        // aqui dentro vai ser garantido que o personagem continuara sendo repelido 
        Vector2 forcaExtra = new Vector2(direcao.x, empurrao * 0.1f);// uma força extra para cima sem mechar no eixo X
        //adiciona uma força (lembrando que é a posicao - a posicao do outro)
        playerRb.AddForce(forcaExtra * empurrao, ForceMode2D.Impulse);
        pm.enabled = false;
        yield return new WaitForSeconds(tempo);
        pm.enabled = true;
    }
}
