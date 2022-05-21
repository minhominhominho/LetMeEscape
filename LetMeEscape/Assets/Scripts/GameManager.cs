using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class SavingData
{
    public static int StageRecord = 0;
    public static List<double> TimeRecords = new List<double>();
    public static string convertTimeToString(double seconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
        return string.Format("{1:D2}:{2:D2}", timeSpan.Minutes, timeSpan.Seconds);
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGameEnded { get; private set; }
    public bool isGamePaused;
    public double timeRecord;


    void Start()
    {
        instance = this;
        isGameEnded = false;
    }

    void Update()
    {

    }

    public bool IsGameRunning()
    {
        return !isGameEnded && !isGamePaused;
    }

    public void CallGameOver()
    {
        if (!isGameEnded)
        {
            isGameEnded = true;
            StartCoroutine(GameOverCoroutine());
        }
    }

    private IEnumerator GameOverCoroutine()
    {
        yield return null;
    }

    public void CallGameClear()
    {
        if (!isGameEnded)
        {
            isGameEnded = true;
            StartCoroutine(StageClearCoroutine());
        }
    }

    private IEnumerator StageClearCoroutine()
    {
        yield return null;
    }
}
