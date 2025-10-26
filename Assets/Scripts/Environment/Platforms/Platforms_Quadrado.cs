using UnityEngine;

public class Platforms_Quadrado : MonoBehaviour
{
    Vector2[] Posicoes = new Vector2[4];
    int pontoAtual = 0;
    public float Velocidade = 5.0f;
    Vector2 PosInicial; //posição inicial

    void Start()
    {
        PosInicial = transform.position;
        
        // configuracao das posicoes + valor a ser somado
        Posicoes[0] = PosInicial + new Vector2(23, 0);  // Direita
        Posicoes[1] = PosInicial + new Vector2(23, 23);  // Cima
        Posicoes[2] = PosInicial + new Vector2(0, 23);  // Esquerda
        Posicoes[3] = PosInicial; // Baixo (volta ao início)
    }

    void Update()
    {
        // Move para o ponto atual
        transform.position = Vector2.MoveTowards(transform.position, Posicoes[pontoAtual], Velocidade * Time.deltaTime);

        // Quando chegar perto do ponto, vai para o próximo
        if (Vector2.Distance(transform.position, Posicoes[pontoAtual]) < 0.1f)
        {
            pontoAtual = (pontoAtual -1+4) % 4; // Cicla entre 0, 1, 2, 3 // pra inverter a direcao é com -1+4 
        }
        // explicacao do % 
        // resto de oprecao matematica de divisao que nessa situacao corresponde a um valor atrelado ao indice
        // 1 % 4 → 1 ÷ 4 = 0 com resto 1 → retorna 1
        // 2 % 4 → 2 ÷ 4 = 0 com resto 2 → retorna 2  
        // 3 % 4 → 3 ÷ 4 = 0 com resto 3 → retorna 3
        // 4 % 4 → 4 ÷ 4 = 1 com resto 0 → retorna 0 se retornar 0 ele continua o loop
        
    }
    
}
