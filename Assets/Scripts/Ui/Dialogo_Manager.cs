using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
public class Dialogo_Manager : MonoBehaviour
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
    public GameObject drSono;


    // travar movimento do player
    Player_Movement pm;
    Player_ataque pa;
    void Awake()
    {
        //pm = GetComponent<Player_Movement_C>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            pm = player.GetComponent<Player_Movement>();
            pa = player.GetComponent<Player_ataque>();
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
        pa.enabled = false; // desativa o ataque do player durante o diálogo
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
                if(textoAtual >= 10)
                {
                    Invoke("DesativarDrSono",1.5f);
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
            pa.enabled = true; // reativa o ataque do player
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
            StartCoroutine(ce.fade());
        }
    }
    void DesativarDrSono()
    {
        drSono.SetActive(false);
    }


}
