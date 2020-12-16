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


    public static string log1;


    private bool DebugStayTrigger;
    void Start()
    {
        byte id = (byte)Random.Range(1, 4);
        this.CurrectPlaceID = id;
        transform.GetChild(0).gameObject.GetComponent<Text>().text = id.ToString();
        RealtimeYpos = transform.localPosition.y;
        OriginalPos = this.transform.localPosition;
        log1 += "First Original: " + OriginalPos;
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
            log1 += "\n Go To Original? " + OriginalPos;
            if (transform.localPosition.y >= OriginalPos.y - 0.1f && transform.localPosition.y <= OriginalPos.y + 0.1f)// || (transform.localPosition.y <= OriginalPos.y - 0.1f && transform.localPosition.y >= OriginalPos.y - 0.05f))
                IsInOriginalPosition();
        }
        
    }

   
    public void PointerDrag()
    {

        isDragging = true;
        backToOriginals = false;
        log1 += "\n Dragging";
    }
    public void PointerDown()
    {
        isPointerDown = true;
        isPointerUp = false;
        g_UIManager.Instance.ResetDropsColor();
        log1 += "\n PointerDown";
    }
    public void PointerUp()
    {
        isPointerUp = true;
        isPointerDown = false;
        isDragging = false;
        Check();
        log1 += "\n PointerUp";
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
        backToOriginals = true;
        log1 += "\n Go Back To original: True";
    }
    private void IsInOriginalPosition()
    {
        backToOriginals = false;
        log1 += "\n Go Back To Original: False";
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
