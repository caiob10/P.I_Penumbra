using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Camera_Effects : MonoBehaviour
{
    // referencia para a camera
    public Image imageBlack;
    float fadeSpeed = 1.5f;// ela interffere no tempo do fade in e out .
    
    void Start()
    {
        imageBlack.CrossFadeAlpha(0, fadeSpeed, false); // Vai para transparente
    }
    //=================================================================================
    // CAMERA SHAKE METHOD
    // metodo para iniciar o shake
    public void shake(float duracao)
    {
        StartCoroutine(Shake( duracao));
    }

    // camera shake
    public IEnumerator Shake(float duracao, float intensidade = 0.1f) // duração e intensidade
    {
        Vector3 originalPosition = transform.localPosition;
        float tempoAtual = 0f;

        while (tempoAtual < duracao)
        {
            float offsetX = Random.Range(-1f, 1f) * intensidade;
            float offsetY = Random.Range(-1f, 1f) * intensidade;

            transform.localPosition = new Vector3(originalPosition.x + offsetX, originalPosition.y + offsetY, originalPosition.z);

            tempoAtual += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
    //=================================================================================
    // FADE IN E OUT
    public void fadein()
    {
        imageBlack.CrossFadeAlpha(0, fadeSpeed, false); // Vai para transparente
    }
    public void fadeout()
    {
        imageBlack.CrossFadeAlpha(1, fadeSpeed, false); // Vai para preto
    }
    public IEnumerator fade()
    {
        Debug.Log("iniciando Fade");
        fadeout();
        yield return new WaitForSeconds(2.5f);//tempo entre o fadeout e o fadein
        fadein();
        Debug.Log("fade terminou");
    }
    //=================================================================================

}
