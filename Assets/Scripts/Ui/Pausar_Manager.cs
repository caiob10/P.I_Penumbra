
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Pausar_Manager : MonoBehaviour
{
    
    [SerializeField] private GameObject PainelPausar;// painel
    [SerializeField] private GameObject PausarCanvas;// canvas
    [SerializeField] private GameObject PainelConfi;// configurações
    [SerializeField] private GameObject uiCanvas;// canvas do jogo deve ficar oculto ao pausar
    [SerializeField] private GameObject uiCanvas2;// canvas do jogo deve ficar oculto ao pausar
    //[SerializeField] private GameObject PainelConfi;
    //[SerializeField] private GameObject PainelAudio;
    //[SerializeField] private GameObject PainelCtrl;
    public bool JogoPausado;
    // referencias para scene manager
    Scene_Manager sManager;
    void Awake()
    {
        Time.timeScale = 1;
        // pegar SceneManager
        
        sManager = FindFirstObjectByType<Scene_Manager>();
        
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            JogoPausado = !JogoPausado;
            pausar();
        }
        
    }
    void pausar()
    {

        if (JogoPausado)
        {
            PainelPausar.SetActive(true);
            PausarCanvas.SetActive(true);
            uiCanvas2.SetActive(false);
            uiCanvas.SetActive(false);
            Time.timeScale = 0;
        }
        else
        {
            PainelPausar.SetActive(false);
            PausarCanvas.SetActive(false);
            uiCanvas2.SetActive(true);
            uiCanvas.SetActive(true);
            Time.timeScale = 1;
        }
        

    }
    public void continuar()
    {
        JogoPausado = false;
        PainelPausar.SetActive(false);
        PausarCanvas.SetActive(false);
        uiCanvas2.SetActive(true);
        uiCanvas.SetActive(true);
        Time.timeScale = 1;
    }
    public void configuracoes()
    {
        Debug.Log("configurações");
        PainelConfi.SetActive(true);
        PainelPausar.SetActive(false);
        // adcionar o empt com os botoes depois 
    }
    public void voltar()
    {
        PainelConfi.SetActive(false);
        PainelPausar.SetActive(true);
    }
    public void menu()
    {
        sManager.menu();
    }
    public void sair()
    {
        sManager.quit();
    }
}
