using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Fim : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private Image fadeImage;
    [SerializeField] private TextMeshProUGUI textoFinal;

    [Header("Configurações")]
    [SerializeField] private float tempoTexto = 10f;   // Texto visível
    [SerializeField] private float fadeDuration = 5f;  // Fade da tela
    [SerializeField] private float fadeTexto = 2f;     // Fade-out do texto (deixei 2s p/ mais suavidade)
    [SerializeField] private string cenaGameOver = "Game_over";

    private float timer = 0f;
    private enum Estado { TelaPreta, MostrandoTexto, FadeTextoSaindo, FadeFinal }
    private Estado estadoAtual = Estado.MostrandoTexto;

    private void Start()
    {
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 1f;
            fadeImage.color = c;
        }

        if (textoFinal != null)
        {
            Color tc = textoFinal.color;
            tc.a = 0f;
            textoFinal.color = tc;
        }
    }

    private void Update()
    {
        switch (estadoAtual)
        {
            case Estado.MostrandoTexto:
                MostrarTexto();
                break;
            case Estado.FadeTextoSaindo:
                FadeOutTexto();
                break;
            case Estado.FadeFinal:
                FadeFinal();
                break;
        }
    }

    private void MostrarTexto()
    {
        timer += Time.deltaTime;

        // Fade-in SUAVE
        float t = Mathf.Clamp01(timer / 1.5f);         // aumentei o tempo p/ suavizar
        float smooth = Mathf.SmoothStep(0f, 1f, t);    // curva suave
        SetAlphaTexto(smooth);

        if (timer >= tempoTexto)
        {
            timer = 0f;
            estadoAtual = Estado.FadeTextoSaindo;
        }
    }

    private void FadeOutTexto()
    {
        timer += Time.deltaTime;

        // Fade-out MUITO SUAVE usando ease-out exponencial
        float t = Mathf.Clamp01(timer / fadeTexto);
        float suave = 1f - Mathf.Pow(1f - t, 2.5f);   // curva lenta no início, rápida no final

        SetAlphaTexto(1f - suave);

        if (timer >= fadeTexto)
        {
            timer = 0f;
            estadoAtual = Estado.FadeFinal;
        }
    }

    private void FadeFinal()
    {
        timer += Time.deltaTime;

        float t = Mathf.Clamp01(timer / fadeDuration);

        // Fade da tela preta com easing: começa lento, termina suave
        float suave = Mathf.SmoothStep(0f, 1f, t);

        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = suave;
            fadeImage.color = c;
        }

        if (timer >= fadeDuration)
        {
            SceneManager.LoadScene(cenaGameOver);
        }
    }

    private void SetAlphaTexto(float a)
    {
        if (textoFinal != null)
        {
            Color tc = textoFinal.color;
            tc.a = a;
            textoFinal.color = tc;
        }
    }
}
