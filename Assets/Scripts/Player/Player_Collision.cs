using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class Player_Collision : MonoBehaviour
{
    Player_Movement pm;
    // para texto

    
    //Dialogo_Manager dm;
    // para a porta
    bool possoSair = false;
    public Camera_Effects cameraEffects;// para ativar o fade in
    public Camera_Manager cameraManager;
    public Camera cameraPrincipal;
    public Camera cameraSecundaria;
 
    void Start()
    {
        // if (cameraEffects == null)
        // {
        //     cameraEffects = FindFirstObjectByType<Camera_Effects>();
        // }
        pm = GetComponent<Player_Movement>();
        //dm = GetComponent<Dialogo_Manager>();
        
        // pegar SceneManager
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && possoSair)
        {
            //mudar posição do player para o inicio
            StartCoroutine(teleporte());
            //cameraEffects.StartCoroutine(cameraEffects.fade());
            StartCoroutine(cameraEffects.fade());
           

        }
    }
    IEnumerator teleporte()
    {
        pm.possoAndar = false;
        yield return new WaitForSeconds(1.5f);//tempo entre o fadeout e o fadein
        transform.position = new Vector3(25, -67, 0);
        cameraManager.offsetY = 5f;
        cameraPrincipal.orthographicSize = 9f;
        cameraSecundaria.orthographicSize = 9f;
        
        yield return new WaitForSeconds(1f);//tempo entre o fadeout e o fadein
        pm.possoAndar = true;
    }

    // trigger para saltos
    //private void OnCollisionStay2D(Collision2D Collision)
    //{
    //    if (Collision.gameObject.CompareTag("Plataforma"))
    //    {
    //        pm.noChao = true;
    //    }
        
    //    if (Collision.gameObject.layer == LayerMask.NameToLayer("Chao"))
    //    {
    //        pm.noChao = true;
    //    }

    //}
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("sairDoQuarto"))
        {
            possoSair = true;
            collision.transform.GetChild(0).gameObject.SetActive(true);//atiar icone da porta
        }
    }


    //---------------------------------------------------------------------------------
    // colisoes para texto
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("texto1") && collision.transform.root.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //dm.Showtext();
            pm.possoAndar = false;
        }


    }
     private void OnCollisionExit2D(Collision2D Collision)
    {
        if (Collision.gameObject.CompareTag("Plataforma"))
        {
            pm.noChao = false;
        }
        // importante para colisão com chão do tipo tilemap
        if (Collision.gameObject.layer == LayerMask.NameToLayer("Chao"))
        {
            pm.noChao = false;
        }
        
    }
    // saida geral para trigger
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("texto1") && gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // só destrói a caixa se o diálogo já terminou
            // if (dm.fimDialogo())
            // {
            //     // tentar definir o proximo valor do texto aqui
            //     pm.possoAndar = true;
            // }
            Destroy(collision.gameObject);
        }
         if (collision.gameObject.CompareTag("sairDoQuarto"))
        {
            possoSair = false;
            collision.transform.GetChild(0).gameObject.SetActive(false);//atiar icone da porta
        }
        
    }
}