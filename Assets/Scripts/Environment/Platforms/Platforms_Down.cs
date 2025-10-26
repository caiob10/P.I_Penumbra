using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Platforms_Down : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 posInicial;
    bool Retornando = false;

    float Velocidade = 3.0f; // velocidade de movimento

    void Start()
    {
        //pegar posição da plataforma
        posInicial = transform.position;
        //pegar rb
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.Log("rb não encontrado");
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        //garantir parametros iniciais
        rb.freezeRotation = true;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0.5f;  // Reduz a gravidade (padrão é 1) vai ajudar a "segurar a queda"
        rb.linearDamping = 2.0f; // resistência ao ar 
        rb.mass = 1.0f;          // controla a massa do objeto, se combianr com damping 0 o objeto cai ainda mais rapido
    }
   
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            StartCoroutine(Caindo());
            Debug.Log("colisão detectada");
        }
    }
    IEnumerator Caindo()
    {

        yield return new WaitForSeconds(1.2f);
        rb.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(1.2f);
        StartCoroutine(Delay());

    }
    // não esta funcionando a detecção de queda por algum motivo 
    //  void OnCollisionExit2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Player"))
    //     {
    //         //chamar plataforma para a posição original
    //         StartCoroutine(Delay());
    //         Debug.Log("sai da colisao");
    //     }
    // }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.0f); // tempo de espera antes de subir
        rb.bodyType = RigidbodyType2D.Static;
        Retornando = true;
        
    }
    void Update()
    {
        if (Retornando == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, posInicial, Velocidade * Time.deltaTime);

            if (Vector2.Distance(transform.position, posInicial) < 0.1)
            {
                transform.position = posInicial;

                Retornando = false;


            }
        }
    }
    
   

}
