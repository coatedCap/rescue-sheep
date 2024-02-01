using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseUIScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject pausePanel;
    public GameObject infoPanel;
    public GameObject levelPanel;
    public GameObject level2Panel;
    public GameObject upgradePanel;
    public GameObject HUD;
    public GameObject NeedMoreSheep;
    public bool isPaused;
    public float savedTimeScale = 0f;
    private Inventory pInv;
    private Movement pMove;
    private TextMeshProUGUI[] varHUD;
    private TextMeshProUGUI sheepNeeded;
    public AudioSource pauseSFX;
    public AudioSource leavePause;
    public AudioSource selected;
    public AudioSource complete;

    public void Start()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        pInv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        pMove = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        varHUD = HUD.GetComponentsInChildren<TextMeshProUGUI>();
        sheepNeeded = NeedMoreSheep.GetComponentInChildren<TextMeshProUGUI>();
        /* 
         0. Health
         1. Stamina
         2. Sheep
         3. Berries
         If the setup of the children are changed, this must also be changed.
         */
        /*
       for (int i = 0; i < varHUD.Length; i++)
       {
           print(varHUD[i].text);
       }
        */
    }

    // Update is called once per frame
    public void Update()
    {
        varHUD[0].text = ": " + pInv.health;
        varHUD[1].text = ": " + (int)pMove.stamina;
        varHUD[2].text = pInv.sheepRescued + " :";
        varHUD[3].text = ": " + pInv.berryCount;
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (isPaused)
            {
                UnPause();
            }
            else
            { 
                Pause();
            }
        }
    }


    public void UpgradeDisplay()
    {
        selected.Play();
        levelPanel.SetActive(false);
        upgradePanel.SetActive(true);
    }
    public void HideUpgrade()
    {
        selected.Play();
        levelPanel.SetActive(true);
        upgradePanel.SetActive(false);
    }


    public void Pause()
    {
        Cursor.visible = true;
        isPaused = true;
        pausePanel.SetActive(true);
        savedTimeScale = Time.timeScale;
        Time.timeScale = 0;
        pauseSFX.Play();
    }
    public void UnPause()
    {
        Cursor.visible = false;
        leavePause.Play();
        isPaused = false;
        pausePanel.SetActive(false);
        infoPanel.SetActive(false);
        Time.timeScale = savedTimeScale;
    }

    public void Info() {
        selected.Play();
        pausePanel.SetActive(false);
        infoPanel.SetActive(true);
    }
    public void BacktoPause() {
        selected.Play();
        pausePanel.SetActive(true);
        infoPanel.SetActive(false);
    }

    public void ReplayLevel() {
        SceneManager.LoadScene(Manager.Instance.sceneIndex);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        Time.timeScale = savedTimeScale;
    }

    public void NextLevel() {
        SceneManager.LoadScene(3);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        Manager.Instance.sceneIndex = 3;
        Time.timeScale = savedTimeScale;
    }

    public void BackToTitle() {
        SceneManager.LoadScene(0);
        Time.timeScale = savedTimeScale;
    }

    public void LevelComplete() {
        complete.Play();
        HUD.SetActive(false);
        levelPanel.SetActive(true);
        Cursor.visible = true;
        savedTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

     public void LevelComplete2() {
        complete.Play();
        HUD.SetActive(false);
        level2Panel.SetActive(true);
        Cursor.visible = true;
        savedTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    public void NeedSheep() {
        sheepNeeded.text = pInv.sheepRescued + "/2 Sheep Rescued";
        NeedMoreSheep.SetActive(true);
    }

    public void GetSheep() {
        NeedMoreSheep.SetActive(false);
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
}
