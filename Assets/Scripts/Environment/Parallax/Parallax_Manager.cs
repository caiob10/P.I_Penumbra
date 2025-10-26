using UnityEngine;
using UnityEngine.UI;
public class Parallax_Manager : MonoBehaviour
{
    [Header("Configurações")]
    public float velocidadeX = 0f;
    public float velocidadeY = 0f;
    
    [Header("camera")]
    public Transform cameraTransform;

    private Vector3 lastCameraPosition;
    private Vector3 initialPosition;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
        
        lastCameraPosition = cameraTransform.position;
        initialPosition = transform.position;
    }

    void LateUpdate()
    {
        if (cameraTransform == null) return;

        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        // MOVIMENTO DO OBJETO
        MoveLayerObject(deltaMovement);

        lastCameraPosition = cameraTransform.position;
    }

    // Move o OBJETO diretamente
    void MoveLayerObject(Vector3 deltaMovement)
    {
        // Calcula o movimento baseado no movimento da câmera e velocidade
        Vector3 movement = new Vector3(deltaMovement.x * velocidadeX, deltaMovement.y* velocidadeY,0f);
        
        // Move o objeto
        transform.position += movement;
    }

    // Método opcional para resetar a posição se necessário
    public void ResetPosition()
    {
        transform.position = initialPosition;
    }
}