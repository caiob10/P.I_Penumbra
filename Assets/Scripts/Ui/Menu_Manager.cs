using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Menu_Manager : MonoBehaviour
{
    
    [SerializeField] private GameObject PainelMenu;
    [SerializeField] private GameObject PainelConfi;
    [SerializeField] private GameObject PainelAudio;
    [SerializeField] private GameObject PainelCtrl;
    [SerializeField] private GameObject PainelCreditos;
    Scene_Manager sManager;
    void Awake()
    {
        Time.timeScale = 1;
        // pegar SceneManager
        
        GameObject sceneObj = GameObject.FindGameObjectWithTag("SceneManager");
        if (sceneObj != null)
        {

            sManager = sceneObj.GetComponent<Scene_Manager>();
        }   
        
        
    }
    public void _Iniciar()
    {
        sManager.casa();
    }
    public void _Configurações()
    {
        PainelMenu.SetActive(false);
        PainelConfi.SetActive(true);
    }
    public void _Sair()
    {
        // sair do jogo pelo menu iniciar
        Debug.Log("jogo encerrado pelo menu");
        Application.Quit();
    }
    public void _Audio()
    {
        PainelAudio.SetActive(true);
        PainelCtrl.SetActive(false);
        PainelCreditos.SetActive(false);
        
    }
    public void _Controles()
    {
        PainelAudio.SetActive(false);
        PainelCtrl.SetActive(true);
        PainelCreditos.SetActive(false);
        
    }
    public void _Creditos()
    {
        PainelAudio.SetActive(false);
        PainelCtrl.SetActive(false);
        PainelCreditos.SetActive(true);
        
    }
    public void _MenuIniciar()
    {
        // botão de voltar das configurações
        PainelAudio.SetActive(false);
        PainelCtrl.SetActive(false);
        PainelCreditos.SetActive(false);
        PainelMenu.SetActive(true);
        PainelConfi.SetActive(false);

    }
    
}
