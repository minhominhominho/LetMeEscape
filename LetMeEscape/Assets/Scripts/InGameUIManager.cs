using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager Instance;
    [SerializeField] private Image speed;
    [SerializeField] private Image coolDown;
    [SerializeField] private TextMeshProUGUI reaminWall;
    [SerializeField] private GameObject puaseButton;
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private GameObject countPanel;
    [SerializeField] private TextMeshProUGUI count;
    [SerializeField] private GameObject ClearPanel;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    public IEnumerator GameStart()
    {
        Time.timeScale = 0;
        GameManager.Instance.puase();
        countPanel.SetActive(true);
        count.text = "3";
        yield return new WaitForSecondsRealtime(1f);
        count.text = "2";
        yield return new WaitForSecondsRealtime(1f);
        count.text = "1";
        yield return new WaitForSecondsRealtime(1f);
        countPanel.SetActive(false);
        GameManager.Instance.resume();
        Time.timeScale = 1;
    }

    void Update()
    {
        speed.fillAmount = PlayerController.Instance.PlayerCurrentSpeed / GameManager.Instance.playerStartSpeed;
        coolDown.fillAmount = 1 - WallCreater.Instance.Timer / GameManager.Instance.wallCreatingCoolTime;
        reaminWall.text = (GameManager.Instance.wallLimitNum - WallCreater.Instance.WallCount).ToString();
    }

    public void ClickPause()
    {
        Time.timeScale = 0;
        puaseButton.SetActive(false);
        resumeButton.SetActive(true);
        GameManager.Instance.isGamePaused = true;
    }

    public void ClickResume()
    {
        Time.timeScale = 1;
        puaseButton.SetActive(true);
        resumeButton.SetActive(false);
        GameManager.Instance.isGamePaused = false;
    }

    public void ShowClearUI()
    {
        ClearPanel.SetActive(true);
    }

    public void ClickNextStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ClickSelectStage()
    {
        print("dd");
        SavingData.isGoToSelectStage = true;
        SceneManager.LoadScene(0);
    }
}
