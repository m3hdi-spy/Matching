using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckAnswer : MonoBehaviour
{
    private static CheckAnswer instantce;
    public static CheckAnswer Instance
    {
        get { return instantce; }
    }

    
    void Start()
    {
        instantce = this;
    }

    public void CheckPlacement(GameObject sender, byte btnID, byte placeID)
    {
        if(btnID == placeID)
        {
            sender.GetComponent<BoxCollider2D>().enabled = false;
            g_UIManager.Instance.ChangeDropPlaceColor(placeID, Color.green);
            if(!GameManager.Instance.IsMainGame)
            sender.GetComponentInParent<ButtonsPlace>().OneScrollDown(true);
            StartCoroutine(Backto5o5(placeID));
            Destroy(sender);
        }
        else
        {
            sender.GetComponentInParent<ButtonsPlace>().MoveToTop(sender.transform);
           // sender.GetComponent<FloatingButton>().GobacktoOriginalPosition();
            g_UIManager.Instance.ChangeDropPlaceColor(placeID, Color.red);
            if(GameManager.Instance.IsMainGame)
            g_UIManager.Instance.DecreaseHeal();
            else
             sender.GetComponentInParent<ButtonsPlace>().OneScrollDown(false);
        }
    }
    IEnumerator Backto5o5(byte place)
    {
        yield return new WaitForSeconds(1.5f);
        g_UIManager.Instance.ChangeDropPlaceColor(place, Color.white);
    }


}
