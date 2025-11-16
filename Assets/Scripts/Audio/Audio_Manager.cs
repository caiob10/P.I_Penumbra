using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Audio_Manager : MonoBehaviour
{
    public enum CanalVolume
    {
        Master,
        Trilhas,
        SFX
    }

    [Space(10)]
    [Header("Configuração Slider Áudio")]
    [Space(10)]
    [SerializeField] private AudioMixer mixer;
    [Header("Coloque o nome do grupo de mix (Exposed Parameter)")]
    [SerializeField] private string parametro;

    [Header("Canal que esse controle representa")]
    [SerializeField] private CanalVolume canal;

    [Header("Configuração Global")]
    [SerializeField] private VolumeConfig volumeConfig;

    [Header("Slider")]
    [SerializeField] private Slider slider;

    [Space(10)]
    [Header("Lista de Músicas")]
    [SerializeField] private AudioClip[] trilhas;
    [SerializeField] private AudioSource audioPlayer;
    private int indexClip = 0;

    private void Start()
    {
        // 1) pegar o valor inicial correto daquele canal
        float valorInicial = 1f;

        if (volumeConfig != null)
        {
            switch (canal)
            {
                case CanalVolume.Master:
                    valorInicial = volumeConfig.master;
                    break;
                case CanalVolume.Trilhas:
                    valorInicial = volumeConfig.trilhas;
                    break;
                case CanalVolume.SFX:
                    valorInicial = volumeConfig.sfx;
                    break;
            }
        }

        // 2) aplica no mixer
        MudarVolume(valorInicial);

        // 3) sincroniza o slider com o valor salvo
        if (slider != null)
        {
            // SetValueWithoutNotify evita chamar o evento duas vezes
            slider.SetValueWithoutNotify(valorInicial);
            slider.onValueChanged.AddListener(MudarVolume);
        }

        // 4) parte de trilhas (igual ao que você já tinha)
        if (audioPlayer == null || trilhas == null || trilhas.Length == 0)
            return;

        audioPlayer.clip = trilhas[indexClip];
        audioPlayer.Play();
    }

    public void MudarVolume(float config)
    {
        // Salva no ScriptableObject
        if (volumeConfig != null)
        {
            switch (canal)
            {
                case CanalVolume.Master:
                    volumeConfig.master = config;
                    break;
                case CanalVolume.Trilhas:
                    volumeConfig.trilhas = config;
                    break;
                case CanalVolume.SFX:
                    volumeConfig.sfx = config;
                    break;
            }
        }

        // Converte slider (0–1) pra dB do mixer
        var volume = Mathf.Lerp(-80, 2, config);
        mixer.SetFloat(parametro, volume);
    }

    private void Update()
    {
        if (audioPlayer == null || trilhas == null || trilhas.Length == 0)
            return;

        if (!audioPlayer.isPlaying)
        {
            indexClip++;

            if (indexClip >= trilhas.Length)
                indexClip = 0;

            audioPlayer.clip = trilhas[indexClip];
            audioPlayer.Play();
        }
    }
}
