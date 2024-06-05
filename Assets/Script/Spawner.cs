using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public EnemyScriptMerge enemy;
    Player player;
    [SerializeField]int enemiesRemainingToSpawn;
    int enemiesRemainingAlive;
    [SerializeField]NextLevel nextStageObj;

    // Start is called before the first frame update
    void Awake(){
        nextStageObj = FindObjectOfType<NextLevel>();
        nextStageObj.gameObject.SetActive(false);
    }
    void Start()
    {
        player=FindObjectOfType<Player>();
        player.OnDeath += OnPlayerDeath;
        enemiesRemainingAlive=0;

    }

    void OnEnable(){
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy(){
        while(enemiesRemainingToSpawn>0){
            float spawnDelay = 5;
            EnemyScriptMerge spawnedEnemy = Instantiate(enemy, transform.position,Quaternion.identity)as EnemyScriptMerge;
            spawnedEnemy.nonSpawnEnemy=false;
            spawnedEnemy.OnDeath +=OnEnemyDeath;
            enemiesRemainingAlive++;
            enemiesRemainingToSpawn--;
            yield return new WaitForSeconds(spawnDelay);

            if(enemiesRemainingAlive>5){
                yield return new WaitUntil(()=>enemiesRemainingAlive<5);
            }
        }
        if(enemiesRemainingAlive==0){
            print("Finish");
        }
    }
    void OnPlayerDeath(){
        this.enabled =false;
    }

    void OnEnemyDeath(){
        enemiesRemainingAlive --;
        if(enemiesRemainingAlive<=0){
            levelFinish();
        }
    }

    void levelFinish(){
        nextStageObj.gameObject.SetActive(true);
    }
}
