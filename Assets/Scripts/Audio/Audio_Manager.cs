using UnityEngine;
using UnityEngine.Audio;

public class Audio_Manager : MonoBehaviour
{
    [Space(10)]
    [Header("Configura��o Slider �udio")]
    [Space(10)]
    [SerializeField] private AudioMixer mixer;
    [Header("Coloque o nome do grupo de mix")]
    [SerializeField] private string parametro;

    public void MudarVolume(float config)
    {
        var volume = Mathf.Lerp(-80, 2, config);
        mixer.SetFloat(parametro, volume);
    }

    [Space(10)]
    [Header("Lista de M�sicas")]

    [SerializeField] private AudioClip[] trilhas;
    [SerializeField] private AudioSource audioPlayer;
    private int indexClip = 0;

    private void Start()
    {
        audioPlayer.clip = trilhas[indexClip];
        audioPlayer.Play();
    }


    private void Update()
    {
        if (!audioPlayer.isPlaying)
        {
            indexClip++;
            

            if (indexClip >= trilhas.Length)
            {
                indexClip = 0;
            }

            audioPlayer.clip = trilhas[indexClip];
            audioPlayer.Play();
        }

        

    }
}
