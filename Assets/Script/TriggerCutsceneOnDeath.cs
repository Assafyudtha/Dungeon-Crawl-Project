using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerCutsceneOnDeath : MonoBehaviour
{
    EnemyScriptMerge enemy;
    [SerializeField] UnityEvent cutsceneTrigger;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<EnemyScriptMerge>();
        enemy.OnDeath += cutsceneTrigger.Invoke;
    }
}
