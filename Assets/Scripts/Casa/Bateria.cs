using UnityEngine;

public class Bateria : MonoBehaviour
{
    [SerializeField] AudioSource somE;
    [SerializeField] AudioSource somI;
    [SerializeField] AudioSource somO;
    bool interagir = false;



    // Update is called once per frame
    void Update()
    {


        if (interagir && Input.GetKeyDown(KeyCode.E))
        {

            somE.Play();

        }

        if (interagir && Input.GetKeyDown(KeyCode.I))
        {

            somI.Play();

        }

        if (interagir && Input.GetKeyDown(KeyCode.O))
        {

            somO.Play();

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









}
