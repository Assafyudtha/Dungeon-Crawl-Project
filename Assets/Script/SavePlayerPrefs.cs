using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayerPrefs : MonoBehaviour
{
    [SerializeField]string namaLevel;

    void OnEnable(){
        if(PlayerPrefs.GetInt(namaLevel)==0){
            PlayerPrefs.SetInt(namaLevel,1);
            PlayerPrefs.Save();
        }
    }
}
