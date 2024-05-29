using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyScriptMerge : LivingEntity
{
    public enum State {Idle ,Chasing,Attaacking};
    State currentState;
    [SerializeField]Player targetEntity;
    [SerializeField]int attackDamage=20;
    [SerializeField]GameObject hpbox;
    float damage = 1;
    public Transform target;
    public float rotateSpeed = 0.02f;
    [SerializeField]float speed=3f;
    private bool playerOnRange;
    NavMeshAgent pathfinder;
    float myCollisionRadius;
    float nextAttackTime;
    float targetCollisionRadius;
    float attackDistanceTreshold=0.5f;
    bool hasTarget;
    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    float cooldown;
    public LayerMask whatisPlayer, whatIsGround;
    //State
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    Vector3 firstStation;
    public bool nonSpawnEnemy;

    //UI
    HealthBarEnemy healthBar;
    [SerializeField]float currentHealth;

    [Header("Respawn Point")]
    [SerializeField]Spawner spawns;

    void Awake(){
        pathfinder =GetComponent<NavMeshAgent>();
        healthBar = GetComponentInChildren<HealthBarEnemy>();
        if(GameObject.FindWithTag("Player")!=null){
            playerOnRange=false;
            target=GameObject.FindWithTag("Player").transform;
            targetEntity=target.GetComponent<Player>();
            myCollisionRadius=GetComponent<CapsuleCollider>().radius;
            targetCollisionRadius=target.GetComponent<CapsuleCollider>().radius;
            sightRange = GetComponent<SphereCollider>().radius;
        }
        if(nonSpawnEnemy){
            firstStation = transform.position;
        }

    }

    protected override void Start()
    {
        base.Start();
        targetEntity.OnDeath += OnTargetDeath;
        healthBar.updateHealth(health,startingHealth);
        StartCoroutine(detectPlayer());
        //Gak jalan karna pas pertamakali jalan playeronrange nya sudha false jadi skip coroutine

    }

    void OnTargetDeath(){
        playerOnRange = false;
        currentState=State.Idle;
        StopAllCoroutines();
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatisPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatisPlayer);
    }

    IEnumerator detectPlayer(){
       if(nonSpawnEnemy){
            if(!playerInSightRange&& !playerInAttackRange){}
            else if(playerInSightRange&& !playerInAttackRange){    //Kalo Player Terdeteksi
                
                if(spawns.enabled==false){
                    spawns.enabled=true;
                }
                            
                ChasePlayer();
            }//Musuh gak mau nyerang karna pake coroutine
            else if(playerInSightRange&& playerInAttackRange){
                AttackPlayer();
            }else{

            }
       }else{
            if(playerInAttackRange){
                AttackPlayer();
            }else{
                ChasePlayer();
            }
       }
       yield return new WaitForSeconds(3f); 
    }

void Patrolling(){
        if(!walkPointSet) searchWalkPoint();

        if(walkPointSet)
        {pathfinder.SetDestination(walkPoint);}

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if(distanceToWalkPoint.magnitude<1f){
            walkPointSet = false;
        }
        

    }

    private void searchWalkPoint(){
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x+randomX, transform.position.y,transform.position.z+randomZ);

        if(Physics.Raycast(walkPoint, -transform.up,2f,whatIsGround)){
            walkPointSet = true;
        }
    }

    public override void TakeDamage(float damage)
    {
        Debug.Log("Health: "+health);
        base.TakeDamage(damage);
        healthBar.updateHealth(this.health,startingHealth);
    }

    void ChasePlayer(){
        hpbox.SetActive(true);
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 targetPosition = target.position-dirToTarget* targetCollisionRadius ;
        if(!dead&&pathfinder.enabled)
        {
            pathfinder.SetDestination(target.position);
        }
    }

    void AttackPlayer(){
        
        var direction = target.position - transform.position;
            direction.y=0;
        pathfinder.SetDestination(transform.position);
        transform.forward = direction;
        if(!alreadyAttacked){
            targetEntity.TakeDamage(attackDamage);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack(){
        alreadyAttacked = false;
    }

}
