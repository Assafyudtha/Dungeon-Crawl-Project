using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField]UnityEvent onTriggerEnter;

    void OnTriggerEnter(Collider other){
        onTriggerEnter.Invoke();
    }
}
