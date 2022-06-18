using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MainMenu { Title, Option, StageSelection }

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject TitlePanel;
    [SerializeField] private GameObject OptionPanel;
    [SerializeField] private GameObject StageSelectPanel;
    [SerializeField] private GameObject StoryPanel;
    [SerializeField] private List<GameObject> Story;
    [SerializeField] private List<GameObject> Locks;


    private void Awake()
    {
        if (SavingData.arrivedFromGame)
        {
            ClickPanel(2);
            SavingData.arrivedFromGame = false;
        }
        else
            ClickPanel(0);
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
        TitlePanel.SetActive(false);
        OptionPanel.SetActive(false);
        StageSelectPanel.SetActive(false);

        switch (index)
        {
            case (int)MainMenu.Title:
                TitlePanel.SetActive(true);
                break;
            case (int)MainMenu.Option:
                OptionPanel.SetActive(true);
                break;
            case (int)MainMenu.StageSelection:
                StageSelectPanel.SetActive(true);
                break;
        }
    }

    public void ClickStory()
    {
        StartCoroutine(stortyCoroutine());
    }

    private IEnumerator stortyCoroutine()
    {
        StoryPanel.SetActive(true);
        int index = 0;
        while (index < Story.Count)
        {
            setActiveStory(index);
            if (Input.GetMouseButtonDown(0))
            {
                index++;
            }
            yield return null;
        }
        StoryPanel.SetActive(false);
    }

    private void setActiveStory(int i)
    {
        foreach (GameObject s in Story)
        {
            s.SetActive(false);
        }

        Story[i].SetActive(true);
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
