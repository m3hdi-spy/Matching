using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragandDrop : MonoBehaviour
{
    private bool isPointerDown;
    private bool isPointerUp;
    private bool isDraging;

    public bool Touchable;

    private Vector3 dragPos = Vector3.zero;

    private bool isInTrigger;

    public bool goToOriginal = false;
    private Vector3 originPos;
    void Start()
    {
        Touchable = true;
    }

    
    void Update()
    {
        if (Input.touchCount > 1) return;
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 touchPos = new Vector3(touch.position.x, touch.position.y, 0.5f);
                dragPos = Camera.main.ScreenToWorldPoint(touchPos);
            }
        }

        if (isDraging)
            transform.position = dragPos;

        if(goToOriginal)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originPos, Time.deltaTime * 5);

            if (transform.localPosition.y < originPos.y)
            {
                if (transform.localPosition.y > originPos.y - 0.02f)
                    EndGotoOriginalPosition();
            }
            else
            {
                if (transform.localPosition.y > originPos.y + 0.02f)
                    EndGotoOriginalPosition();
            }
        }
    }


    public void OnPointerDown()
    {
        if (!Touchable)
            return;
        
        isPointerDown = true;
        isPointerUp = false;
        isDraging = false;
    }

    public void OnPoinerUp()
    {
        isPointerDown = false;
        isPointerUp = true;
        isDraging = false;

        StartCoroutine(ResetPointerUp());

        if (!isInTrigger)
            BackToOriginalPosition(false);
    }
    public void OnPointerDrag()
    {
        if (!Touchable)
            return;

        if (gameObject.GetComponent<UnityEngine.UI.Image>().color == Color.red)
            gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.white;

        isPointerDown = false;
        isPointerUp = false;
        isDraging = true;
    }
    IEnumerator ResetPointerUp()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        isPointerUp = false;
    }
    public void BackToOriginalPosition(bool inWrongAnswer)
    {
        goToOriginal = true;
        originPos = gameObject.GetComponent<Mode6Buttons>().OriginalPosition;
        if (inWrongAnswer)
            gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.red;
    }

    private void EndGotoOriginalPosition()
    {
        goToOriginal = false;
        transform.localPosition = originPos;
        gameObject.GetComponent<UnityEngine.UI.Image>().color = Color.white;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isPointerUp)
        {
            isInTrigger = true;
            transform.GetComponentInParent<CheckAnswer6>().CheckAnswer(this.gameObject, collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInTrigger = false;
    }
}
