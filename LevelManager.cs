using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    byte gameMode;
    bool isMainGame;


    public float Model3RepeatRate = 2.5f;
    public float Model6RepeatRate = 2.5f;
    public float Model7RepeatRate = 2.5f;

    public byte Mode5CurrentClick = 0;

    public bool isGamePause;
    private static LevelManager instance;
    public static LevelManager Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    public void SetupLevel(byte mode, bool main)
    {
        gameMode = mode;
        isMainGame = main;
        switch(mode)
        {
            case 3:
                {
                    g_UIManager.Instance.StartLevel(0);
                    if (main)
                        AutoScroller();
                    break;
                }
            case 5:
                {
                    g_UIManager.Instance.StartLevel(1);
                    break;
                }
            case 6:
                {
                    g_UIManager.Instance.StartLevel(2);
                    break;
                }
            case 7:
                {
                    g_UIManager.Instance.StartLevel(3);
                    break;
                }
        }
        
    }

    void AutoScroller()
    {
        if(gameMode == 3)
        {
            ButtonsPlace.Instance.AutoScroll(Model3RepeatRate);
        }
    }
    public void StartTimer5()
    {

    }
}
