using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Player_knockBack : MonoBehaviour
{
     // variavel para empurrar o player pra longe da fogueira
    public float empurrao = 1.0f;

    public float tempo = 0.5f; // tempo que o inimigo ficara travado e tem que ser menbor que o tempo do proximo ataque
    public void hitflash(SpriteRenderer sr)
    {
        StartCoroutine(Flash(sr));
    }
    IEnumerator Flash (SpriteRenderer sr)
    {
        // Muda para vermelho
        Color corOriginal = sr.color;// cor original
        float duracao = 0.5f; //tempo total onde o efeito é permitido
        float t = 0; // tempo decorrico
        float piscaPisca = 5.0f;
        while (t < duracao)
        {
            if (sr == null) yield break;
            t += Time.deltaTime;
            // calculo pra conseguir piscar 
            float oscilador = Mathf.PingPong(t * piscaPisca, 1f);
            //realmente começa o piscaPisca
            sr.color = Color.Lerp(corOriginal, Color.red, oscilador);
            yield return null;
            
        }
        sr.color = corOriginal;
    }
    public void AlvoKnockBack(GameObject Alvo)
    {
        if (Alvo.CompareTag("Raiva"))
        {
            Debug.Log("Aplicando knockback na Raiva");
            Rigidbody2D rbAlvo = Alvo.GetComponent<Rigidbody2D>();
            if (rbAlvo != null)
            {
                Debug.Log("Empurrando Raiva");

                // Calcular direção: inimigo é empurrado PARA LONGE do player
                Vector2 direcao = (Alvo.transform.position - transform.position).normalized;

                // Aplicar o knockback
                StartCoroutine(knockback(rbAlvo, Alvo, direcao, tempo));
            }

        }
         if (Alvo.CompareTag("Medo"))
        {
            Debug.Log("Aplicando knockback no medo");
            Rigidbody2D rbAlvo = Alvo.GetComponent<Rigidbody2D>();
           if (rbAlvo != null)
            {
                Debug.Log("Empurrando medo");
                
                // Calcular direção: inimigo é empurrado PARA LONGE do player
                Vector2 direcao = (Alvo.transform.position - transform.position).normalized;
                
                // Aplicar o knockback
                StartCoroutine(knockback(rbAlvo,Alvo,direcao, tempo));
            }
            
        }      
    }

    IEnumerator knockback(Rigidbody2D rbInimigo,GameObject Alvo, Vector2 direcao, float tempo)
    {
        // garantir que o inimigo esta vivo
        if (Alvo == null) yield break;
        // precisei desabilitar todos os scripts do inimigo 
        MonoBehaviour[] scriptsInimigo = Alvo.GetComponents<MonoBehaviour>();
        List<MonoBehaviour> scriptsDesabilitados = new List<MonoBehaviour>();
        foreach (var script in scriptsInimigo)
        {
            if (script != this && script.enabled)
            {
                script.enabled = false;
                scriptsDesabilitados.Add(script);
            }
        }
        if (rbInimigo != null)
        {
            //zerar o movimento do player vai impedir de adicionar outra força antes de completar o ciclo
            rbInimigo.linearVelocity = Vector2.zero;
            // aqui dentro vai ser garantido que o personagem continuara sendo repelido 
            Vector2 forcaExtra = new Vector2(direcao.x, empurrao * 0.2f);// uma força extra para cima sem mechar no eixo X
            //adiciona uma força (lembrando que é a posicao - a posicao do outro)
            rbInimigo.AddForce(forcaExtra * empurrao, ForceMode2D.Impulse);
        }


        yield return new WaitForSeconds(tempo);
        if(Alvo != null)
        {
            foreach (var script in scriptsDesabilitados)
            {
                if (script != null)
                {
                    script.enabled = true;
                }
            }
        }
        
        
    }
}
