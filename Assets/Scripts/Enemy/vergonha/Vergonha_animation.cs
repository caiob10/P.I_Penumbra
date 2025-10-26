using System.Collections;
using UnityEngine;

public class Vergonha_animation : MonoBehaviour
{
    Animator ani;
    Vergonha_Correr vCorrer;
    //corroutine para animação de correr
    public bool correndo;
    [HideInInspector]public float VelocidadeDeAnimação;
    //bool coroutineRodando = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ani = GetComponent<Animator>();
        vCorrer = GetComponent<Vergonha_Correr>();
    }

    public void SetCorrer(float velocidade)
    {
        ani.SetFloat("correr", velocidade);
    }

    // Força o personagem a ficar parado (Idle)
    public void SetIdle()
    {
        ani.SetFloat("correr", 0f);
    }

    // ====== ANIMAÇÕES DE EVENTO ÚNICO ======
    public void SetLevantar()
    {
        ani.ResetTrigger("Chutar");
        ani.SetTrigger("Levantar");
    }

    public void PlayChutar()
    {
        ani.ResetTrigger("Levantar");
        ani.SetTrigger("Chutar");
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
