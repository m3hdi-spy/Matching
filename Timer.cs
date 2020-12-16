using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float timer = 0f;
    bool bTiming;

    private static Timer instance;
    public static Timer Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if(bTiming)
        timer += Time.deltaTime;
    }

    public void StartTimer()
    {
        timer = 0f;
        bTiming = true;
    }
    public float GetTimer()
    {
        bTiming = false;
        return timer % 60;
    }
}
