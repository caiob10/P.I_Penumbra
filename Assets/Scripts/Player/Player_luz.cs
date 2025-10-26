using UnityEngine;
using System.Collections.Generic;
public class Player_luz : MonoBehaviour
{
    SpriteRenderer sr;
    List<int> tipoDeLuz = new List<int>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        foreach (SortingLayer Layer in SortingLayer.layers)
        {
            tipoDeLuz.Add(Layer.id);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("luz_interna"))
        {
            sr.sortingLayerID = tipoDeLuz[2];
        }

    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("luz_interna"))
        {
            sr.sortingLayerID = tipoDeLuz[0];
        }
    }
}
