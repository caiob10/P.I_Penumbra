using UnityEngine;

public class Button_Manager : MonoBehaviour
{
    [SerializeField] private GameObject porta;
    SpriteRenderer sr;
    bool onButton = false ;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            onButton = !onButton;
            
        }   
 
    }
    void Update()
    {
        if (onButton)
        {
            porta.GetComponent<BoxCollider2D>().enabled = false;
            // troca de sprite pra saber se apertou o botao
            sr.color = new Color(1f, 0f, 0f, 1f);
        }
        else
        {
            porta.GetComponent<BoxCollider2D>().enabled = true;
            // troca de sprite pra saber se apertou o botao
            sr.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
