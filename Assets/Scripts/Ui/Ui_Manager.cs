using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ui_Manager : MonoBehaviour
{
    // referencia pra global volume vignette

    public Slider coolDownSlider;
    public Image fillImage;
    Player_ataque pAtaque;
    // atualizar vida
    Player_Status ps;
    public Slider vidaSlider;
    public Image FillImageVida;
    public Image UiMaterial;
    public Material glowOff;
    public Material glowVerde;
    public Material glowLaranja;
    public Material glowVermelho;
    public float piscaPisca;
    private bool flashAtivo = false;// garantir que não vai bugar o bagulho
    void Awake()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            pAtaque = playerObj.GetComponent<Player_ataque>();
            ps = playerObj.GetComponent<Player_Status>();

        }
        
    }


    // Update is called once per frame
    void Update()
    {
        //atualizar o slider de cooldown
        coolDownSlider.value = pAtaque.cooldownTimer;

        if (coolDownSlider.value >= coolDownSlider.maxValue)
        {
            if (ps.vida >= 30)
            {
                fillImage.color = Color.white;// quando encher pode atacar novamente
            }
            else
            {
                fillImage.color = Color.grey;// quando encher pode atacar novamente
            }            

        }
        // naturalmente, enquanto estiver carregando, é vermelho
        else
        {
            if (ps.vida >= 30)
            {
                //fillImage.color = Color.red; // caso contrario é vermelho
                fillImage.color = Color.red; // caso contrario é vermelho escuro
            }
            else
            {
                fillImage.color = new Color(0.5f,0.0f,0.0f,1.0f); // caso contrario é vermelho escuro
            }           
            
        }
        //atualizar o slider de vida
        vidaSlider.value = ps.vida;

    }
    public void vidaVerde()
    {
        FillImageVida.color = Color.green; //verde
        UiMaterial.material = glowVerde;
        
    }
    public void vidalaranja()
    {
        FillImageVida.color = new Color(1.0f, 0.65f, 0.0f, 1.0f); // Laranja
        UiMaterial.material = glowLaranja;
    }

    public IEnumerator flashVida()
    {
        
        if (ps.vida >= 31f) yield break;// evitar conflitos com a outra coroutine
        
        UiMaterial.material = glowVermelho;
        if(ps.vida<=10)
        {
            UiMaterial.material = glowOff;
        }
        // Enquanto a vida for menor ou igual a 10, continua piscando
        while (ps.vida <= 30f)
        {
            float oscilador = Mathf.PingPong(Time.time * piscaPisca, 1f);

            if (oscilador > 0.5f)
            {
                FillImageVida.color = Color.red;
                
            }
            else
            {
                FillImageVida.color = Color.white;
                
            }
            yield return null;
        }


    }
    public IEnumerator flashBlackLife()
    {
        if (flashAtivo || ps.vida <= 30f) yield break;// evitar conflitos com a outra coroutine
        flashAtivo = true;
        //Color corAtual = FillImageVida.color; // Guarda a cor atual
        yield return new WaitForSeconds(0.2f); // Tempo rápido do flash
        FillImageVida.color = Color.red;
        
        yield return new WaitForSeconds(0.2f); // Tempo rápido do flash
        FillImageVida.color = Color.white;
        
        yield return new WaitForSeconds(0.2f); // Tempo rápido do flash
        //FillImageVida.color = corAtual;       // Volta para a cor original
        ps.AtualizarCoresVida();
        flashAtivo = false;
    }
}
