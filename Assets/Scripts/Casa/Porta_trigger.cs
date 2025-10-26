using System;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class Porta_trigger : MonoBehaviour
{
    public casa_Manager casaManager;
    public Camera_Effects cameraEffects;// para ativar o fade in
    public GameObject player;// para ativar o icone de interação
    public Portas portas;
    //public GameObject icone;
    public enum Portas
    {
        irparaEscada_Sala, irparaCorredor, irparaQuartoNatalia, sairDoQuartoNatalia, irparaQuartoJuliana,
        sairDoQuartoJuliana
    }
    // boleanas para controlar entrada e saida 
    public bool playerdentro = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (casaManager == null)
        {
            casaManager = FindFirstObjectByType<casa_Manager>();
        }
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (cameraEffects == null)
        {
            cameraEffects = FindFirstObjectByType<Camera_Effects>();
        }
    }

    void Update()
    {
        if (playerdentro && Input.GetKeyDown(KeyCode.E) )
        {
            // ativar fadein
            cameraEffects.StartCoroutine(cameraEffects.fade());
            // Teleportar o player para a localização designada
            StartCoroutine(porta());// o tempo deve ser igual ao do fadein
        }
        
    }
    // desativar o movimento do player por um tempo
    
    public IEnumerator porta()
    {
        player.GetComponent<Player_Movement_C>().ani.SetFloat("andando", 0);
        player.GetComponent<Player_Movement_C>().enabled = false;
        //esperar um pouco para dar tempo do fadein começar
        yield return new WaitForSeconds(1.8f);
        //aqui espera o tempo do fadein
        //================================================================
        //teleportar o player
        if (portas == Portas.irparaEscada_Sala)
        {
            casaManager.entrarNaSala();
        }
        else if (portas == Portas.irparaCorredor)
        {
            casaManager.entrarNoCorredor();
        }
        else if (portas == Portas.irparaQuartoNatalia)
        {
            casaManager.entrarNoQuartoNatalia();
        }
        else if (portas == Portas.sairDoQuartoNatalia)
        {
            casaManager.sairDoQuartoNatalia();
        }
        else if (portas == Portas.irparaQuartoJuliana)
        {
            casaManager.entrarNoQuartoJuliana();
        }
        else if (portas == Portas.sairDoQuartoJuliana)
        {
            casaManager.sairDoQuartoJuliana();
        }
        //================================================================
            //esperar o tempo do fadein
            yield return new WaitForSeconds(1.5f);
        
        player.GetComponent<Player_Movement_C>().enabled = true;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Ativar ícone de interação e enquanto estiver dentro do trigger, player dentro é true
            transform.GetChild(0).gameObject.SetActive(true);
            //  if (icone != null)
            // {
            //     icone.SetActive(true);
            // }
            
            playerdentro = true;
        }
        
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            transform.GetChild(0).gameObject.SetActive(false);
            // if (icone != null)
            // {
            //     icone.SetActive(false);
            // }
            playerdentro = false;
        }
        
    }
    // Update is called once per frame
    
}
