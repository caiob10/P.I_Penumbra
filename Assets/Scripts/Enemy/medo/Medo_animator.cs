using UnityEngine;

public class Medo_animator : MonoBehaviour
{
    Animator ani;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    public void SetInvestida()
    {
        ani.SetTrigger("investida");
    }
    public void SetIdle()
    {
        ani.SetTrigger("idle");
    }
    public void SetDisparo()
    {
        ani.SetTrigger("disparo");
    }
    
}
