using UnityEngine;
using System.Collections;

public class Medo_ciclo : MonoBehaviour
{
    public enum Estado
    {
        Detectar, SortearAtaque , aumentarTamanho,mover,Esperar, Reiniciar
    }
    public Estado estadoAtual = Estado.Detectar;
    public bool batalhaAtivada = false;// ativar o ciclo


    //referencia para os outros scripts
    Medo_Deteccao md;
    public Medo_Disparo mDisparo;
    Medo_Investida mInvestida;
    public Medo_Tamanho mTamanho;
    public int tipoAtaque;
    public float tempoUpdate = 0.0f; //tempo incrementado no update
    public float tempoDesejadoUpdate = 1f; // tempo final desejado
    // divisão de 1 segundo dividido pelo valor dos quadros .
    // 60 quadros(tempo do update normalmente)
    // 0.0167f	60x
    // 0.02f	50x	
    // 0.025f	40x	
    // 0.033f	30x	
    // 0.05f	20x	
    // 0.1f	    10x	
    // 0.2f	    5x	
    // 0.5f	    2x	
    // 1.0f	    1x	

    void Awake()
    {

        md = GetComponent<Medo_Deteccao>();
        mDisparo = GetComponent<Medo_Disparo>();
        mInvestida = GetComponent<Medo_Investida>();
        mTamanho = GetComponent<Medo_Tamanho>();
    }

   void Update()
    {
        if (batalhaAtivada == false) return;
        //tentando aumentar o intervalo entre as checagens
        tempoUpdate += Time.deltaTime;// vai iniciar a contagem
        if (tempoUpdate >= tempoDesejadoUpdate)
        {

            switch (estadoAtual)
            {
                
                case Estado.Detectar:
                    if(md.detectarPlayer())
                    {
                        
                        Debug.Log(estadoAtual);
                        estadoAtual = Estado.SortearAtaque;
                    }
                 
                    break;
                case Estado.SortearAtaque:
                    tipoAtaque = Random.Range(1, 3);
                    if (tipoAtaque == 1)
                    {
                        //investida
                        tipoAtaque = 0;
                        mInvestida.StartCoroutine(mInvestida.Investida(5f));
                        
                        
                       
                    }
                    if(tipoAtaque ==2)
                    {
                        tipoAtaque = 0;
                        mDisparo.Disparo();
                        estadoAtual = Estado.aumentarTamanho;
                        
                    }
                    break;

                case Estado.aumentarTamanho:


                    mTamanho.StartCoroutine(mTamanho.aumentarTamanho(0.1f));
                    break;

                case Estado.Esperar:
                    StartCoroutine(cooldown(0.5f));
                    Debug.Log("fim de ciclo");
                    break;

                case Estado.Reiniciar:
                    estadoAtual = Estado.Detectar;
                    break;
            }
            tempoUpdate = 0;   
        }
       
    }
    


    IEnumerator cooldown(float tempo)
    {
        yield return new WaitForSeconds(tempo);
        estadoAtual = Estado.Reiniciar;
    }
}
