using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckAnswer5 : MonoBehaviour
{
    public List<GameObject> cardsImg = new List<GameObject>();

    private string FirstCard = string.Empty;
    private string SecondCard = string.Empty;

    
    private GameObject FirstSelected;
    public GameObject SecondSelected;

    private GameObject FirstSelectedHelper;
    private GameObject SecondSelectedHelper;

    
    private byte CorrectCounter = 0;
    private byte MistakeCounter = 0;

    public void FirstClick(GameObject clicked, string w)
    {
        FirstCard = w;
        FirstSelected = clicked;
        gameObject.GetComponent<GenerateCards>().EnableButton(false, clicked);
    }

    public void SecondClick(GameObject clicked, string w)
    {
        if(FirstSelected == null)
        {
            BadTouch();
            return;
        }
        SecondCard = w;
        SecondSelected = clicked;
        gameObject.GetComponent<GenerateCards>().EnableButton(false, clicked);

        if (FirstCard == SecondCard)
            CorrectAnswer();
        else WrongAnswer();
    }
    void CorrectAnswer()
    {
        gameObject.GetComponent<GenerateCards>().EnableAllButtons(false);
        CorrectCounter++;
        FirstCard = string.Empty;
        SecondCard = string.Empty;
        g_UIManager.Instance.IncreaseScore();
        StartCoroutine(CardsAnimation(true));
        if (CorrectCounter == gameObject.GetComponent<GenerateCards>().MatchCardsNumber)
            g_UIManager.Instance.LevelFinish(true, Timer.Instance.GetTimer().ToString("0"), MistakeCounter.ToString());

        
    }
    void WrongAnswer()
    {
        gameObject.GetComponent<GenerateCards>().EnableAllButtons(false);
        MistakeCounter++;
        FirstCard = string.Empty;
        SecondCard = string.Empty;
        StartCoroutine(CardsAnimation(false));
        Handheld.Vibrate();
        if (GameManager.Instance.IsMainGame)
        g_UIManager.Instance.DecreaseHeal();
    }

    IEnumerator CardsAnimation(bool isCor)
    {
        yield return new WaitForSeconds(1);
        if (LevelManager.Instance.isGamePause) { ResetValues(); yield break; }
        if (isCor)
        {
           // gameObject.GetComponent<GenerateCards>().EnableButton(true, null);
            Destroy(FirstSelected);
            Destroy(SecondSelected);

        }
        else
        {
            FirstSelected.GetComponent<Cards>().FadeCard(false);
            SecondSelected.GetComponent<Cards>().FadeCard(false);

            gameObject.GetComponent<GenerateCards>().EnableButton(true, FirstSelected);
            gameObject.GetComponent<GenerateCards>().EnableButton(true, SecondSelected);
        }
        

        yield return null;
        if (LevelManager.Instance.Mode5CurrentClick == GameManager.Instance.Mode5MaxClick)
            LevelManager.Instance.Mode5CurrentClick = 0;

        gameObject.GetComponent<GenerateCards>().EnableAllButtons(true);
        FirstSelected = null;
        SecondSelected = null;
    }

    

    public void BadTouch()
    {
        foreach (Transform ch in transform)
        {
            if (ch.name == "LowerPlace") continue;
            if (ch.GetChild(0).GetComponent<Image>().color.a != 0)
            {
                Color c = ch.GetChild(0).GetComponent<Image>().color;
                Color t = ch.GetChild(0).GetChild(0).GetComponent<Text>().color;
                c.a = 0;
                t.a = 0;
                ch.GetChild(0).GetComponent<Image>().color = c;
                ch.GetChild(0).GetChild(0).GetComponent<Text>().color = t;
            }
        }
        ResetValues();
       
    }
    public void ResetValues()
    {
        CorrectCounter = 0;
        MistakeCounter = 0;
        FirstCard = string.Empty;
        SecondCard = string.Empty;
        FirstSelected = null;
        SecondSelected = null;
        LevelManager.Instance.Mode5CurrentClick = 0;
    }
}
