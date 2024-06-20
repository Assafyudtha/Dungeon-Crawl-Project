using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvents : MonoBehaviour
{
    EnemyScriptMerge enemy;
    [SerializeField]UnityEvent EventTrigger;
    // Start is called before the first frame update
    void Start()
    {
        enemy=GetComponent<EnemyScriptMerge>();
        enemy.OnDeath+=Event;
    }

    void Event(){
        EventTrigger.Invoke();
    }
}
