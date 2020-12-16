using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupMode6 : MonoBehaviour
{
    private byte SentenceCount = 6;


    //private GameObject Firstgroup;
    //private GameObject Secondgroup;

    public GameObject ButtonPrefab;
    public GameObject AnsPlace;

    private List<GameObject> Childs = new List<GameObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateSentence(byte scount)
    {

        transform.GetChild(0).GetComponent<UnityEngine.UI.LayoutGroup>().enabled = true;

        if (scount == 0) scount = SentenceCount;

        for (byte i = 0; i < (scount / 2); i++)
        {
            GameObject btns = Instantiate(ButtonPrefab, transform.GetChild(0));
            btns.GetComponent<Mode6Buttons>().ID = i;
            btns.GetComponent<DragandDrop>().Touchable = true;
            btns.GetComponent<UnityEngine.UI.Button>().enabled = true;
            btns.GetComponent<BoxCollider2D>().enabled = true;

            GameObject aPlace = Instantiate(AnsPlace, transform.GetChild(1));
            aPlace.GetComponent<Mode6AnswerPlace>().ID = i;
            aPlace.GetComponent<BoxCollider2D>().enabled = true;

            Childs.Add(btns);
            Childs.Add(aPlace);
        }
        

        StartCoroutine(LevelStart());
    }

    private IEnumerator LevelStart()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        transform.GetChild(0).GetComponent<UnityEngine.UI.LayoutGroup>().enabled = false;

        foreach (Transform t in transform.GetChild(0))
            t.GetComponent<Mode6Buttons>().SaveOriginalPosition();

        Timer.Instance.StartTimer();
        g_UIManager.Instance.ResetTimer();
        gameObject.GetComponent<CheckAnswer6>().Corrects = 0;
        if(GameManager.Instance.IsMainGame)
        g_UIManager.Instance.isTimerStart = true;

        /*foreach (Transform t in transform.GetChild(1))
            t.GetComponent<Mode6Buttons>().SaveOriginalPosition();
        */
    }

    public void ResetButtnons()
    {
        
        for(byte i = 0; i < Childs.Count; i++)
        {
            Destroy(Childs[i].gameObject);
        }
        
        Childs.Clear();
    }

}
