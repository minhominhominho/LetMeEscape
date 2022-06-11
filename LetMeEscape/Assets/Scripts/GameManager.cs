using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public static class SavingData
{
    public static int StageRecord = 0;
    public static List<double> TimeRecords = new List<double>();
    public static string convertTimeToString(double seconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
        return string.Format("{1:D2}:{2:D2}", timeSpan.Minutes, timeSpan.Seconds);
    }

    public static bool isGoToSelectStage = false;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float playerStartSpeed;
    public float speedToZeroSec;
    public int wallCreatingCoolTime;
    public int wallLimitNum;
    private bool isGameEnded;
    public bool isGamePaused;
    private double timeRecord;


    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }

        isGameEnded = false;
    }

    private void Start()
    {
        StartCoroutine(InGameUIManager.Instance.GameStart());
    }

    public bool IsGameRunning()
    {
        return !isGameEnded && !isGamePaused;
    }

    public void puase()
    {
        isGamePaused = true;
    }

    public void resume()
    {
        isGamePaused = false;
    }

    public void CallGameOver()
    {
        if (!isGameEnded)
        {
            print("Game Over");
            isGameEnded = true;
            StartCoroutine(GameOverCoroutine());
        }
    }

    private IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void CallGameClear()
    {
        if (!isGameEnded)
        {
            print("Game Clear");
            isGameEnded = true;
            StartCoroutine(StageClearCoroutine());
        }
    }

    private IEnumerator StageClearCoroutine()
    {
        SavingData.StageRecord = SceneManager.GetActiveScene().buildIndex > SavingData.StageRecord ? SceneManager.GetActiveScene().buildIndex : SavingData.StageRecord;
        SoundManager.Instance.PlaySFX(SFXType.GameClear);
        InGameUIManager.Instance.ShowClearUI();
        yield return null;
    }
}
