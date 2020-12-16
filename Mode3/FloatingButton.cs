using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingButton : MonoBehaviour
{
    [SerializeField]
    Vector3 OriginalPos = Vector3.zero;
    float RealtimeYpos;
    
    float Ypos;
    Vector3 dragPos = Vector3.zero;

    public bool isDragging;
    public bool isPointerDown;
    public bool isPointerUp;
    public bool backToOriginals;

    public bool isLast;

    public byte CurrectPlaceID;
    bool IsInPlace;
    byte PlaceID;


    public List<string> logs = new List<string>();


    private bool DebugStayTrigger;
    void Start()
    {
        byte id = (byte)Random.Range(1, 4);
        this.CurrectPlaceID = id;
        transform.GetChild(0).gameObject.GetComponent<Text>().text = id.ToString();
        RealtimeYpos = transform.localPosition.y;
        OriginalPos = this.transform.localPosition;
        logs.Add("FirstPos: " + OriginalPos);
    }

    
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 touchPos = new Vector3(touch.position.x, touch.position.y, 0.5f);
                dragPos = Camera.main.ScreenToWorldPoint(touchPos);
            }
        }

        if (isDragging && isPointerDown)
            transform.position = dragPos;

        if(backToOriginals)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, OriginalPos, 120);
            
            if (transform.localPosition.y >= OriginalPos.y - 0.1f && transform.localPosition.y <= OriginalPos.y + 0.1f)// || (transform.localPosition.y <= OriginalPos.y - 0.1f && transform.localPosition.y >= OriginalPos.y - 0.05f))
                IsInOriginalPosition();
        }
        
    }

   
    public void PointerDrag()
    {
        logs.Add("draging");
        isDragging = true;
        backToOriginals = false;
    }
    public void PointerDown()
    {
        logs.Add("PDown");
        isPointerDown = true;
        isPointerUp = false;
        g_UIManager.Instance.ResetDropsColor();
    }
    public void PointerUp()
    {
        logs.Add("PuP");
        isPointerUp = true;
        isPointerDown = false;
        isDragging = false;
        Check();
    }

    public void OnPlace(byte id)
    {
        IsInPlace = true;
        PlaceID = id;
    }
    public void NotOnPlace()
    {
        IsInPlace = false;
        PlaceID = 0;
        
    }

    public void GobacktoOriginalPosition()
    {
        logs.Add("Start Back to Originals");
        logs.Add("Current Pos: " + transform.localPosition);
        logs.Add("Original: " + OriginalPos);
        backToOriginals = true;
    }
    private void IsInOriginalPosition()
    {
        logs.Add("Original End!");
        logs.Add("Current Position after: " + transform.localPosition);
        backToOriginals = false;
    }


    void Check()
    {
        if(IsInPlace)
        {
            CheckAnswer.Instance.CheckPlacement(this.gameObject, System.Convert.ToByte(gameObject.GetComponentInChildren<Text>().text), PlaceID);
        }
        else
        {
            GobacktoOriginalPosition();
        }
    }

    public void SaveOriginalPosition(float Y)
    {
        OriginalPos.y -= Y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DebugStayTrigger = true;
        if (collision.CompareTag("Respawn"))
        {
            if(isLast && !isDragging)
            gameObject.GetComponentInParent<ButtonsPlace>().InstantButton();
        }
        gameObject.GetComponentInParent<ButtonsPlace>().GUISetup(isPointerDown, isPointerUp, isDragging, backToOriginals, DebugStayTrigger);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        DebugStayTrigger = false;
        gameObject.GetComponentInParent<ButtonsPlace>().GUISetup(isPointerDown, isPointerUp, isDragging, backToOriginals, DebugStayTrigger);
    }

    
}
