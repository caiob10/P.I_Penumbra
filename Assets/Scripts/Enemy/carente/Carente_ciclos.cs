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

    // Flags por estado
    

    void Start()
    {
        Cdeteccao = GetComponent<Carente_deteccao>();
        Canimation = GetComponent<Carente_animation>();
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
            
            Debug.Log("Player detectado!");
        }
        yield return new WaitForSeconds(1f);
        executandoEstado = false;
       
    }

    IEnumerator estadoCorrer()
    {
        executandoEstado = true;
        yield return new WaitForSeconds(1f);
        executandoEstado = false;
    }

    IEnumerator estadoCutucar()
    {
        executandoEstado = true;
        yield return new WaitForSeconds(1f);
        executandoEstado = false;
    }

    IEnumerator estadoEsperar()
    {
        executandoEstado = true;
         yield return new WaitForSeconds(1f);
        executandoEstado = false;
    }
    IEnumerator estadoReiniciar()
    {
        executandoEstado = true;
         yield return new WaitForSeconds(1f);
        executandoEstado = false;
    }
}
