using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player_Movement : MonoBehaviour
{
    //Componentes
    public Rigidbody2D  rb;
    //public bool possoAndar; ia usar para bloquar andar quando empurrado

    //variaveis
    [SerializeField] private float Velocidade = 16.0f;
    
    [SerializeField] private float ForcaGravidadePulo = 2f;
    [SerializeField] private float ForcaGravidadeQueda = 2f;
    [SerializeField] private float ForcaPulo = 600f;
    bool podePular = true;

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
    [SerializeField] private AudioSource puloAudio;
    [SerializeField] private LayerMask cenarioLayer;
    [SerializeField] private Transform pePlayer;

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
        
        Pular();
        // valorInput = rb.linearVelocity.x;
        if (rb.linearVelocity.y > 0)
        {
            rb.gravityScale = ForcaGravidadePulo;
        }
        else if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = ForcaGravidadeQueda;
        }
        else
        {
            rb.gravityScale = 1f;
        }

        noChao = Physics2D.OverlapCircle(pePlayer.position, 0.3f, cenarioLayer);

    }


    private void FixedUpdate()
    {
        if (!noChao)
        {
            Velocidade = 8f;
        }
        else
        {
            Velocidade = 16f;
        }



        if (noChao)
        {
            //ani.ResetTrigger("caindo");
            ani.SetTrigger("idle");
        }


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
        //if (rb.linearVelocity.y <= -10)
        //{
        //    rb.linearVelocity = new Vector2(rb.linearVelocity.x, -10);
        //}

    }


    private void Movimento()
    {
        hr = Input.GetAxis("Horizontal");
        //vr = Input.GetAxis("Vertical");
        if (possoAndar)
        {

            rb.linearVelocityX = hr * Velocidade;
            ani.SetFloat("correndo", Mathf.Abs(hr));
            //if (hr == 0)
            //{
            //    rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);// estou testando remover a desaceleração por causa da escada
            //    ani.SetFloat("correndo", 0f);
            //}

        }
        else
        {
            // é uma adaptação para quando o player estiver em diálogo não tocar a animação de andar
            ani.SetFloat("correndo", 0f);
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
        if (podePular && noChao && Input.GetButtonDown("Jump"))
        {
            
            ani.ResetTrigger("caindo");
            ani.SetTrigger("pulando");
            //rb.AddForce(new Vector2(0, 1) * ForcaPulo, ForceMode2D.Impulse);
            puloAudio.Play();
            rb.AddForceY(ForcaPulo);
            noChao = false;
            
        }
        
        
    }
}