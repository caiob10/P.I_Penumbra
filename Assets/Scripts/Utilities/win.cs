using UnityEngine;
using UnityEngine.SceneManagement;

public class win : MonoBehaviour
{
    public Raiva_status raiva;
    public Enemy_Vergonha vergonha;
    public Medo_Vida medo;
    private float tempo = 0f;
    
    void Update()
    {
        if (raiva.raivaMorto && vergonha.vergonhaMorto && medo.medoMorto)
        {
            tempo += Time.deltaTime;
            if (tempo >= 2)
            {
                SceneManager.LoadScene("win");
            }
            
        }




    }
}
