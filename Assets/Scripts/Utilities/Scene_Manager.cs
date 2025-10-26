using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class Scene_Manager : MonoBehaviour
{
    
    private static Scene_Manager instance;
    
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void casa()
    {
        SceneManager.LoadScene("Casa");
    }
    public void pesadelo()
    {
        SceneManager.LoadScene("Level");
    }
    public void menu()
    {
        SceneManager.LoadScene("Menu");
    }
    // recarregar fase
    public void recarregar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // estudando outras implementações de load 
    }
    public void quit()
    {
        // encerrar jogo
        Debug.Log("jogo encerrado");
        Application.Quit();
    }
    // permitir teleportar entre fases para ver bugs

    // // Carrega uma fase específica pelo nome
    // public void LoadLevel(string levelName)
    // {
    //     SceneManager.LoadScene(levelName);
    // }

    // Reinicia a fase atual



}
