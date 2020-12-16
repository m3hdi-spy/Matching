using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCards : MonoBehaviour
{
    public GameObject CardsPrefab;
    [SerializeField]
    private List<GameObject> CardsList = new List<GameObject>();
    List<GameObject> CardsImage = new List<GameObject>();
    public float LastYPos;
    public byte MatchCardsNumber;

    public byte HideHintTimer = 3;
    byte removeCounter = 0;

    public float HintShowTimer = 2.5f;

    private Transform LowerPos;
    void Start()
    {
        LowerPos = transform.GetChild(0);
        

    }

   public void StartLevel()
   {
        CardsList.Clear();
        for (byte i = 0; i < 30; i++)
            InstantCards();
   }

    void InstantCards()
    {
        GameObject cd = Instantiate(CardsPrefab, transform);
        CardsList.Add(cd);
        removeCounter = 0;
    }

    public void RemoveExtraCards(GameObject sender)
    {
        removeCounter++;
        if (sender.transform.localPosition.y < LowerPos.localPosition.y)
        {
            Destroy(sender);
            CardsList.Remove(sender);
        }
        if (removeCounter == 30)
            CheckNumberOfCards();
    }

    void CheckNumberOfCards()
    {
        if (CardsList.Count % 2 != 0)
        {
            Destroy(CardsList[CardsList.Count - 1]);
            CardsList.Remove(CardsList[CardsList.Count - 1]);
        }
        MatchCardsNumber = (byte)(CardsList.Count / 2);
        SetWords();
    }
    public void SetWords()
    {
        byte allcards = (byte)CardsList.Count;
        
        for(byte i = 0; i < allcards; i+=2)
        {
            CardsList[i].GetComponent<Cards>().Word = i.ToString();
            CardsList[i].transform.GetChild(0).GetComponentInChildren<UnityEngine.UI.Text>().text = i.ToString();
            CardsList[i].transform.SetSiblingIndex(Random.Range(0, allcards - 1));

            if (i + 1 < allcards)
            {
                CardsList[i + 1].GetComponent<Cards>().Word = i.ToString();
                CardsList[i + 1].transform.GetChild(0).GetComponentInChildren<UnityEngine.UI.Text>().text = i.ToString();
                CardsList[i + 1].transform.SetSiblingIndex(Random.Range(0, allcards - 1));
            }
            
        }


        for (byte k = 0; k < allcards; k++)
        {
            if (CardsList[k].name == "LowerPlace") continue;
            CardsImage.Add(CardsList[k].transform.GetChild(0).gameObject);
        }
        g_UIManager.Instance.Pressedhint = true;
        StartCoroutine(Hidecards(CardsImage));
        
    }

    public IEnumerator Hidecards(List<GameObject> cds)
    {
        yield return new WaitForSeconds(HideHintTimer);
        gameObject.GetComponent<UnityEngine.UI.GridLayoutGroup>().enabled = false;
        g_UIManager.Instance.LerpColorChange(cds, true);
        LevelManager.Instance.Mode5CurrentClick = 0;
        gameObject.GetComponent<CheckAnswer5>().ResetValues();
        
    }

    public IEnumerator Hint()
    {
        for (byte u = 0; u < CardsImage.Count; u++)
        {
            if (CardsImage[u] == null) continue;
            CardsImage[u].GetComponentInParent<Cards>().FadeCard(true);
            CardsImage[u].GetComponentInParent<UnityEngine.UI.Button>().enabled = false;
        }
        
        yield return new WaitForSeconds(HintShowTimer);
        g_UIManager.Instance.LerpColorChange(CardsImage, true);
    }
    public void ResetCards()
    {
        foreach (Transform child in transform)
        {
            if (child.name != "LowerPlace")
                Destroy(child.gameObject);
        }
        CardsList.Clear();
        CardsImage.Clear();
        transform.GetComponent<UnityEngine.UI.GridLayoutGroup>().enabled = true;
    }
    public void EnableButton(bool isEnable, GameObject sender)
    {
            sender.GetComponent<UnityEngine.UI.Button>().enabled = isEnable;
    }
    public void EnableAllButtons(bool isEnable)
    {
        foreach(Transform btns in transform)
        {
            if (btns.name == "LowerPlace") continue;
            btns.GetComponent<UnityEngine.UI.Button>().enabled = isEnable;
        }
    }
}
