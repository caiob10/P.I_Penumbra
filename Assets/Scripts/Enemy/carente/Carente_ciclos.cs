using UnityEngine;
using System.Collections;
public class Carente_ciclos : MonoBehaviour
{
    public enum Estado
    {
        Detectar, Correr, cutucar, Esperar, Reiniciar
    }

    public Estado estadoAtual = Estado.Detectar;
    public bool batalhaAtivada = false;
    [HideInInspector]public bool executandoEstado = false;

    // Referências
    Carente_deteccao Cdeteccao;
    Carente_animation Canimation;
    Carente_movimento Cmovimento;

    // Flags por estado
    

    void Start()
    {
        Cdeteccao = GetComponent<Carente_deteccao>();
        Canimation = GetComponent<Carente_animation>();
        Cmovimento = GetComponent<Carente_movimento>();
    }

    void Update()
    {
        if (!batalhaAtivada || executandoEstado) return;

        switch (estadoAtual)
        {
            case Estado.Detectar:
                 StartCoroutine(estadoDetectar());
                break;

            case Estado.Correr:
                 StartCoroutine(estadoCorrer());
                break;

            case Estado.cutucar:
                 StartCoroutine(estadoCutucar());
                break;

            case Estado.Esperar:
                 StartCoroutine(estadoEsperar());
                break;

            case Estado.Reiniciar:
                StartCoroutine(estadoReiniciar());
                break;
        }
    }

    IEnumerator estadoDetectar()
    {
        executandoEstado = true;
        Debug.Log("Estado: Detectar");
        Canimation.SetIdle();   
        //setar trigger de animação idle
        if (Cdeteccao.detectarPlayer())
        {
            
            estadoAtual = Estado.Correr;
            Canimation.SetPular();
            Debug.Log("Player detectado!");
        }
        yield return new WaitForSeconds(1f);
        executandoEstado = false;
       
    }

    IEnumerator estadoCorrer()
    {
        executandoEstado = true;
        
        bool ladoAlado = Cmovimento.ladoAlado();
        
        Cmovimento.saltar();
        if(ladoAlado)
        {
            estadoAtual = Estado.cutucar;
        }
        else
        {
            // Se não chegou, volta para Detectar para tentar novamente
            estadoAtual = Estado.Detectar;
        }
  
        executandoEstado = false;
        yield return null;
    }

    IEnumerator estadoCutucar()
    {
        executandoEstado = true;
        Debug.Log("Estado: Cutucar");
        Canimation.SetCutucar();
        yield return new WaitForSeconds(0.5f);
        estadoAtual = Estado.Esperar;
        executandoEstado = false;
    }

    IEnumerator estadoEsperar()
    {
        executandoEstado = true;
        Canimation.SetIdle();   
        yield return new WaitForSeconds(0.5f);
        estadoAtual = Estado.Detectar;
        executandoEstado = false;
    }
    IEnumerator estadoReiniciar()
    {
        executandoEstado = true;
        Canimation.SetIdle();   
        yield return new WaitForSeconds(1f);
        estadoAtual = Estado.Detectar;
        executandoEstado = false;
    }
}
