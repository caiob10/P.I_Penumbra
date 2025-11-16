using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fim : MonoBehaviour
{
    
    [SerializeField] private Image fadeImage;  // a imagem preta da UI

    
    [SerializeField] private float fadeDuration = 2f;  // tempo até a tela ficar totalmente preta
    [SerializeField] private string cenaGameOver = "Game_over";

    private float timer = 0f;

    private void Start()
    {
        
        // Garante que o fade começa transparente
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 0f;
            fadeImage.color = c;
        }
    }

    private void Update()
    {
      

        // 2) Fade para preto
        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / fadeDuration);

        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = t;                // vai de 0  1
            fadeImage.color = c;
        }

        // 3) Depois de fadeDuration segundos, troca de cena
        if (timer >= fadeDuration)
        {
            SceneManager.LoadScene(cenaGameOver);
        }
    }
}
