using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trriger : MonoBehaviour
{
    public byte ID;

    Color HolderColor = Color.yellow;

    private bool thisImage;

    void Start()
    {
        thisImage = gameObject.GetComponent<Image>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        g_UIManager.Instance.ChangeDropPlaceColor(ID, HolderColor);
        collision.gameObject.GetComponent<FloatingButton>().OnPlace(ID);
        Debug.Log("Stay");
       // Debug.Log(ID + "  :  btn " + collision.gameObject.GetComponent<FloatingButton>().CurrectPlaceID);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        g_UIManager.Instance.ChangeDropPlaceColor(ID, Color.white);
        collision.gameObject.GetComponent<FloatingButton>().NotOnPlace();

    }
}
