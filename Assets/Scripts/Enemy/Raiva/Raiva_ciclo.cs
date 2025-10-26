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
    // redução do "tempo" de checagem do update
    // update roda a 60 fps em media e a chagem esta muito alta para o switchCase
    // por esse motivo o switch só vai ser checado depois de 10X do tempo normal do update
    
   
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
                    Debug.Log("o estado atuel é :"+ estadoAtual);
                    if(rd.detectarPlayer())
                    {
                        estadoAtual = Estado.Saltar;
                        Debug.Log(estadoAtual);
                    
                    }
                    
                   
                //     if (!proximoEstado)
                //     {
                //         if ()
                //         {
                //             StartCoroutine(cooldown(1));
                //         }
                //     }
                    // else if (!rd.detectarPlayer()) // 🎯 SUA IDEIA: Se player SAIR, reseta
                    // {
                    //     proximoEstado = false; // 🎯 Zera para detectar de novo quando player voltar
                    //     Debug.Log("Player saiu - Resetando detecção");
                    // }
                    // else
                    // {
                    
                    
                    //     proximoEstado = false;
                    //     Debug.Log("detectado");
                    // }
                    break;

                case Estado.Saltar:

                    // if (!proximoEstado)
                    // {
                    Debug.Log("o estado atuel é :"+ estadoAtual);
                    rs.saltar();
                    //     StartCoroutine(cooldown(1));
                    //     Debug.Log("Saltando...");
                    //     proximoEstado = true;
                    // }
                    // else
                    // {
                    estadoAtual = Estado.Esperar;
                    Debug.Log(estadoAtual);
                    
                    //     proximoEstado = false;
                    // }
                    break;

                case Estado.Esperar:

                    // if (!proximoEstado)
                    // {
                    Debug.Log("o estado atuel é :"+ estadoAtual);
                    StartCoroutine(cooldown(0.5f));
                    Debug.Log("Esperando...");
                    // }
                    // else
                    // {
                    estadoAtual = Estado.Reiniciar;
                    Debug.Log("o estado atuel é :"+ estadoAtual);
                    
                    //     proximoEstado = false;
                    // }
                    break;

                case Estado.Reiniciar:

                    //     if (!proximoEstado)
                    //     {
                    estadoAtual = Estado.Detectar;
                    Debug.Log("Reiniciando...");
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
