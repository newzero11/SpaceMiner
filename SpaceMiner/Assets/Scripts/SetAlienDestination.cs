using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetAlienDestination : MonoBehaviour
{
    [SerializeField]
    private GameObject rocks;
    private Transform target;
    private NavMeshAgent agent;
    private Animator animator;
    private int targetNum;
    private bool isArrival = false;
    public bool AttackMode = false;

    [SerializeField]
    private GameObject player;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        //The destination of the nav mesh agent is one of the rocks
        //When the alien starts heading for their destination, play a walking animation.
        targetNum = Random.Range(0, rocks.transform.childCount);
        target = rocks.transform.GetChild(targetNum);
        agent.SetDestination(target.position);
        animator.SetBool("isWalking", true);
    }

    void Update()
    {

        //Set a variable that determines whether the destination has been reached.
        if (agent.isActiveAndEnabled && checkReachedDestination()) {
            isArrival = true;
        }

        //Repeat the following situation while the alien is alive.
        if (GetComponent<ManageAlienHealth>().checkAlienIsDead() != true) {
            //When aliens arrive at the rock, which is their destination
            //while not in attack mode, they set a new rock as their destination.
            if (isArrival && !AttackMode) {
                isArrival = false;
                agent.ResetPath();
                agent.isStopped = true;
                animator.SetBool("isWalking", false);
                if (rocks.transform.childCount != 0) {
                    StartCoroutine(steerToOther());
                }                 
            }
            //If an alien detects the player and enters attack mode,
            //the destination changes to the player's position.
            if (AttackMode) {
                agent.SetDestination(player.transform.position);
                agent.stoppingDistance = 3f;

                //Check the distance between an alien and the player,
                //and change the attack mode to false when the distance increases.
                //This is to prevent aliens from attacking the player after the player leaves the planet.
                float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
                if (distanceToPlayer >= 10) {
                    isArrival = false;
                    agent.ResetPath();
                    AttackMode = false;
                    GetComponent<AttackPlayer>().AttackMode = false;
                    GetComponent<AttackPlayer>().changeToOriginalEyeMaterial();
                    agent.stoppingDistance = 1f;
                }
            }
        }
        //Aliens no longer move when they die.
        else {
            agent.isStopped = true;
        }       
    }

    //It is a function to check if aliens have arrived at their destination.
    private bool checkReachedDestination() {
        if (!agent.pathPending) {
            if (agent.remainingDistance <= agent.stoppingDistance) {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) { //reached destination                
                    return true;
                }
            }
        }
        return false;
    }

    //It is a function that determines a new destination when an alien arrives at a destination.
    private IEnumerator steerToOther() {
        yield return new WaitForSeconds(2f);

        if (rocks.transform.childCount != 0) {
            targetNum = Random.Range(0, rocks.transform.childCount);
            target = rocks.transform.GetChild(targetNum);
            agent.SetDestination(target.position);
            agent.isStopped = false;
            animator.SetBool("isWalking", true);
        }
        
    }
  
}
