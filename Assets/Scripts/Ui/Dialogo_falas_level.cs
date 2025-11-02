using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
public class Dialogo_falas_Level : MonoBehaviour
{
    public string[] falasDoObjeto;
    public string[] nomesDoObjeto;

    void OnTriggerStay2D(Collider2D other)
    {
        // Encontra o manager único na cena
        GameObject dialogoManagerObject = GameObject.FindWithTag("texto1");

        if (dialogoManagerObject != null)
        {
            Dialogo_Manager dm = dialogoManagerObject.GetComponent<Dialogo_Manager>();
            //aparecer o dialogo icone
            transform.GetChild(0).gameObject.SetActive(true);
            if (dm != null && Input.GetKeyDown(KeyCode.E))
            {
                // PASSA as falas específicas deste objeto para o manager
                dm.linhasDialogo = falasDoObjeto;
                dm.nomePersonagem = nomesDoObjeto;
                dm.Showtext();
                dm.efeito1 = gameObject.CompareTag("efeito_C_1"); // remove a tag para não reativar

                Destroy(gameObject);// destrói o objeto de fala após sair do trigger
            }

        }
        else
        {
            Debug.LogError("Dialogo_Manager_C não encontrado no objeto com a tag 'texto1'.");

        }


    }
    void OnTriggerExit2D(Collider2D collision)
    {
        // Esconder o ícone de diálogo
        transform.GetChild(0).gameObject.SetActive(false);
    }

}
