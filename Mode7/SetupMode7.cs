using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupMode7 : MonoBehaviour
{
   
    public GameObject ButtonsPrefab;
    public float TimerRatio = 0.2f;
    void Start()
    {
        
    }

    public void StartLevel()
    {
        for (byte i = 0; i < 3; i++)
        {
            GameObject first = Instantiate(ButtonsPrefab, transform.GetChild(0));
            GameObject second = Instantiate(ButtonsPrefab, transform.GetChild(1));

            first.GetComponent<ButtonsMode7>().ID = i;
            first.GetComponentInChildren<Text>().text = i.ToString();
           // first.GetComponent<Toggle>().group = transform.GetChild(0).GetComponent<ToggleGroup>();

            second.GetComponent<ButtonsMode7>().ID = i;
            second.GetComponentInChildren<Text>().text = i.ToString();
            // second.GetComponent<Toggle>().group = transform.GetChild(1).GetComponent<ToggleGroup>();

            gameObject.GetComponent<CheckAnswer7>().Buttons.Add(first);
            gameObject.GetComponent<CheckAnswer7>().Buttons.Add(second);
        }

        foreach (Transform t in transform.GetChild(0))
            t.SetSiblingIndex(Random.Range(0, 3));

        Timer.Instance.StartTimer();
        if(GameManager.Instance.IsMainGame)
        g_UIManager.Instance.isTimerStart = true;

        g_UIManager.Instance.TimerScale = TimerRatio;
        gameObject.GetComponent<CheckAnswer7>().LevelStarted();
    }

    public void ResetLevel()
    {
        foreach (Transform t in transform.GetChild(0))
            Destroy(t.gameObject);
        foreach (Transform t in transform.GetChild(1))
            Destroy(t.gameObject);

    }
}
