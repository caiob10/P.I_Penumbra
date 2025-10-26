using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
public class Dialogo_Manager : MonoBehaviour
{
    public TextMeshProUGUI textDisplay; // link no inspector
    // vou colcoar nele mesmo o mecanismo pra mostrar o texto
    [SerializeField] private GameObject textoCanvas;// canvas
    public string[] linhasDialogo;       // ou use ScriptableObject
    private int textoAtual = 0;



    // travar movimento do player
    Player_Movement pm;
    void Awake()
    {
        pm = GetComponent<Player_Movement>();
    }

    public void Showtext()
    {
        textoCanvas.SetActive(true);
        pm.possoAndar = false; // trava o player
    }
    void Update()
    {
        if (textoAtual < linhasDialogo.Length)
        {
            textDisplay.text = linhasDialogo[textoAtual];
            if (Input.GetMouseButtonDown(0) && pm.possoAndar == false) // avança com clique
            {
                textoAtual++;

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
    //posso pensar em fazer um sistema mais complexo
    // tipo um botão para avançar, outro para voltar
    // e talvez um para fechar o balão de texto 

    // é uma confrimação para saber se realmente acabou o diálogo
    public bool fimDialogo()
    {
        Debug.Log("fim do dialogo");
        // a ideia é definir o proximo valor do texto aqui
        return textoAtual >= linhasDialogo.Length;
    }



}
