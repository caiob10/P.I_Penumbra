using UnityEngine;

public class Platforms_Manager : MonoBehaviour
{   // direção x
    Vector2 PosicaoAx; // vetor para receber posição atual
    Vector2 PosicaoBx; // recebe posição atual+ deslocamento
    Vector2 DeslocamentoX = new Vector2(5, 0);
    public float DirecaoX = 1; // se for positivo, anda pra direita e negativo para a esquerda
     //direção y
    Vector2 PosicaoAy; // vetor para receber posição atual
    Vector2 PosicaoBy; // recebe posição atual+ deslocamento
    Vector2 DeslocamentoY = new Vector2(0, 5);
    public float DirecaoY = 1; // se for positivo, anda pra direita e negativo para a esquerda
    // outras variaveis
    float Velocidade = 5.0f; // velocidade de movimento


    void Start()
    {
        PosicaoAx = transform.position;
        PosicaoBx = PosicaoAx + DeslocamentoX;
        PosicaoAy = transform.position;
        PosicaoBy = PosicaoAy + DeslocamentoY;


    }

    // Update is called once per frame
    void Update()
    {
        MovimentoX();
    }
    void MovimentoX()
    {
        float movimento = DirecaoX * Velocidade * Time.deltaTime;
        transform.position = new Vector2(transform.position.x + movimento, transform.position.y);
        PosicaoAx = transform.position;

        if (Vector2.Distance(PosicaoAx, PosicaoBx) < 0.1)
        {
            transform.position = PosicaoBx;
            DirecaoX = 0;
            MovimentoY();
        }
    }
    void MovimentoY()
    {
        float movimento = DirecaoY * Velocidade * Time.deltaTime;
        transform.position = new Vector2(transform.position.x , transform.position.y + movimento);
        PosicaoAy = transform.position;

        if (Vector2.Distance(PosicaoAy, PosicaoBy) < 0.1)
        {
            transform.position = PosicaoBx;
            DirecaoY = 0;
        }
    }
    
    
}
