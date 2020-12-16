using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Debugger
{
   

    public static void SaveAction(string action)
    {
        PlayerPrefs.SetString("LastActions", PlayerPrefs.GetString("LastActions") + '\n' + action);
    }

    public static string ShowActions()
    {
        string txt = string.Empty;
        txt = PlayerPrefs.GetString("LastActions");
        PlayerPrefs.DeleteKey("LastActions");
        return txt;
    }
}
