using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class g_UIManager : MonoBehaviour
{
    public Image[] DropObjects = new Image[3];
    private bool isColorCoolDown;
    public GameObject pnlMenu;

    public GameObject[] pnlGameModes = new GameObject[4];
    

    public GameObject pnlPause;

    private ushort score;
    public ushort Score
    {
        get { return score; }
    }
    public Text txtScore;


    List<GameObject> FadedObject;
    [SerializeField]
    bool bFadingAll;
    [SerializeField]
    private Color Acolor;
    private Color txtColor = Color.black;
    float alpha = 0;

    private Image[] HealsImage = new Image[3];
    public Transform[] HealsParent = new Transform[2];
    public Sprite sprtFullHeal;
    public Sprite sprtEmpyHeal;
    public byte HealsCount = 3;


    public float TimerScale = 0.1f;
    public bool isTimerStart;
    private float tempTimer = 1;
    public Image imgTimerFill;
    public Image[] imgFills;

    public bool Pressedhint = false;
    private byte HintCount = 3;
    public Text txtHintCounter;


    public byte Mistakes;
    //===============[FINISH Props]======
    public GameObject pnlFinish;
    public Button btnNextLevel;

    public Text txtIsWinMode6;
    public Text txtFailCount6;
    public Text txtFinishTimer6;

    private static g_UIManager instance;
    public static g_UIManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(bFadingAll)
        {
            Acolor.a = Mathf.Lerp(Acolor.a, alpha, Time.deltaTime * 3);
            txtColor.a = Mathf.Lerp(txtColor.a, alpha, Time.deltaTime * 3);
            for (byte i = 0; i < FadedObject.Count; i++)
            {
                if (FadedObject[i] == null) continue;
                FadedObject[i].GetComponent<Image>().color = Acolor;
                FadedObject[i].GetComponentInChildren<Text>().color = txtColor;
                
            }
            if (Acolor.a <= alpha + 0.01f)
            {
                bFadingAll = false;
                for (byte i = 0; i < FadedObject.Count; i++)
                {
                    if (FadedObject[i] == null) continue;
                    FadedObject[i].GetComponentInParent<Button>().enabled = true;
                }
                Timer.Instance.StartTimer();
                if (GameManager.Instance.IsMainGame)
                    isTimerStart = true;
                
            }
         }

        if(isTimerStart)
        {
            tempTimer -= Time.deltaTime * TimerScale;

            imgTimerFill.fillAmount = tempTimer;
            if (tempTimer <= 0.001f)
                LevelFinish(false, gameObject.GetComponent<Timer>().GetTimer().ToString("0"), Mistakes.ToString());
            
        }
       
    }
    public void StartLevel(byte panelIndex)
    {
        ResetHeals();
        ResetTimer();
        pnlGameModes[panelIndex].SetActive(true);
        switch(GameManager.Instance.GameMode)
        {
            case 3:
                {
                    pnlGameModes[panelIndex].transform.GetChild(0).GetComponent<ButtonsPlace>().StartLevel();
                    break;
                }
            case 5:
                {
                    pnlGameModes[panelIndex].transform.GetChild(1).GetComponent<GenerateCards>().StartLevel();
                    txtScore = pnlGameModes[panelIndex].transform.GetChild(0).GetChild(0).Find("ScorePar").GetChild(0).GetComponent<Text>();
                    HintCount = 3;
                    txtHintCounter.text = "3";
                    Pressedhint = false;
                    imgTimerFill = imgFills[0];
                    break;
                }
            case 6:
                {
                    pnlGameModes[panelIndex].transform.GetChild(1).GetComponent<SetupMode6>().GenerateSentence(0);
                    txtScore = pnlGameModes[panelIndex].transform.GetChild(0).Find("ScorePar").GetChild(0).GetComponent<Text>();
                    imgTimerFill = imgFills[1];

                    break;
                }
            case 7:
                {
                    pnlGameModes[panelIndex].transform.GetChild(1).GetComponent<SetupMode7>().StartLevel();
                    txtScore = pnlGameModes[panelIndex].transform.GetChild(0).Find("ScorePar").GetChild(0).GetComponent<Text>();
                    imgTimerFill = imgFills[2];

                    break;
                }
        }
    }

    public void LevelFinish(bool isWin, string timer, string failCount)
    {
        pnlFinish.SetActive(true);
        isTimerStart = false;
        LevelManager.Instance.isGamePause = true;
        if (!isWin)
            btnNextLevel.interactable = false;

        switch(GameManager.Instance.GameMode)
        {
            case 3:
                {
                    btnNextLevel.interactable = false;
                    string wtxt = (isWin == true) ? "YouWin" : "You Lose!";
                    txtIsWinMode6.text = wtxt;

                    txtFinishTimer6.text = timer + " seconds";
                    txtFailCount6.text = failCount;
                    break;
                }
            case 5:
                {
                    string wtxt = (isWin == true) ? "YouWin" : "You Lose!";
                    txtIsWinMode6.text = wtxt;

                    txtFinishTimer6.text = timer + " seconds";
                    txtFailCount6.text = failCount;

                    pnlGameModes[1].transform.GetChild(1).GetComponent<GenerateCards>().ResetCards();
                    break;
                }
            case 6:
                {
                    string wtxt = (isWin == true) ? "YouWin" : "You Lose!";
                    txtIsWinMode6.text = wtxt;

                    txtFinishTimer6.text = timer + " seconds";
                    txtFailCount6.text = failCount;

                    pnlGameModes[2].GetComponentInChildren<SetupMode6>().ResetButtnons();
                    break;
                }
            case 7:
                {
                    string wtxt = (isWin == true) ? "YouWin" : "You Lose!";
                    txtIsWinMode6.text = wtxt;

                    txtFinishTimer6.text = timer + " seconds";
                    txtFailCount6.text = failCount;

                    pnlGameModes[3].GetComponentInChildren<SetupMode7>().ResetLevel();
                    break;
                }

        }
        ResetTimer();
        Mistakes = 0;
    }
    

    public void ChangeObjectColor(GameObject gObject, Color caler)
    {
        gObject.GetComponent<Image>().color = caler;
    }

    public void ChangeDropPlaceColor(byte index, Color color)
    {
       
        if (color == Color.yellow)
        {
            for(byte i = 0; i < 3; i++)
            {
                DropObjects[i].color = Color.white;
                if(i == index - 1)
                    DropObjects[index - 1].color = color;
            }
            
        }

        if (color == Color.green)
        {
            DropObjects[index - 1].color = color;
           StartCoroutine(DropPlacesCooldownbug());
        }
        else if (color == Color.red)
        {
            DropObjects[index - 1].color = color;
            StartCoroutine(DropPlacesCooldownbug());
        }
        if (color == Color.white)
        {
            if(!isColorCoolDown)
            DropObjects[index - 1].color = color;

        }
        //if entertriger yellow
        // if exittriger white
        //else if(win) GREEN
        //else if(lose) RED

    }
    public void ResetDropsColor()
    {
        DropObjects[0].color = Color.white;
        DropObjects[1].color = Color.white;
        DropObjects[2].color = Color.white;
    }

    private IEnumerator DropPlacesCooldownbug()
    {
        isColorCoolDown = true;
        yield return new WaitForSeconds(0.4f);
        isColorCoolDown = false;
    }


    public void LerpColorChange(List<GameObject> gObject, bool fadein)
    {
        
        for (byte i = 0; i < gObject.Count; i++)
        {
            if (gObject[i] != null)
            {
                Acolor = gObject[i].GetComponent<Image>().color;
                break;
            }
        }
        
        FadedObject = gObject;
        if (fadein)
            alpha = 0;
        else
            alpha = 1;

        
        bFadingAll = true;
        StartCoroutine(HintCoolDown());
    }


    public void ResetHeals()
    {
        Transform CorPar = null;
        Mistakes = 0;
        switch(GameManager.Instance.GameMode)
        {
            case 3:
                {
                    CorPar = HealsParent[0]; break;
                }
            case 5:
                {
                    CorPar = HealsParent[1]; break;
                }
            case 6:
                {
                    CorPar = HealsParent[2]; break;
                }
            case 7:
                {
                    CorPar = HealsParent[3]; break;
                }
        }

        for(byte i = 0; i < 3; i++)
        {
            HealsImage[i] = CorPar.GetChild(i).GetComponent<Image>();
            HealsImage[i].sprite = sprtFullHeal;
        }
        HealsCount = 3;
        
    }
    public void DecreaseHeal()
    {
        if (HealsCount == 1)
            LevelFinish(false, gameObject.GetComponent<Timer>().GetTimer().ToString("0"), "3");
        else
        {
            HealsCount--;
            HealsImage[HealsCount].sprite = sprtEmpyHeal;
        }
        
    }

    public void IncreaseScore()
    {
        score++;
        txtScore.text = score.ToString();
    }
    public void ResetTimer()
    {
        tempTimer = 1;
        imgTimerFill.fillAmount = 1;
    }
    //=========================[ On Click ]=======


    public void OnClick_Mode5Hint()
    {
        if (Pressedhint || HintCount == 0) return;

       Pressedhint = true;
        HintCount--;
        txtHintCounter.text = HintCount.ToString();
       StartCoroutine(pnlGameModes[1].GetComponentInChildren<GenerateCards>().Hint());
        StartCoroutine(HintCoolDown());
    }

    public void OnClick_MainMenu()
    {

        pnlFinish.SetActive(false);
        btnNextLevel.interactable = true;
        
        byte mod = GameManager.Instance.GameMode;

        if (mod == 3)
        { 
            if (pnlGameModes[0].activeInHierarchy)
            {
                foreach (Transform child in pnlGameModes[0].transform.GetChild(0))
                    Destroy(child.gameObject);

                pnlGameModes[0].transform.GetChild(0).GetComponent<ButtonsPlace>().StopScrolling();
                pnlGameModes[0].SetActive(false);
                
            }
        }
        else if(mod == 5)
        {
            if(pnlGameModes[1].activeInHierarchy)
            {
                pnlGameModes[1].transform.GetChild(1).GetComponent<GenerateCards>().ResetCards();
                pnlGameModes[1].SetActive(false);
            }
        }
        else if (mod == 6)
        {
            if (pnlGameModes[2].activeInHierarchy)
            {
                pnlGameModes[2].GetComponentInChildren<SetupMode6>().ResetButtnons();
                Timer.Instance.GetTimer();
                isTimerStart = false;
                pnlGameModes[2].SetActive(false);
            }
        }
        else if (mod == 7)
        {
            if (pnlGameModes[3].activeInHierarchy)
            {
                pnlGameModes[3].GetComponentInChildren<SetupMode7>().ResetLevel();
                Timer.Instance.GetTimer();
                isTimerStart = false;
                pnlGameModes[3].SetActive(false);
            }
        }
        //pnlGameModes[0].SetActive(false);
        // pnlGameModes[1].SetActive(false);
        //pnlGameModes[2].SetActive(false);

        ResetDropsColor();
        ResetTimer();
        Time.timeScale = 1;
        pnlPause.SetActive(false);
        pnlMenu.SetActive(true);
    }

    public void OnClick_NextLevel()
    {
        LevelManager.Instance.SetupLevel(GameManager.Instance.GameMode, GameManager.Instance.IsMainGame);
        pnlFinish.SetActive(false);
    }
    public void OnClick_Pause()
    {
        pnlPause.SetActive(true);
        Time.timeScale = 0.001f;
    }

    public void OnClick_Continue()
    {
        pnlPause.SetActive(false);
        Time.timeScale = 1;
    }





    //---------
    IEnumerator HintCoolDown()
    {
        yield return new WaitForSeconds(5);
        Pressedhint = false;
    }

    
}
