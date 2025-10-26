using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Vergonha : MonoBehaviour

{
    public float vida = 100f;

    public void LevarDano(float danoRecebido)
    {
        vida -= danoRecebido;
        Debug.Log("Vergonha levou dano, vida atual: " + vida);
        if (vida <= 0)
        {
            Morrer();
        }
    }
    void Morrer()
    {
        Debug.Log("Vergonha morreu!");
        // Aqui você pode adicionar efeitos de morte, animações, etc.
        Destroy(gameObject);
    }
    //detecção
    
    // public bool Detectado = false;// detecção ok
    // public bool Esperando = false;
    // public Transform Player;
    // public LayerMask playerLayer;
    // //movimento 
    // float velocidade = 10.0f;
    // // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
    //     if (Player == null)
    //     {
    //         GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
    //         if (playerObj != null)
    //         {
    //             Player = playerObj.transform;
    //         }
    //     }
        
    // }
    // // Update is called once per frame
    // void Update()
    // {

    //     if (!Detectado)
    //     {
    //         // tem algo no circulo?
    //         Collider2D col = Physics2D.OverlapCircle(transform.position, raioVisao, playerLayer);
    //         //é o player
    //         bool playerDentro = col != null && col.CompareTag("Player");
    //         // se é o player
    //         if (playerDentro && Player != null)
    //         {
    //             //calula a distancia
    //             float distancia = Vector2.Distance(transform.position, Player.position);
    //             Debug.Log("player detectado" + distancia);
    //             //movimento liberado
    //             transform.position = Vector2.MoveTowards(transform.position, Player.position, velocidade * Time.deltaTime);

    //         }

    //     }
    //     else
    //     {
    //         if (Esperando == false)
    //         {
    //             StartCoroutine(Espera());
    //             Debug.Log("Player detectado pelo raycast");
    //         }
            
    //     }
        
    // }
    // IEnumerator Espera()
    // {
    //     Esperando = true;
    //     yield return new WaitForSeconds(5f);
    //     Detectado = false;
    //     Esperando = false;

    // }
   
}
