using UnityEngine;
using System.Collections;
public class Camera_Manager : MonoBehaviour
{
    [SerializeField] private Transform player; // Referência ao player (arraste no Inspector)
    [SerializeField] private float offsetX = 0f;                 // Distância da câmera em relação ao player no eixo X
    public float offsetY = 5f;                 // Distância da câmera em relação ao player no eixo Y
    [SerializeField] private float smoothSpeed = 10f;             // Velocidade da suavização do movimento da câmera
    public bool CameraLimite = false;


    void Update()
    {
         // Pega a posição atual da câmera
            Vector3 pos = transform.position;

            // Calcula a posição alvo no eixo X (posição do player + offset)
            float targetX = player.position.x + offsetX;
            float targetY = player.position.y + offsetY;
            // Suaviza a movimentação da câmera no eixo X
            pos.x = Mathf.Lerp(pos.x, targetX, smoothSpeed * Time.deltaTime);
            pos.y = Mathf.Lerp(pos.y, targetY, smoothSpeed * Time.deltaTime);
            // Atualiza a posição da câmera
            transform.position = pos;

    }
   
    

}
