using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAnswer6 : MonoBehaviour
{

    public byte Corrects;

    public void CheckAnswer(GameObject firstObj, GameObject secObj)
    {
        if (firstObj.GetComponent<Mode6Buttons>().ID == secObj.GetComponent<Mode6AnswerPlace>().ID)
            CorrectAnswer(firstObj, secObj);
        else
            WrongAnswer(firstObj);

    }
    private void CorrectAnswer(GameObject fObject, GameObject sObject)
    {
        Corrects++;
        g_UIManager.Instance.IncreaseScore();
        fObject.GetComponent<DragandDrop>().goToOriginal = false;
        fObject.GetComponent<DragandDrop>().Touchable = false;
        fObject.GetComponent<UnityEngine.UI.Button>().enabled = false;
        fObject.GetComponent<UnityEngine.UI.Image>().color = Color.green;
        fObject.transform.SetParent(sObject.transform.GetChild(1));
        fObject.transform.localPosition = Vector3.zero;
        StartCoroutine(MakeSureOfPosition(fObject));
        fObject.GetComponent<BoxCollider2D>().enabled = false;
        sObject.GetComponent<BoxCollider2D>().enabled = false;

        sObject.GetComponent<Mode6AnswerPlace>().IsActive = false;
        if (Corrects == 3)
        {
            Corrects = 0;
            g_UIManager.Instance.LevelFinish(true, Timer.Instance.GetTimer().ToString("0"), g_UIManager.Instance.Mistakes.ToString());
        }
    }
    private void WrongAnswer(GameObject fObject)
    {
        fObject.GetComponent<DragandDrop>().BackToOriginalPosition();
        g_UIManager.Instance.Mistakes++;
        Handheld.Vibrate();
        if (GameManager.Instance.IsMainGame)
            g_UIManager.Instance.DecreaseHeal();
    }


    IEnumerator MakeSureOfPosition(GameObject btn)
    {
        yield return new WaitForEndOfFrame();
        yield return null;
        if(btn != null)
        if (btn.transform.localPosition != Vector3.zero)
            btn.transform.localPosition = Vector3.zero;
    }
    
    void AnimationAfter(GameObject fObject, GameObject sObject)
    {

        
        
    }


     

}
