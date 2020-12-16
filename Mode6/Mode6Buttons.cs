using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mode6Buttons : MonoBehaviour
{
    public byte ID;

    public Vector3 OriginalPosition;

    void Start()
    {
        gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = ID.ToString();
        
    }

    public void SaveOriginalPosition()
    {
        this.OriginalPosition = this.transform.localPosition;
    }
}
