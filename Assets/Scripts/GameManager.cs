using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
        }    
        else
        {
            Destroy(gameObject);
        }
    }    

    [SerializeField] Image gameOverPanel;
    [SerializeField] Image winPanel;

    void Start() 
    {
        HideGameOverPanel();
        HideWinPanel();    
    }

    public void ShowWinPanel()
    {
        winPanel.gameObject.SetActive(true);
    }

    private void HideWinPanel()
    {
        winPanel.gameObject.SetActive(false);
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.gameObject.SetActive(true);
    }

    public void HideGameOverPanel()
    {
        gameOverPanel.gameObject.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

}
