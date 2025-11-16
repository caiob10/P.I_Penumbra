using UnityEngine;
using UnityEngine.SceneManagement;

public class win : MonoBehaviour
{
    public Raiva_status raiva;
    public Enemy_Vergonha vergonha;
    public Medo_Vida medo;
    
    void Update()
    {
        if (raiva.raivaMorto && vergonha.vergonhaMorto && medo.medoMorto)
        {
            SceneManager.LoadScene("win");
        }




    }
}
