using Unity.VisualScripting;
using UnityEngine;


public class Player_Collision : MonoBehaviour
{
    Player_Movement pm;
    // para texto
    Scene_Manager sManager;
    SpriteRenderer sr ;
    Dialogo_Manager dm;
    

    void Start()
    {
        pm = GetComponent<Player_Movement>();
        dm = GetComponent<Dialogo_Manager>();
        // pegar SceneManager
        GameObject sceneObj = GameObject.FindGameObjectWithTag("SceneManager");
        if (sceneObj != null)
        {
            sManager = sceneObj.GetComponent<Scene_Manager>();
        }
    }
    
    
    // trigger para saltos
    private void OnCollisionStay2D(Collision2D Collision)
    {
        if (Collision.gameObject.CompareTag("Plataforma"))
        {
            pm.noChao = true;
        }
        if(Collision.gameObject.layer == LayerMask.NameToLayer("Chao"))
        {
            pm.noChao = true;
        }
       
    }

    private void OnCollisionExit2D(Collision2D Collision)
    {
        if (Collision.gameObject.CompareTag("Plataforma"))
        {
            pm.noChao = false;
        }
        // importante para colisão com chão do tipo tilemap
        if(Collision.gameObject.layer == LayerMask.NameToLayer("Chao"))
        {
            pm.noChao = false;
        }
    }
    //---------------------------------------------------------------------------------
    // colisoes para texto
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("texto1") && collision.transform.root.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            dm.Showtext();
            pm.possoAndar = false;
        }
        
        
    }
    // saida geral para trigger
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("texto1") && gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // só destrói a caixa se o diálogo já terminou
            if (dm.fimDialogo())
            {
                // tentar definir o proximo valor do texto aqui
                pm.possoAndar = true;
            }
            Destroy(collision.gameObject);
        }
        
        
    }
}