using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIScript : MonoBehaviour
{   
    [SerializeField]GameObject pause;
    [SerializeField]GameObject UIGameplay;
    string nextLevel;
    [SerializeField]Player playerInput;
    [SerializeField]GameObject gameoverUI;

    void Awake(){
        playerInput = FindObjectOfType<Player>();
        playerInput.OnDeath += GameOver;
    }

    public void Pause(){
        Time.timeScale = 0f;
        playerInput.enabled=false;
        pause.SetActive(true);
        UIGameplay.SetActive(false);
    }
    public void PauseWin(){
        Time.timeScale = 0f;
        playerInput.enabled=false;
    }

    public void Resume(){
        Time.timeScale=1f;
        playerInput.enabled=true;

    }
    public void Restart(){
        Time.timeScale=1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void BackToMenu(){
        Time.timeScale=1f;
        SceneManager.LoadScene("Main");
    }
    public void NextLevel(){
        Time.timeScale=1f;
        SceneManager.LoadScene(nextLevel);
    }

    void GameOver(){
        Time.timeScale = 0f;
        gameoverUI.SetActive(true);
        UIGameplay.SetActive(false);
    }

    public void NextScene(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex+1);
    }
}
