using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class InteracoesManager : MonoBehaviour
{

    [SerializeField] AudioSource effSonoro;
    bool interagir = false;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (interagir && Input.GetKeyDown(KeyCode.E))
        {

            StartCoroutine(Interacao());

        }





    }

    


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interagir = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interagir = false;
        }
    }



    public IEnumerator Interacao()
    {
        
        effSonoro.Play();

        yield return new WaitForSeconds(effSonoro.clip.length);
    }
    
    








}
