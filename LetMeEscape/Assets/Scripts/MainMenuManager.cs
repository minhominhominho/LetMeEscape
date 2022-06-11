using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MainMenu { Title, Option, StageSelection }

public class MainMenuManager : MonoBehaviour
{
    public GameObject TitlePanel;
    public GameObject OptionPanel;
    public GameObject StageSelectPanel;
    public List<GameObject> Locks;

    private MainMenu currentIndex;



    private void Awake()
    {
        if (!SavingData.isGoToSelectStage)
        {
            ClickPanel(0);
            SavingData.isGoToSelectStage = false;
        }
        else
            ClickPanel(2);
    }
    private void Start()
    {
        for (int i = 0; i < Locks.Count; i++)
            if (SavingData.StageRecord >= i)
            {
                Locks[i].SetActive(false);
            }
    }

    public void ClickPanel(int index)
    {
        bool changeBGM = false;
        TitlePanel.SetActive(false);
        OptionPanel.SetActive(false);
        StageSelectPanel.SetActive(false);

        switch (index)
        {
            case (int)MainMenu.Title:
                changeBGM = currentIndex == MainMenu.StageSelection ? true : false;
                TitlePanel.SetActive(true);
                break;
            case (int)MainMenu.Option:
                OptionPanel.SetActive(true);
                break;
            case (int)MainMenu.StageSelection:
                changeBGM = currentIndex == MainMenu.Title ? true : false;
                StageSelectPanel.SetActive(true);
                break;
        }

        if (changeBGM)
        {
            SoundManager.Instance.PlayBGM();
        }

        currentIndex = (MainMenu)index;
    }

    public void ClickStage(int stageNum)
    {
        SceneManager.LoadScene(stageNum);
    }

    public void ClickExit()
    {
        Application.Quit();
    }
}
