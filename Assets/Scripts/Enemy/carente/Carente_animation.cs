using UnityEngine;

public class Carente_animation : MonoBehaviour
{
    Animator ani;

    void Start()
    {
        ani = GetComponent<Animator>();
    }
    public void SetIdle()
    {
        ani.SetTrigger("idle");

    }
    public void SetPular()
    {
        ani.SetTrigger("pulando");
    }
    public void SetCutucar()
    {
        ani.SetTrigger("cutucar");
    }
}
