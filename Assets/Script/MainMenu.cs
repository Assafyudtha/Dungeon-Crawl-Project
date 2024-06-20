using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.Rendering;
using UnityEngine.Rendering;

public class MainMenu : MonoBehaviour
{
    [SerializeField]Button l1;
    [SerializeField]Button l2;
    [SerializeField]Button l3;
    [SerializeField]Button l4;
    [SerializeField]Button l5;


    void Start(){
        checkSaved();
    }
    public void checkSaved(){
        if(PlayerPrefs.GetInt("L1")==1){
            l1.interactable=true;
        }
        if(PlayerPrefs.GetInt("L2")==1){
            l2.interactable=true;
        }
        if(PlayerPrefs.GetInt("L3")==1){
            l3.interactable=true;
        }
        if(PlayerPrefs.GetInt("L4")==1){
            l4.interactable=true;
        }
        if(PlayerPrefs.GetInt("L5")==1){
            l5.interactable=true;
        }
    }
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
    public void ResetProgress(){
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    public void ContinueGame(){

    }

    public void SetVolume(){
        float vol=volume.value;
        PlayerPrefs.SetFloat("sound volume", vol);
        PlayerPrefs.Save();
        AudioListener.volume = vol;
    }

    public void setSlider(){
        volume.value= PlayerPrefs.GetFloat("sound volume");
    }

    public void playLevel1(){
        SceneManager.LoadScene(1);
    }

    public void playLevel2(){
        SceneManager.LoadScene(4);
    }

    public void playLevel3(){
        SceneManager.LoadScene(5);
    }

    public void playLevel4(){
        SceneManager.LoadScene(6);
    }

    public void playLevel5(){
        SceneManager.LoadScene(7);
    }
}
