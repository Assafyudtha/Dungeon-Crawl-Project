
using UnityEngine;
using UnityEngine.AI;


public class EnemyScriptTest : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    
    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public LayerMask whatisPlayer, whatIsGround;
    //State
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    float cooldown;

    private void Awake(){
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update(){
        //Check Enemy in sight
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatisPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatisPlayer);

        if(!playerInSightRange&& !playerInAttackRange){Patrolling();}
        if(playerInSightRange&& !playerInAttackRange){ChasePlayer();}
        if(playerInSightRange&& playerInAttackRange){
            if(Time.time>cooldown)
            {
                cooldown = Time.time + timeBetweenAttacks;
                AttackPlayer();
            }
        }

    }

    void Patrolling(){
        if(!walkPointSet) searchWalkPoint();

        if(walkPointSet)
        {agent.SetDestination(walkPoint);}

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
 
    void ChasePlayer(){
        agent.SetDestination(player.position);
    }

    void AttackPlayer(){
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        if(!alreadyAttacked){
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack(){
        alreadyAttacked = false;
    }
}
