using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckAnswer7 : MonoBehaviour
{
    bool YekActive, DoActive;

    byte FirstID = 125;
    byte secID;
    GameObject FirstSender;
    GameObject SecondSender;

    public List<GameObject> Buttons = new List<GameObject>();
    public GameObject ParentsNotAllowed;

    byte CorrectAnswersCounter = 0;
    byte MistakesAnsCounter = 0;
    void Awake()
    {
        ParentsNotAllowed = gameObject;
    }

    private void Update()
    {
       
            if (EventSystem.current.currentSelectedGameObject == ParentsNotAllowed || EventSystem.current.currentSelectedGameObject.GetComponent<ButtonsMode7>() == null)
            {
                if (FirstSender != null) FirstSender = null;
                FirstID = 125;
                YekActive = false;
                if (SecondSender != null) SecondSender = null;
                secID = 124;
                DoActive = false;
            }
        
    }
    public void LevelStarted()
    {
        FirstID = 125;
        secID = 124;
        FirstSender = null;
        SecondSender = null;
        YekActive = false;
        DoActive = false;
        CorrectAnswersCounter = 0;
        MistakesAnsCounter = 0;
    }
    public void CheckAnswer(GameObject sender)
    {
        
        if(sender.transform.parent.name == "Yek")
        {
            YekActive = true;
            FirstID = sender.GetComponent<ButtonsMode7>().ID;
            FirstSender = sender;
        }
        else
        {
            DoActive = true;
            secID = sender.GetComponent<ButtonsMode7>().ID;
            SecondSender = sender;
        }


        if (YekActive && DoActive)
        {
            if (FirstID == secID)
                CorrectAnswer(FirstSender, SecondSender);
            else
                WrongAnswer(FirstSender, SecondSender);

            YekActive = false;
            DoActive = false;
        }
         
    }
    private void CorrectAnswer(GameObject fObj, GameObject sObj)
    {
        g_UIManager.Instance.ChangeObjectColor(fObj, Color.green);
        g_UIManager.Instance.ChangeObjectColor(sObj, Color.green);
        fObj.GetComponent<UnityEngine.UI.Button>().enabled = false;
        sObj.GetComponent<UnityEngine.UI.Button>().enabled = false;
        fObj.GetComponent<ButtonsMode7>().Answered = true;
        sObj.GetComponent<ButtonsMode7>().Answered = true;

        g_UIManager.Instance.IncreaseScore();
        CorrectAnswersCounter++;
        if (CorrectAnswersCounter == 3)
        {
            g_UIManager.Instance.LevelFinish(true, Timer.Instance.GetTimer().ToString("0"), MistakesAnsCounter.ToString());
            CorrectAnswersCounter = 0;
            MistakesAnsCounter = 0;
        }
    }
    private void WrongAnswer(GameObject fObj, GameObject sObj)
    {
        g_UIManager.Instance.ChangeObjectColor(fObj, Color.red);
        g_UIManager.Instance.ChangeObjectColor(sObj, Color.red);

        fObj.GetComponent<UnityEngine.UI.Button>().enabled = false;
        sObj.GetComponent<UnityEngine.UI.Button>().enabled = false;

        StartCoroutine(ResetAnswerColor(fObj, sObj));
        Handheld.Vibrate();
        MistakesAnsCounter++;
        g_UIManager.Instance.Mistakes++;
        if(MistakesAnsCounter == 3 && GameManager.Instance.IsMainGame)
        {
            MistakesAnsCounter = 0;
            CorrectAnswersCounter = 0;
        }
        if (GameManager.Instance.IsMainGame)
            g_UIManager.Instance.DecreaseHeal();
    }

    IEnumerator ResetAnswerColor(GameObject fObj, GameObject sObj)
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForSeconds(1);
        
            g_UIManager.Instance.ChangeObjectColor(fObj, Color.white);
            g_UIManager.Instance.ChangeObjectColor(sObj, Color.white);
            fObj.GetComponent<UnityEngine.UI.Button>().enabled = true;
            sObj.GetComponent<UnityEngine.UI.Button>().enabled = true;
        
    }

}
