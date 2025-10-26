using System.Collections.Generic;
using UnityEngine;
using System.Collections;
public class Player_Collision_C : MonoBehaviour
{
    Player_Movement_C pm;
    // para texto
    Scene_Manager sManager;
    Camera_Manager camera_Manager;
    Camera_Effects cameraEffects;
    bool playerDentro;
    bool CoroutineAtiva = false;
    //Dialogo_Manager_C dm;
    void Awake()
    {
        pm = GetComponent<Player_Movement_C>();
        //dm = GetComponent<Dialogo_Manager_C>();
        // pegar SceneManager
        GameObject sceneObj = GameObject.FindGameObjectWithTag("SceneManager");
        if (sceneObj != null)
        {
            sManager = sceneObj.GetComponent<Scene_Manager>();
        }
        GameObject cameraObj = GameObject.FindGameObjectWithTag("MainCamera");
        if (cameraObj != null)
        {
            camera_Manager = cameraObj.GetComponent<Camera_Manager>();
            cameraEffects = cameraObj.GetComponent<Camera_Effects>();
        }
    }
    void Update()
    {
        if (playerDentro && Input.GetKeyDown(KeyCode.E))
            {
                if (CoroutineAtiva) return;
                // ativar fadein
                cameraEffects.StartCoroutine(cameraEffects.fade());
                // Teleportar o player para a localização designada


                StartCoroutine(proximaFase(2.0f));
            }
    }
    // trigger para interação com a casa
    private void OnTriggerStay2D(Collider2D collider)
    {

        if (collider.gameObject.CompareTag("Pfase"))
        {
            playerDentro = true;
            collider.transform.GetChild(0).gameObject.SetActive(true);
            

        }


    } 
     void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Pfase"))
        {
            
            other.transform.GetChild(0).gameObject.SetActive(false);
            playerDentro = false;
        }
        
    }
    
    IEnumerator proximaFase(float tempo)
    {
        CoroutineAtiva = true;
        pm.ani.SetFloat("andando", 0);
        pm.enabled = false;
        yield return new WaitForSeconds(tempo);
        CoroutineAtiva = false;
        pm.enabled = true;
        yield return new WaitForSeconds(0.1f);
        sManager.pesadelo();
    }
   
   
}