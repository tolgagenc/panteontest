using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject startPnl;

    private void OnEnable()
    {
        EventManager.start += StartGame;
    }

    private void OnDisable()
    {
        EventManager.start -= StartGame;
    }

    // Start is called before the first frame update
    void Start()
    {
        if ( !PlayerPrefs.HasKey("Level") )
        {
            PlayerPrefs.SetInt("Level", 0);
        }
    }
    
    public void StartGame()
    {
        startPnl.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(startPnl.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void NextScene()
    {
        if (PlayerPrefs.GetInt("Level")+1 >= SceneManager.sceneCountInBuildSettings)
        {
            PlayerPrefs.SetInt("Level", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level")+1);
        }
        Preferences.isFinish = false;
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
    }
    public void Replay()
    {
        Preferences.isFinish = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartButton()
    {
        EventManager.StartEvent();
    }
}
