using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEntity
{
    private enum State{Chasing,Idle,Attacking};
    State currentState;
    [SerializeField]LivingEntity targetEntity;
    [SerializeField]int maxHealth= 100;
    [SerializeField]int attackDamage=20;
    private int currentHealth;
    float damage = 1;
    public Transform target;
    public float rotateSpeed = 0.02f;
    [SerializeField]float speed=3f;
    private bool playerOnRange ;
    NavMeshAgent pathfinder;
    float myCollisionRadius;
    float nextAttackTime;
    float timeBetweenAttacks=1;
    float targetCollisionRadius;
    float attackDistanceTreshold=1.5f;
    bool hasTarget;
    void Awake(){
        pathfinder =GetComponent<NavMeshAgent>();
        if(GameObject.FindWithTag("Player")!=null){
            currentState=State.Idle;
            playerOnRange=false;
            target=GameObject.FindWithTag("Player").transform;
            targetEntity=target.GetComponent<LivingEntity>();
            myCollisionRadius=GetComponent<CapsuleCollider>().radius;
            targetCollisionRadius=target.GetComponent<CapsuleCollider>().radius;
        }
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        targetEntity.OnDeath += OnTargetDeath;

        //Gak jalan karna pas pertamakali jalan playeronrange nya sudha false jadi skip coroutine

    }

    // Update is called once per frame
    void Update(){
        print(currentState);
        if(target!=null)
        {
            if(playerOnRange){
                currentState=State.Chasing;
                if(Time.time > nextAttackTime){
                    float sqrDstToTarget = (target.position - transform.position).sqrMagnitude;
                    if(sqrDstToTarget<Mathf.Pow(attackDistanceTreshold+myCollisionRadius+targetCollisionRadius,2)){
                        nextAttackTime = Time.time + timeBetweenAttacks;
                        StopCoroutine(UpdatePath());
                        StartCoroutine(Attack());
                    }else if(currentState==State.Chasing){
                        StopCoroutine(Attack());
                        StartCoroutine(UpdatePath());
                        
                    }
                }

            }else{
                currentState=State.Idle;
            }
        }
        print("playeronrange"+playerOnRange);
    }

    void OnTargetDeath(){
        playerOnRange = false;
        currentState=State.Idle;
        StopAllCoroutines();
    }
    
    IEnumerator UpdatePath(){
        float refreshRate = 1.5f;      
        while(playerOnRange)
        {   
            if(currentState==State.Chasing&&target!=null){
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                Vector3 targetPosition = target.position-dirToTarget*(myCollisionRadius+ targetCollisionRadius+ attackDistanceTreshold/2) ;
                if(!dead){
                    pathfinder.SetDestination(targetPosition);
                    print("Lagi jalan");
                }
            }
            yield return new WaitForSeconds(refreshRate);   
        } 
    }

    IEnumerator Attack(){
        currentState = State.Attacking;
        pathfinder.enabled=false;
        RotateTowardsTarget();
        Vector3 originalPosition = transform.position;
        Vector3 dirToTarget=(target.position-transform.position).normalized;
        Vector3 attackPosition = target.position - dirToTarget * myCollisionRadius;
        attackPosition.y=-0.5f;
        float attackSpeed = 3;
        float percent = 0;
        bool hasAppliedDamage = false;
        while(percent<=1){
            if(percent>=.5f && !hasAppliedDamage){
                hasAppliedDamage=true;
                targetEntity.TakeDamage(damage);
            }
            percent += Time.deltaTime *attackSpeed;
            float interpolation = (-Mathf.Pow(percent,2)+percent)*4;
            transform.position=Vector3.Lerp(originalPosition,attackPosition,interpolation);
            pathfinder.enabled=true;
            yield return null;
        }
        currentState=State.Chasing;
        pathfinder.enabled=true;
    }

    private void FixedUpdate(){

    }

    private void OnTriggerStay(Collider coll){
        if(coll.tag=="Player"){
            playerOnRange=true;
        }
    }

    private void OnTriggerExit(Collider coll){
        if(coll.tag=="Player"){
            playerOnRange=false;
        }
    }

    private void RotateTowardsTarget(){
        target = GameObject.FindWithTag("Player").transform;
        Vector3 targetDirection = target.position - transform.position;
        Quaternion look = Quaternion.LookRotation(targetDirection);
        transform.rotation=Quaternion.Slerp(transform.rotation,look,rotateSpeed);

        /*float angle = MathF.Atan2(targetDirection.y,targetDirection.x)*Mathf.Rad2Deg;
        Quaternion q = Quaternion.LookRotation(new Vector3(0,0, angle));
        transform.rotation = Quaternion.Slerp(transform.localRotation,q,rotateSpeed);*/
    }

    private void GetTarget(){
        //target = GameObject.FindGameObjectWithTag("Player").transform;

    }


}
