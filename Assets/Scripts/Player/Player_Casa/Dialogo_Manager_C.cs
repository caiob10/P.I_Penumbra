using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
public class Dialogo_Manager_C : MonoBehaviour
{
    public TextMeshProUGUI textDisplay; // link no inspector
    public TextMeshProUGUI nomeDisplay; // link no inspector
    // vou colcoar nele mesmo o mecanismo pra mostrar o texto
    [SerializeField] private GameObject textoCanvas;// canvas
    public string[] linhasDialogo;
    public string[] nomePersonagem;  // array de strings para o dialogo
    private int textoAtual = 0; // indice do texto atual
    //---------------------------------------------------------------------------------
    public bool efeito1;


    // travar movimento do player
    Player_Movement_C pm;
    void Awake()
    {
        //pm = GetComponent<Player_Movement_C>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            pm = player.GetComponent<Player_Movement_C>();
        }
        else
        {
            Debug.LogError("Player não encontrado! Verifique a tag.");
        }
    }

    public void Showtext()
    {
        textoCanvas.SetActive(true);
        pm.possoAndar = false; // trava o player
        // zera a velocidade do player ao iniciar o diálogo
        Rigidbody2D rb = pm.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
    void Update()
    {

        if (textoAtual < linhasDialogo.Length)
        {
            textDisplay.text = linhasDialogo[textoAtual];
            nomeDisplay.text = nomePersonagem[textoAtual];
            if (Input.GetMouseButtonDown(0) && pm.possoAndar == false) // avança com clique
            {

                textoAtual++;
                // efeito de camera 1
                if (textoAtual == 10 && efeito1 == true)
                {
                    efeitos(); // Chama o shake
                    efeito1 = false; // Para não chamar de novo
                }

            }
            else if (Input.GetMouseButtonDown(1) && pm.possoAndar == false) // avança com clique
            {
                if (textoAtual > 0)
                {
                    textoAtual--;
                }
            }

        }
        if (textoAtual >= linhasDialogo.Length)
        {
            textoCanvas.SetActive(false); // esconde o balão quando acaba
            textoAtual = 0; // reinicia se quiser
            pm.possoAndar = true;

        }
    }
    public void efeitos()
    {
        Camera_Effects ce = Camera.main.GetComponent<Camera_Effects>();
        if (ce != null)
        {
            ce.shake(1.5f);
        }
    }
    // public TextMeshProUGUI textDisplay; // link no inspector
    // public TextMeshProUGUI nomeDisplay; // link no inspector
    // vou colcoar nele mesmo o mecanismo pra mostrar o texto
    // [SerializeField] private GameObject textoCanvas;// canvas
    // public string[] linhasDialogo;
    // public string[] nomePersonagem;  // array de strings para o dialogo
    // private int textoAtual = 0; // indice do texto atual
    //---------------------------------------------------------------------------------



    // travar movimento do player
    // Player_Movement_C pm;
    // void Awake()
    // {
    //    //pm = GetComponent<Player_Movement_C>();
    //     GameObject player = GameObject.FindGameObjectWithTag("Player");
    //     if (player != null)
    //     {
    //         pm = player.GetComponent<Player_Movement_C>();
    //     }
    //     else
    //     {
    //         Debug.LogError("Player não encontrado! Verifique a tag.");
    //     }
    // }

    // public void Showtext()
    // {
    //     textoCanvas.SetActive(true);
    //     pm.possoAndar = false; // trava o player
    // }
    // void Update()
    // {

    //     if (textoAtual < linhasDialogo.Length)
    //     {
    //         textDisplay.text = linhasDialogo[textoAtual];
    //         nomeDisplay.text = nomePersonagem[textoAtual];
    //         if (Input.GetMouseButtonDown(0) && pm.possoAndar == false) // avança com clique
    //         {
    //             textoAtual++;

    //         }
    //         else if (Input.GetMouseButtonDown(1) && pm.possoAndar == false) // avança com clique
    //         {
    //             if (textoAtual > 0)
    //             {
    //                 textoAtual--;
    //             }
    //         }

    //     }
    //     if (textoAtual >= linhasDialogo.Length)
    //     {
    //         textoCanvas.SetActive(false); // esconde o balão quando acaba
    //         textoAtual = 0; // reinicia se quiser
    //         pm.possoAndar = true;
    //     }
    // }
    //posso pensar em fazer um sistema mais complexo
    // tipo um botão para avançar, outro para voltar
    // e talvez um para fechar o balão de texto 

    // é uma confrimação para saber se realmente acabou o diálogo
    // public bool fimDialogo()
    // {
    //     Debug.Log("fim do dialogo");
    //     // a ideia é definir o proximo valor do texto aqui
    //     return textoAtual >= linhasDialogo.Length;
    // }



}
