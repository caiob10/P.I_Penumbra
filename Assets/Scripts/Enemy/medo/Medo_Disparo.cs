using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
public class Medo_Disparo: MonoBehaviour
{
    // coponentes que precisam de acesso
    Rigidbody2D rb;
    //disparo
    public GameObject prefabDisparo;
    private Vector2 origemDeDisparo;
    float velocidade = 10.2f;// velocidade do disparo
    // referencia pro player 
    public LayerMask playerLayer;
    public Transform playerTransform;
    // vetores para calculo de movimento
    Vector2 direcao; // direção do disparo
    Vector2 alvo;// ponto final do disparo
    
    void Awake()    
    {
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                playerTransform = playerObj.transform;
            }
        }
        rb = GetComponent<Rigidbody2D>();
    }
   
    public void Disparo()
    {
        
        direcao = (Vector2)(playerTransform.position - transform.position);
        alvo = direcao.normalized;
        // origem do disparo
        origemDeDisparo = (Vector2)transform.position + alvo * 1;
        //gerando instancia do prefab
        GameObject instanciaPrefab = Instantiate(prefabDisparo, origemDeDisparo, quaternion.identity);
        //aplicando fisica no disparo
        Rigidbody2D rbDisparo = instanciaPrefab.GetComponent<Rigidbody2D>();
        // adicionar força extra no eixo y
        Vector2 forcafinal = new Vector2(alvo.x * velocidade, alvo.y * velocidade+5);
        rbDisparo.linearVelocity = forcafinal;
        
       
        

        Destroy(instanciaPrefab, 15f);
        
        
    }
    
   
}
