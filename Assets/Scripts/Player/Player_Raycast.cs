using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;
public class Player_Raycast : MonoBehaviour
{
    // ESTE SISTEMA É SOMENTE PARA DETECÇÃO VISUAL DE OBJETOS 
    // SEU OBJETIVO É RETORNAR UM VALOR SE O RAIO DE VISAO ATINGIR ALGO
    // variaveis para raycast
    public float Comprimento = 3.5f;
    // direção dos raycast
    public Vector2 origem0; // origem do raycast
    public Vector2 direcao0 = Vector2.right; 
    
    //============================================================
    //referencia para inimigos
    public Vergonha_ciclos Cvergonha; //

    public Medo_Investida medo;// vou usar uma unica referencia para o medo
    // referencias para ver
    //bool podeVer = true;
    //bool corrotinaRodando;
    public Light2D luzDeVisao;
    void Start()
    {
        if(luzDeVisao ==null)
        {
            GameObject luzObjeto = GameObject.FindGameObjectWithTag("LuzDeVisao");
            if (luzObjeto != null)
            {
                luzDeVisao = luzObjeto.GetComponent<Light2D>();
            }
                
        }
    }

    //============================================================
    void Update()
    {


        // define o lado do raycast
        float lado = Mathf.Sign(transform.localScale.x);
        direcao0 = Vector2.right * lado; // central
        origem0 = new Vector2(transform.position.x + direcao0.normalized.x * 1.0f, transform.position.y);
        // raycast
        //if (podeVer == false || corrotinaRodando == true) return;
        RaycastHit2D hit0 = Physics2D.Raycast(origem0, direcao0, Comprimento);
        //===========================================================
        //detecções com inimigos 

        // novo inimigo e sua detecção pelo raycast

        // Vergonha
        if (hit0.collider != null && hit0.collider.CompareTag("Stalker"))
        {
            Cvergonha = hit0.collider.GetComponent<Vergonha_ciclos>();
            if (Cvergonha != null)
            {
                //vergonha.Detectado = true;
                Debug.Log(" raycast0 detectou o stalker!");
                // reseta os booleanos para a próxima verificação

                // StartCoroutine(limberarInimigo());
                // Cvergonha.executandoEstado = false;
                // Cvergonha.estadoAtual = Vergonha_ciclos.Estado.Detectado;
                //StartCoroutine(proximaVisao());
            }
            
        }
    }
    // IEnumerator limberarInimigo()
    // {   
    //     Cvergonha.batalhaAtivada = false;
    //     yield return new WaitForSeconds(1);
    //     Cvergonha.batalhaAtivada = true;
    // }
    //  IEnumerator proximaVisao()
    // {

    //     podeVer = false;
    //     luzDeVisao.enabled = false;
    //     corrotinaRodando = true;
    //     yield return new WaitForSeconds(5);

    //     podeVer = true;
    //     luzDeVisao.enabled = true;
    //     //Cvergonha = null;
    //     corrotinaRodando = false;
    // }
    // desenha o raycast na cena
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        origem0 = new Vector2(transform.position.x + direcao0.normalized.x * 1.0f, transform.position.y);
        float lado = Mathf.Sign(transform.localScale.x);
        direcao0 = Vector2.right * lado;
        
        // Desenha com os valores calculados agora
        Gizmos.DrawLine(origem0, origem0 + direcao0.normalized * Comprimento);
        
    }
}
