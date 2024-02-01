using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject creditsPanel;
    public GameObject levelPanel;
    public GameObject infoPanel;

    public AudioSource selected;

    // Start is called before the first frame update
    public void StartGame()
    {
        selected.Play();
        SceneManager.LoadScene(2);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        Manager.Instance.sceneIndex = 2;
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // only works on exe
        Application.Quit();
#endif
    }

    public void ShowCredits() {
        selected.Play();
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void ShowMain() {
        selected.Play();
        mainPanel.SetActive(true);
        creditsPanel.SetActive(false);
        levelPanel.SetActive(false);
        infoPanel.SetActive(false);
    }
    public void ShowLevelSelect()
    {
        selected.Play();
        mainPanel.SetActive(false);
        levelPanel.SetActive(true);
    }
    public void ShowInfo() 
    {
        selected.Play();
        mainPanel.SetActive(false);
        infoPanel.SetActive(true);
    }

    // Bandaid solution to level selecting
    // Blueprint for modular level loading is to record the indices of the level scenes then load as necessary.
    public void LevelChoose()
    {
        selected.Play();
        Manager.Instance.sceneIndex = 3;
        SceneManager.LoadScene(3);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }
}
