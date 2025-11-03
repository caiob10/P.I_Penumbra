using System.Collections;
using UnityEngine;

public class Raiva_ciclo : MonoBehaviour
{
    public enum Estado
    {
        Detectar, Saltar, Esperar, Reiniciar
    }
    public Estado estadoAtual = Estado.Detectar;
    public bool batalhaAtivada = false;// ativar o ciclo
    
    
    //referencia para os outros scripts
    Raiva_deteccao rd;
    Raiva_saltos rs;
    Raiva_animation raivaAnimation;
    // redução do "tempo" de checagem do update
    // update roda a 60 fps em media e a chagem esta muito alta para o switchCase
    // por esse motivo o switch só vai ser checado depois de 10X do tempo normal do update
    
    public bool executandoEstado = false;
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

    void Start()
    {
        rd = GetComponent<Raiva_deteccao>();
        rs = GetComponent<Raiva_saltos>();
        raivaAnimation = GetComponent<Raiva_animation>();

    }

   void Update()
    {
        if (!batalhaAtivada || executandoEstado) return;
        
        // Sistema de tempo reduzido mantido
        tempoUpdate += Time.deltaTime;
        if (tempoUpdate >= tempoDesejadoUpdate)
        {
            switch (estadoAtual)
            {
                case Estado.Detectar:
                    StartCoroutine(estadoDetectar());
                    break;

                case Estado.Saltar:
                    StartCoroutine(estadoSaltar());
                    break;

                case Estado.Esperar:
                    StartCoroutine(estadoEsperar());
                    break;

                case Estado.Reiniciar:
                    StartCoroutine(estadoReiniciar());
                    break;
            }
            tempoUpdate = 0;
        }
       
    }
    IEnumerator estadoDetectar()
    {
        executandoEstado = true;
        Debug.Log("Estado: Detectar");
        raivaAnimation.SetIdle();
        if (rd.detectarPlayer())
        {
            
            estadoAtual = Estado.Saltar;
            
            Debug.Log("Player detectado! Mudando para o estado Saltar.");
        }

        executandoEstado = false;
        yield return null;
    }
    IEnumerator estadoSaltar()
    {
        executandoEstado = true;
        Debug.Log("Estado: Saltar");
        raivaAnimation.SetSubir();
        rs.saltar();
        estadoAtual = Estado.Esperar;
        Debug.Log("Mudando para o estado Esperar.");
        executandoEstado = false;
        yield return null;
    }
    IEnumerator estadoEsperar()
    {
        executandoEstado = true;
        Debug.Log("Estado: Esperar");
        if (rs.estouNoChao)
        {
            raivaAnimation.SetCair();
        }
        //raivaAnimation.SetCair();
        yield return new WaitForSeconds(0.1f); // Cooldown
        
        Debug.Log("Esperando...");
        
        estadoAtual = Estado.Reiniciar;
        executandoEstado = false;
    }

    IEnumerator estadoReiniciar()
    {
        executandoEstado = true;
        Debug.Log("Estado: Reiniciar");
        raivaAnimation.SetIdle();
        // Qualquer lógica de reset adicional pode ir aqui
        
        estadoAtual = Estado.Detectar;
        Debug.Log("Reiniciando ciclo...");
        
        executandoEstado = false;
        yield return null;
    }
    
}
