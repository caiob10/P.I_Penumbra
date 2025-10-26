using UnityEngine;

public class Platforms_PingPongX : MonoBehaviour
{
    Vector2 PosicaoAx; // vetor para receber posição atual
    Vector2 PosicaoBx; // recebe posição atual+ deslocamento
    Vector2 Deslocamento = new Vector2(25, 0);
    

    float Velocidade = 0.5f; // velocidade de movimento
    //public float Direcao = 1; // se for positivo, anda pra direita e negativo para a esquerda

    void Start()
    {
        PosicaoAx = transform.position;
        PosicaoBx = PosicaoAx + Deslocamento;


    }

    // Update is called once per frame
    void Update()
    {
        // Movimento automático de vai-e-volta entre ponto A e B
        // é o mesmo codigo do platformsmanager, porem simplificado 
        float pingPong = Mathf.PingPong(Time.time * Velocidade, 1f);
        transform.position = Vector2.Lerp(PosicaoAx, PosicaoBx, pingPong);
    }
    
    
}
