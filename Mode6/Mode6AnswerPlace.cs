using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mode6AnswerPlace : MonoBehaviour
{
    public byte ID;
    public bool IsActive = true;
    private void Start()
    {
        gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = ID.ToString();
    }

}
