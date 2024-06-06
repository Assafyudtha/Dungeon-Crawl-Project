using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIScript : MonoBehaviour
{   
    [SerializeField]GameObject pause;
    string nextLevel;
    [SerializeField]Player playerInput;

    public void Pause(){
        Time.timeScale = 0f;
        playerInput.enabled=false;
        pause.SetActive(true);
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
}
