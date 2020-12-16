using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SaveAction(string action)
    {
        PlayerPrefs.SetString("LastActions", PlayerPrefs.GetString("LastActions") + '\n' + action);
    }

    public string ShowActions()
    {
        string txt = string.Empty;
        txt = PlayerPrefs.GetString("LastActions");
        PlayerPrefs.DeleteKey("LastActions");
        return txt;
    }
}
