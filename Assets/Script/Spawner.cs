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
    [Tooltip("Cantumkan penghalang atau Portal level selanjutnya kalau ini spawner terakhir")]
    [SerializeField]NextLevel nextStageObj;

    [Tooltip("Aktifkan ini jike Spawner Bagian akhir, jika final set true lalu disable scriptnya juga gameobject nya")]
    [SerializeField]bool finalSpawner=false;

    // Start is called before the first frame update
    void Awake(){
        if(finalSpawner){
            nextStageObj.gameObject.SetActive(false);
        }
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
            float spawnDelay = 2;
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
        if(nextStageObj!=null){
            if(enemiesRemainingAlive<0&&finalSpawner){
                levelFinish();
            }else if(enemiesRemainingAlive<0&&nextStageObj.gateNextPassage){
                levelFinish();
            }
        }
    }

    void levelFinish(){
        if(!nextStageObj.gateNextPassage){
        nextStageObj.gameObject.SetActive(true);}
        nextStageObj.enabled=true;
    }
}
