using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsPlace : MonoBehaviour
{
    public GameObject ButtonsPrefab;
    private short BasicYpos = 200;
    private short[] Xpositions = { -450, -400, -370, -330, -250, -200, -170, -130, -100, -60, -25 };

    public float ScrollSpeed = 0.25f;
    public bool Scrolling;
    private static ButtonsPlace instance;
    public static ButtonsPlace Instance
    {
        get { return instance; }
    }

    public GameObject gmdeb;
    public Text txt;
    public GUIStyle gStyle;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (Scrolling)
            ScrollDown();
        
    }
    public void StartLevel()
    {
        Time.timeScale = 1;
        for (byte i = 0; i < 7; i++)
            InstantButton();
    }
    
    public void InstantButton()
    {
        GameObject btn = Instantiate(ButtonsPrefab, transform);
        btn.transform.localPosition = SetPosition();
        btn.GetComponent<FloatingButton>().isLast = true;
        if (transform.childCount > 1)
            transform.GetChild(transform.childCount - 2).GetComponent<FloatingButton>().isLast = false;

        Debugger.SaveAction("Instant new Button");

        Timer.Instance.StartTimer();
    }

    Vector2 SetPosition()
    {
        Vector2 pos;
        float lastY;

        if (transform.childCount > 1)
            lastY = transform.GetChild(transform.childCount - 2).localPosition.y;
        else
            lastY = -300;

        short y = (short)(BasicYpos + lastY);

        short x = Xpositions[Random.Range(0, Xpositions.Length - 1)];
        byte abs = (byte)Random.Range(0, 11);
        if (abs % 2 != 0)
            x = (short)Mathf.Abs(x);

        pos = new Vector2(x, y);

        return pos;
    }

    public void MoveToTop(Transform obj)
    {
        
        obj.GetComponent<FloatingButton>().isLast = true;
        obj.SetSiblingIndex(transform.childCount - 1);
        if (transform.childCount > 1)
            transform.GetChild(transform.childCount - 2).GetComponent<FloatingButton>().isLast = false;

        Debugger.SaveAction("Button: " + obj.GetComponent<FloatingButton>().CurrectPlaceID + "Move to Top.");
        obj.localPosition = SetPosition();
        obj.GetComponent<FloatingButton>().SetOrigialPosition(obj.localPosition);
    }
    void ScrollDown()
    {
        foreach (Transform tc in transform)
        {
          //  if (tc.GetComponent<FloatingButton>().backToOriginals)
           //     continue;
            tc.localPosition -= new Vector3(0, ScrollSpeed, 0);
            tc.GetComponent<FloatingButton>().SaveOriginalPosition(ScrollSpeed);

        }

        //InstantButton((byte)(transform.childCount-1));
    }

    public void OneScrollDown(bool instante)
    {
        foreach (Transform tc in transform)
        {
            
            tc.localPosition -= new Vector3(0, 200, 0);
            tc.GetComponent<FloatingButton>().SaveOriginalPosition(200);
        }
        if(instante)
        InstantButton();
    }

    public void AutoScroll(float rRate)
    {
        ScrollSpeed = rRate;
        Scrolling = true;
    }
    public void StopScrolling()
    {
        Scrolling = false;
    }


    bool pdown, pup, drag, originalpos, staytri;

    public void GUISetup(bool pd, bool pu, bool dr, bool org, bool str)
    {
        pdown = pd;
        pup = pu;
        drag = dr;
        originalpos = org;
        staytri = str;
    }


    private void OnGUI()
    {
        GUI.Label(new Rect(new Vector2(10, 80), new Vector2(220, 35)), "On Pointer Down: " + pdown, gStyle);
        GUI.Label(new Rect(new Vector2(10, 110), new Vector2(220, 35)), "On Pointer Up: " + pup, gStyle);
        GUI.Label(new Rect(new Vector2(10, 140), new Vector2(220, 35)), "On Pointer Dragging: " + drag, gStyle);

        GUI.Label(new Rect(new Vector2(10, 170), new Vector2(220, 35)), "Back to Original: " + originalpos, gStyle);

        GUI.Label(new Rect(new Vector2(10, 200), new Vector2(220, 35)), "Stay Trigger: " + staytri, gStyle);
    }

    public void ded()
    {
        gmdeb.SetActive(!gmdeb.activeInHierarchy);
        txt.text = Debugger.ShowActions();
    }
}
