using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cards : MonoBehaviour
{

    public string Word; 
    public bool isInArea;

    private GameObject imgBack;
    private GameObject txtWord;

    bool bFading;
    bool bFadeOut;

    public Color txtColor, imgColor;
    float TargetA;

    public bool BadGetway;
    void Start()
    {
        StartCoroutine(SendYPosition());
        imgBack = transform.GetChild(0).gameObject;
        txtWord = imgBack.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 1)
            StartCoroutine(GetTwoTouch());

        if(bFading)
        {
            txtColor.a = TargetA;
            imgColor.a = TargetA;
            txtWord.GetComponent<Text>().color = txtColor;
            imgBack.GetComponent<Image>().color = imgColor;
            bFading = false;
            /*if (TargetA == 1)
                if (LevelManager.Instance.Mode5CurrentClick == 2)
                    transform.GetComponentInParent<GenerateCards>().EnableButton(true, null);
            */
        }
        
        
        
    }


    private IEnumerator SendYPosition()
    {
        yield return new WaitForEndOfFrame();
        transform.GetComponentInParent<GenerateCards>().RemoveExtraCards(this.gameObject);
        
    }
    public void Click()
    {
        if (BadGetway) return;
        if (LevelManager.Instance.Mode5CurrentClick == GameManager.Instance.Mode5MaxClick)
            return;

        LevelManager.Instance.Mode5CurrentClick++;

        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            this.gameObject.GetComponent<Button>().enabled = false;

            if (LevelManager.Instance.Mode5CurrentClick == 1)
                transform.parent.GetComponent<CheckAnswer5>().FirstClick(this.gameObject, Word);
            else if (LevelManager.Instance.Mode5CurrentClick == 2)
                transform.parent.GetComponent<CheckAnswer5>().SecondClick(this.gameObject, Word);
            else
            {
                this.gameObject.GetComponent<Button>().enabled = true;
                LevelManager.Instance.Mode5CurrentClick = 0;
                return;
            }
        }

        if((LevelManager.Instance.Mode5CurrentClick == GameManager.Instance.Mode5MaxClick && transform.parent.GetComponent<CheckAnswer5>().SecondSelected == null) || LevelManager.Instance.Mode5CurrentClick == 0)
        {
            transform.parent.GetComponent<CheckAnswer5>().BadTouch();
            return;
        }
        // if (LevelManager.Instance.Mode5CurrentClick == GameManager.Instance.Mode5MaxClick)
        // StartCoroutine(isButtonsEnable(false));
                FadeCard(true);
    }
    IEnumerator GetTwoTouch()
    {
        BadGetway = true;
        yield return new WaitForSeconds(0.1f);
        BadGetway = false;
    }

    public void FadeCard(bool show)
    {
        bFading = true;

        if (show)
        {
            TargetA = 1;
            txtWord.GetComponent<Text>().color = txtColor;
            imgBack.GetComponent<Image>().color = imgColor;
        }
        else
        {
            TargetA = 0;
            txtWord.GetComponent<Text>().color = Color.white;
            imgBack.GetComponent<Image>().color = Color.white;
        }

        
    }

   
}
