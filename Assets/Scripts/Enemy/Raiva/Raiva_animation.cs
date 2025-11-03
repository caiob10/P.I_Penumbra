using UnityEngine;
using System.Collections;
public class Raiva_animation : MonoBehaviour
{
    Animator ani;
    
    //corroutine para animação de correr
    public bool correndo;
    [HideInInspector]public float VelocidadeDeAnimação;

    void Start()
    {
        ani = GetComponent<Animator>();

    }    
    public void SetIdle()
    {
        ani.SetTrigger("idle");
    }

    // ====== ANIMAÇÕES DE EVENTO ÚNICO ======
    public void SetSubir()
    {
        ani.SetTrigger("subir");
    }
    public void SetCair()
    {
        ani.SetTrigger("queda");
    }
 

    //retornar fim das animações
    public bool EstaNaAnimacao(string nome)
    {
        return ani.GetCurrentAnimatorStateInfo(0).IsName(nome);
    }

    public bool AnimacaoTerminou(string nome)
    {
        var info = ani.GetCurrentAnimatorStateInfo(0);
        return info.IsName(nome) && info.normalizedTime >= 1f && !ani.IsInTransition(0);
    }

}
