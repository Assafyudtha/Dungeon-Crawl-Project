using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class NextLevel : MonoBehaviour
{
    [SerializeField]string nextScene;
    GameObject UIgame;
    GameObject winCondition;
    UIScript ui;
    public bool gateNextPassage=false;
    void Start(){
        ui = FindObjectOfType<UIScript>();
        UIgame = ui.UIGameplay;
        winCondition = ui.winCondition;
    }

    void OnEnable(){
        if(gateNextPassage){
            Destroy(gameObject);
        }else{
            gameObject.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider coll){
        if(gateNextPassage){
            return;
        }else{
            if(coll.tag == "Player"){
                UIgame.SetActive(false);
                winCondition.SetActive(true);
                ui.PauseWin();
            }
        }
    }

    void Update(){

    }
    public void nextLevel(){
        SceneManager.LoadScene(nextScene);
    }
}
