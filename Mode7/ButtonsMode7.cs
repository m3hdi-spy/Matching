using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsMode7 : MonoBehaviour
{
    public byte ID;
    public bool Answered;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnClickMe()
    {
        // if (transform.parent.GetComponent<UnityEngine.UI.ToggleGroup>().AnyTogglesOn())
        //transform.parent.GetComponent<UnityEngine.UI.ToggleGroup>().SetAllTogglesOff();
           // transform.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
        
        transform.parent.parent.GetComponent<CheckAnswer7>().CheckAnswer(this.gameObject);
    }
}
