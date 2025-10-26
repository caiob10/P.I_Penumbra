using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Medo_Tamanho : MonoBehaviour
{
    Medo_ciclo mc;
    float tempoAtual = 0;
    public float tamanhoAcumulado = 0;
    float tamanhoAtual;
    Player_Status ps;

    void Awake()
    {
        if (ps == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)//se conseguir pegar o player
            {
                ps = playerObj.GetComponent<Player_Status>();// pegar vida

            }
        }
        mc = GetComponent<Medo_ciclo>();
    }
    public IEnumerator aumentarTamanho(float tempo)
    {

        while(tamanhoAcumulado<=20 && tempoAtual<=tempo)
        {

            tempoAtual += Time.deltaTime;
            float percentualVida = ps.vida / 100f;
            tamanhoAtual= percentualVida;

            transform.localScale += transform.localScale * tamanhoAtual * Time.deltaTime; //adiciona + 1 na scale do medo   
            tamanhoAcumulado = tamanhoAcumulado + tamanhoAtual;
            yield return null;
        }
        
        tempoAtual = 0;
        mc.estadoAtual = Medo_ciclo.Estado.Esperar;
    }
}
