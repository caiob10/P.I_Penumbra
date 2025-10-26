using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class Ui_RenderEffects : MonoBehaviour
{
    // referencias para vida do player
    Player_Status ps;
    [SerializeField] private Volume globalVolume;
    //acesso aos efeitos
    private DepthOfField depthOfField;
    private Vignette vignette;
    // configurações de vidnette
    float intensidadeInicial;
    float intensidadeAtual;
    // variaveis para depth
    float DepthOfFieldIntensidadeInicial;
    float DepthOfFieldIntensidadeAtual;
    void Awake()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            ps = playerObj.GetComponent<Player_Status>();

        }
        // Set the layer of the GameObject containing the Volume to ignore the specified layers
        globalVolume.profile.TryGet<Vignette>(out vignette);
        globalVolume.profile.TryGet<DepthOfField>(out depthOfField);
    }
    void Start()
    {
        intensidadeInicial = vignette.intensity.value;// salvar valor atual
        DepthOfFieldIntensidadeInicial = depthOfField.gaussianEnd.value;
    }
    void Update()
    {

        if (vignette != null && ps != null)
        {
            // Atrelar diretamente: vida baixa = vignette forte
            float percentualVida = ps.vida / 100f;

            // vida = intensidadeInicial, 0% vida = intensidade máxima
            intensidadeAtual = Mathf.Lerp(0.5f, intensidadeInicial, percentualVida);

            vignette.intensity.value = intensidadeAtual;
        }
        if(depthOfField !=null && ps !=null)
        {
            if (ps.vida >= 30)
            {
                depthOfField.active = false;
            }
            else
            {
                depthOfField.active = true;
                //alterar depthOfField
                float percentualVida = ps.vida / 100f;
                DepthOfFieldIntensidadeAtual = Mathf.Lerp(1f, DepthOfFieldIntensidadeInicial, percentualVida);
                depthOfField.gaussianMaxRadius.value = DepthOfFieldIntensidadeAtual;
            }
        }
    }  
}
