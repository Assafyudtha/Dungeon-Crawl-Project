using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField]string nextScene;
    [SerializeField]GameObject UIgame;
    [SerializeField]GameObject winCondition;
    UIScript ui;
    void Start(){
        ui = FindObjectOfType<UIScript>();
    }

    void OnTriggerEnter(Collider coll){
        if(coll.tag == "Player"){
            UIgame.SetActive(false);
            winCondition.SetActive(true);
            ui.PauseWin();
        }
    }

    void Update(){

    }
    public void nextLevel(){
        SceneManager.LoadScene(nextScene);
    }
}
