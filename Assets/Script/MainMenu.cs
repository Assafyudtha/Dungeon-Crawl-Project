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
        PlayerPrefs.SetFloat("sound volume", volume.value);
    }

    public void setSlider(){
        volume.value= PlayerPrefs.GetFloat("sound volume");
    }
}
