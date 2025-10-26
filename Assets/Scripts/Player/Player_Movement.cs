using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player_Movement : MonoBehaviour
{
    //Componentes
    public Rigidbody2D  rb;
    //public bool possoAndar; ia usar para bloquar andar quando empurrado

    //variaveis
    float Velocidade = 16.0f;
    float EscalaGravidade = 1f;
    float ForcaGravidade = 7.0f;
    float ForcaPulo = 10.0f;
    // andar
    public float vr{ get; private set; }// essa nova configuração permite pegar variaveis mas nao alteralas
    public float hr{ get; private set; }
    public bool possoAndar = true;
    // public float valorInput;
    public bool noChao;
    //animações
    Animator ani;
    float velY;



    //referencias para colisao com escada que esta em outro script
   //referencia para colisao com escada
   private Player_Collision pc;

    [SerializeField] private AudioSource andarAudio;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<Player_Collision>();
        ani = GetComponent<Animator>();
        if (rb == null)
        {
            Debug.Log("rb não encontrado");
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        velY = rb.linearVelocityY;
        Movimento();
        if (!noChao)
        {
            if (velY > 0.1f)
            {
                ani.ResetTrigger("caindo");
                ani.SetTrigger("pulando");
            }
            else if (velY < -0.1f)
            {
                ani.ResetTrigger("pulando");
                ani.SetTrigger("caindo");
            }
            //limitar queda
            if (rb.linearVelocity.y <= -10)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, -10);
            }
        }
        Pular();
        // valorInput = rb.linearVelocity.x;
        if (rb.linearVelocity.y > 0)
        {
            rb.gravityScale = EscalaGravidade;
        }
        else if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = ForcaGravidade;
        }

        
    }

    private void Movimento()
    {
        hr = Input.GetAxis("Horizontal");
        //vr = Input.GetAxis("Vertical");
        if (possoAndar == true)
        {

            rb.linearVelocity = new Vector2(hr * Velocidade, rb.linearVelocity.y);
            ani.SetFloat("correndo", Mathf.Abs(hr));
            if (hr == 0)
            {
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);// estou testando remover a desaceleração por causa da escada
                ani.SetFloat("correndo", 0f);
            }

        }
        



        // --- CONTROLE DO ÁUDIO ---
        if (noChao && Mathf.Abs(hr) > 0.1f)
        {
            // toca o som apenas se não estiver tocando
            if (!andarAudio.isPlaying)
            {
                andarAudio.Play();
            }
        }
        // -----------------------------




        if (rb.linearVelocity.x > 0)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        else if (rb.linearVelocity.x < 0)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
    }

    private void Pular()
    {
        if (noChao && Input.GetButtonDown("Jump"))
        {
            
            ani.ResetTrigger("caindo");
            ani.SetTrigger("pulando");
            rb.AddForce(new Vector2(0, 1) * ForcaPulo, ForceMode2D.Impulse);
            noChao = false;
            
        }
        
        
    }
}