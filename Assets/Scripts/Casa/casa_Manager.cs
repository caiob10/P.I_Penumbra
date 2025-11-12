using UnityEngine;

public class casa_Manager : MonoBehaviour
{
    //entradas mistas
    private Transform sala;// complementa o corredor
    private Transform corredor; //complementa a sala
    //entradas quartos
    private Transform quartoNatalia;//entrada para o quarto da Natalia
    private Transform quartoJuliana;// entrada para o quarto da Juliana

    // //saidas para fora dos quartos
    private Transform portaJuliana;// porta de saida do quarto da Juliana
    private Transform portaNatalia;// porta de saida do quarto da Natalia
    //referencia para o player
    private Transform player;

    void Awake()
    {

        player = GameObject.Find("Player_Casa").transform;
        sala = GameObject.Find("Escada_Sala").transform;
        corredor = GameObject.Find("Corredor").transform;
        quartoNatalia = GameObject.Find("Interno_Quarto_Natalia").transform;
        portaNatalia = GameObject.Find("Porta_Quarto_Natalia").transform;
        quartoJuliana = GameObject.Find("Interno_Quarto_Juliana").transform;
        portaJuliana = GameObject.Find("Porta_Quarto_Juliana").transform;

    }

    // metodos para teleportar para dentro e fora dos quartos
    public void entrarNoCorredor()
    {
        player.position = corredor.position;
    }
    public void entrarNaSala()
    {
        player.position = sala.position;
    }
    public void entrarNoQuartoNatalia()
    {
        player.position = quartoNatalia.position;
    }
    public void sairDoQuartoNatalia()
    {
        player.position = portaNatalia.position;
    }
    public void entrarNoQuartoJuliana()
    {
        player.position = quartoJuliana.position;
        Debug.Log(quartoJuliana.position);
    }
    public void sairDoQuartoJuliana()
    {
        player.position = portaJuliana.position;
    }


    //private void Update()
    //{
    //    Debug.Log(quartoJuliana.position);
    //}
}   
