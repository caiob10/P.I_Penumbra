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
    [SerializeField] private AudioSource ataque;

    //referencia para os outros scripts
    Medo_Deteccao md;
    public Medo_Disparo mDisparo;
    Medo_Investida mInvestida;
    public Medo_Tamanho mTamanho;
    public Medo_animator mAnimator;
    public int tipoAtaque;
    public float tempoUpdate = 0.0f; //tempo incrementado no update
    public float tempoDesejadoUpdate = 1f; // tempo final desejado
    bool executandoEstado = false;
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
        mAnimator = GetComponent<Medo_animator>();
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
                    StartCoroutine(estadoDetectar());
                    break;
                case Estado.SortearAtaque:
                    StartCoroutine(SortearAtaque());
                    break;

                case Estado.aumentarTamanho:

                    StartCoroutine(aumentarTamanho());
                    break;
                    
                case Estado.Esperar:
                    StartCoroutine(cooldown(0.1f));
                    Debug.Log("fim de ciclo");
                    break;

                case Estado.Reiniciar:
                    estadoAtual = Estado.Detectar;
                    break;
            }
            tempoUpdate = 0;   
        }
       
    }
    

    IEnumerator estadoDetectar()
    {
        executandoEstado = true;
        mAnimator.SetIdle();
        
        if (md.detectarPlayer())
        {
            
            estadoAtual = Estado.SortearAtaque;

            Debug.Log("Player detectado! Mudando para o estado SortearAtaque.");
        }

        executandoEstado = false;
        yield return null;
    }
    IEnumerator SortearAtaque()
    {
        executandoEstado = true;
        //lógica para sortear o ataque
        tipoAtaque = Random.Range(1, 4);
        ataque.Play();
        if (tipoAtaque == 1)
        {
            //investida
            tipoAtaque = 0;
            mAnimator.SetInvestida();
            mInvestida.StartCoroutine(mInvestida.Investida(5f));
            
        }
        if(tipoAtaque ==2)
        {
            tipoAtaque = 0;
            mAnimator.SetDisparo();
            mDisparo.Disparo();
            estadoAtual = Estado.aumentarTamanho;
        }
        if(tipoAtaque ==3)
        {
            tipoAtaque = 0;
            mAnimator.SetInvestida();
            mInvestida.StartCoroutine(mInvestida.Investida(5f));
        }
        
        executandoEstado = false;
        yield return null;
    }
    IEnumerator aumentarTamanho()
    {
        mAnimator.SetIdle();
        executandoEstado = true;
        mTamanho.StartCoroutine(mTamanho.aumentarTamanho(0.1f));
        executandoEstado = false;
        yield return null;
    }
    IEnumerator cooldown(float tempo)
    {   
        yield return new WaitForSeconds(tempo);
        estadoAtual = Estado.Reiniciar;
    }
}
