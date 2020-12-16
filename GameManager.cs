using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isMaingame;
    public bool IsMainGame
    {
        get { return isMaingame; }
    }
    private byte gameMode;
    public byte GameMode
    {
        get { return gameMode; }
    }

    public byte Mode5MaxClick
    {
        get { return 2; }
    }


    private static GameManager instance;
    public static GameManager Instance
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

    // Update is called once per frame
    public void CreateLevel(byte mode, bool main)
    {
        gameMode = mode;
        isMaingame = main;
        LevelManager.Instance.SetupLevel(mode, main);
    }
}
