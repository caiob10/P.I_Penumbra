using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player_Movement_C1 : MonoBehaviour
{
    //Componentes
    public Rigidbody2D rb;
    public Animator ani;
    private Player_Collision_C pc;
    //public bool possoAndar; ia usar para bloquar andar quando empurrado
    private float timer = 0f;
    [SerializeField] private float tempoParaComeçarAndar = 10f;

    //variaveis
    [SerializeField] private float Velocidade = 2.5f;
    public float EscalaGravidade = 1.0f;
    public float ForcaGravidade = 9.0f;
    //private float ForcaPulo = 10.0f;
    // andar
    public float vr { get; private set; }// essa nova configuração permite pegar variaveis mas nao alteralas
    public float hr { get; private set; }
    public bool possoAndar = true;
    [SerializeField] private AudioSource andarAudio;



    void Start()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<Player_Collision_C>();
        if (rb == null)
        {
            Debug.Log("rb não encontrado");
            rb = gameObject.AddComponent<Rigidbody2D>();
        }



    }

    


    // Update is called once per frame
    void Update()
    {
        Movimento();
        timer += Time.deltaTime;
    }

    private void Movimento()
    {
        if (timer >= tempoParaComeçarAndar)
        {
            hr = 1;
        }
        
        //vr = Input.GetAxis("Vertical");
        if (possoAndar == true)
        {
            rb.linearVelocity = new Vector2(hr * Velocidade, rb.linearVelocity.y);

            ani.SetFloat("andando", Mathf.Abs(hr));
            if (hr == 0)
            {

                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);// estou testando remover a desaceleração por causa da escada

            }
        }
        else
        {
            ani.SetFloat("andando", 0);
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
        if (rb.linearVelocity.x > 0)
        {

            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.linearVelocity.x < 0)
        {

            transform.localScale = new Vector3(-1, 1, 1);
        }
        // --- CONTROLE DO ÁUDIO ---
        if (Mathf.Abs(hr) > 0.1f)
        {
            // toca o som apenas se não estiver tocando
            if (!andarAudio.isPlaying)
            {
                andarAudio.Play();
            }
        }
        // -----------------------------

    }
    // atualmente estou usando para travar o movimento quando entrar em salas com a porta_trigger

}