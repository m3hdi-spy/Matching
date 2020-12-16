using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m_UIManager : MonoBehaviour
{

    public GameObject pnlMenu;
    public GameObject pnlSecondMode;

    byte currentMode;
    bool isMainGame;

    void Start()
    {
        
    }

   

    public void OnClick_Mode3()
    {
        currentMode = 3;
        pnlSecondMode.SetActive(true);
    }
    public void OnClick_Mode5()
    {
        currentMode = 5;
        pnlSecondMode.SetActive(true);
    }
    public void OnClick_Mode6()
    {
        currentMode = 6;
        pnlSecondMode.SetActive(true);
    }
    public void OnClick_Mode7()
    {
        currentMode = 7;
        pnlSecondMode.SetActive(true);
    }
    public void OnClick_Warmup()
    {
        isMainGame = false;
        pnlMenu.SetActive(false);
        pnlSecondMode.SetActive(false);
        GameManager.Instance.CreateLevel(currentMode, false);
    }
    public void OnClick_MainGame()
    {
        isMainGame = true;
        pnlMenu.SetActive(false);
        pnlSecondMode.SetActive(false);
        GameManager.Instance.CreateLevel(currentMode, true);
    }

    public void OnClick_CloseChooseGameMode()
    {
        pnlSecondMode.SetActive(false);
        currentMode = 0;
    }


}
