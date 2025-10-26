using UnityEngine;
using System.Collections;
public class Vergonha_ciclos : MonoBehaviour
{
     public enum Estado
    {
        Detectar, Correr, Atacar, Esperar, Reiniciar, Detectado, Fugir
    }

    public Estado estadoAtual = Estado.Detectar;
    public bool batalhaAtivada = false;
    [HideInInspector]public bool executandoEstado = false;

    // Referências
    Vergonha_Detectar vd;
    Vergonha_Ataque vAtaque;
    Vergonha_Correr vCorrer;
    Vergonha_animation vAnimation;

    // Flags por estado
    

    void Start()
    {
        vd = GetComponent<Vergonha_Detectar>();
        vAtaque = GetComponent<Vergonha_Ataque>();
        vCorrer = GetComponent<Vergonha_Correr>();
        vAnimation = GetComponent<Vergonha_animation>();
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

            case Estado.Atacar:
                 StartCoroutine(estadoChutar());
                break;

            case Estado.Esperar:
                 StartCoroutine(estadoEsperar());
                break;

            case Estado.Detectado:
                 StartCoroutine(estadoDetectado());
                break;

            case Estado.Fugir:
                 StartCoroutine(estadoFugir());
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
        vAnimation.SetIdle();

        while (estadoAtual == Estado.Detectar)
        {
            if (vd.detectarPlayer())
            {
                Debug.Log("Detectou o player, levantando...");
                vAnimation.SetLevantar();

                //yield return new WaitUntil(() => vAnimation.AnimacaoTerminou("Levantar"));
                yield return new WaitForSeconds(1);
                estadoAtual = Estado.Correr;
                break;
            }
            yield return null;
        }
        executandoEstado = false;
       
    }

    IEnumerator estadoCorrer()
    {
        executandoEstado = true;
        Debug.Log("Estado: Correr");

        vAnimation.SetCorrer(vCorrer.velocidade);
        yield return StartCoroutine(vCorrer.Correr(3f));// quando acabar a corrida, termina a corrida
        vAnimation.SetCorrer(0f);

        estadoAtual = Estado.Atacar;
        executandoEstado = false;
    }

    IEnumerator estadoChutar()
    {
        executandoEstado = true;
        Debug.Log("Estado: Atacar");

        vAnimation.PlayChutar();
        //yield return new WaitUntil(() => vAnimation.AnimacaoTerminou("Chutar"));
        yield return new WaitForSeconds(0.2f);
        // if (estadoAtual == Estado.Detectado)
        // {
        //    yield break;
        // } 
        vAtaque.StartCoroutine(vAtaque.Atacar());
        yield return new WaitForSeconds(0.5f);
        estadoAtual = Estado.Esperar;
        executandoEstado = false;
    }

    IEnumerator estadoEsperar()
    {
        executandoEstado = true;
        Debug.Log("Estado: Esperar");

        vAnimation.SetIdle();
        yield return new WaitForSeconds(1f);

        estadoAtual = Estado.Reiniciar;
        executandoEstado = false;
    }

    IEnumerator estadoDetectado()
    {
        batalhaAtivada = true;
        executandoEstado = true;
        Debug.Log("Estado: Detectado");

        yield return new WaitForSeconds(1f);

        estadoAtual = Estado.Fugir;
        executandoEstado = false;
    }

    IEnumerator estadoFugir()
    {
        executandoEstado = true;
        Debug.Log("Estado: Fugir");

        vAnimation.SetCorrer(vCorrer.velocidade);
        yield return StartCoroutine(vCorrer.Fugir(3f));
        vAnimation.SetCorrer(0f);

        estadoAtual = Estado.Reiniciar;
        executandoEstado = false;
    }

    IEnumerator estadoReiniciar()
    {
        executandoEstado = true;
        Debug.Log("Estado: Reiniciar");
        vAnimation.SetIdle();
        yield return new WaitForSeconds(1f);

        estadoAtual = Estado.Detectar;
        executandoEstado = false;
    }
}
