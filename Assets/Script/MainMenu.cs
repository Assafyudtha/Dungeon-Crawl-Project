using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.Rendering;
using UnityEngine.Rendering;

public class MainMenu : MonoBehaviour
{
    public Slider volume;
    public void NewGame(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex+1);
    }

    public void Option(){

    }

    public void ExitGame(){
        Application.Quit();
    }

    public void ContinueGame(){

    }

    public void SetVolume(){
        float vol=volume.value;
        PlayerPrefs.SetFloat("sound volume", vol);
        AudioListener.volume = vol;
    }

    public void setSlider(){
        volume.value= PlayerPrefs.GetFloat("sound volume");
    }

    public void playLevel1(){
        SceneManager.LoadScene(1);
    }

    public void playLevel2(){
        SceneManager.LoadScene(1);
    }

    public void playLevel3(){
        SceneManager.LoadScene(1);
    }

    public void playLevel4(){
        SceneManager.LoadScene(1);
    }

    public void playLevel5(){
        SceneManager.LoadScene(1);
    }
}
