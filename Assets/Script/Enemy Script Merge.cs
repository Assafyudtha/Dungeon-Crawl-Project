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
    [SerializeField]GameObject hpJamu;
    [SerializeField]GameObject staminaJamu;

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
    Vector3 dirToTarget;

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
    Animator anims;
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
        anims=GetComponent<Animator>();
        if(targetEntity!=null){
        targetEntity.OnDeath += OnTargetDeath;}
        healthBar.updateHealth(health,startingHealth);
        //if(!nonSpawnEnemy){
        StartCoroutine(detectPlayer());
        //}
        //Gak jalan karna pas pertamakali jalan playeronrange nya sudha false jadi skip coroutine

    }

    void OnTargetDeath(){
        playerOnRange = false;
        currentState=State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatisPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatisPlayer);
    }

    IEnumerator detectPlayer(){
        while(target!=null)
        {
            if(nonSpawnEnemy){
                    if(!playerInSightRange&& !playerInAttackRange){
                        while(!playerInSightRange){
                            anims.SetBool("Chasing",false);
                            print(playerInSightRange);
                            yield return new WaitUntil(()=>playerInSightRange);
                        }
                    }
                    else if(playerInSightRange&& !playerInAttackRange){    //Kalo Player Terdeteksi
                        
                        if(spawns.enabled==false){
                            spawns.enabled=true;
                        }
                                    
                        ChasePlayer();
                        yield return new WaitForSeconds(0.1f);
                    }//Musuh gak mau nyerang karna pake coroutine
                    else if(playerInSightRange&& playerInAttackRange){
                        AttackPlayer();
                    }

            }else{
                    if(playerInAttackRange){
                        AttackPlayer();
                    }else{
                        ChasePlayer();
                    }
            }
            yield return new WaitForSeconds(1f); 
        }
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
        healthBar.updateHealth(health,startingHealth);
    }

    void ChasePlayer(){
        hpbox.SetActive(true);
        anims.SetBool("Chasing",true);     
        if(target != null){
            dirToTarget = (target.position - transform.position).normalized;
        }
        Vector3 targetPosition = target.position-dirToTarget* (targetCollisionRadius-attackRange) ;
        if(!dead&&pathfinder.enabled)
        {
            pathfinder.SetDestination(targetPosition);
        }
    }

    void AttackPlayer(){
        anims.SetBool("Chasing",false);
        var direction = target.position - transform.position;
            direction.y=0;
        pathfinder.SetDestination(transform.position);
        transform.forward = direction;
        if(!alreadyAttacked){
            anims.Play("Attack");
            targetEntity.TakeDamage(attackDamage);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack(){
        alreadyAttacked = false;
    }

    public override void Die()
    {
        powerUp(hpJamu,staminaJamu);
        base.Die();
    }

    public GameObject powerUp( GameObject heal,GameObject stamina){
        float randomPowerup = Random.Range (0,1f);
        Vector3 Above=new Vector3(0,1,0);
        if(randomPowerup<=0.7f){
            return null;
        }else if (randomPowerup>0.7f&&randomPowerup<=0.9f){
           return Instantiate(heal, transform.position+Above, Quaternion.identity);
        }else{
           return Instantiate(stamina, transform.position+Above, Quaternion.identity);
        }

    }

}
