using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField]string nextScene;
    void OnTriggerEnter(Collider coll){
        if(coll.tag == "Player"){
            SceneManager.LoadScene(nextScene);
        }
    }

    void Update(){

    }
}
