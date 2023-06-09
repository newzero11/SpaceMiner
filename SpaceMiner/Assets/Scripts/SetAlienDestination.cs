using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetAlienDestination : MonoBehaviour
{
    [SerializeField]
    private GameObject rocks;
    private int num_of_rocks;
    private List<Transform> rock_list = new List<Transform>();
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
        //player = GameObject.FindWithTag("Player");
        num_of_rocks = rocks.transform.childCount;
        for (int i = 0; i < num_of_rocks; i++) {
            rock_list.Add(rocks.transform.GetChild(i));
        }
        targetNum = Random.Range(0, num_of_rocks);
        //Debug.Log(targetNum);
        target = rock_list[targetNum];
        agent.SetDestination(target.position);
        animator.SetBool("isWalking", true);
    }

    void Update()
    {
        
        if (agent.isActiveAndEnabled && checkReachedDestination()) {
            isArrival = true;
        }

        if (GetComponent<ManageAlienHealth>().checkAlienIsDead() != true) {
            if (isArrival && !AttackMode) {
                isArrival = false;
                agent.ResetPath();
                agent.isStopped = true;
                animator.SetBool("isWalking", false);
                StartCoroutine(steerToOther());
            }

            if (AttackMode) {
                agent.SetDestination(player.transform.position);
                agent.stoppingDistance = 3f;

                //check player and alien distance? or on the planet?
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
        else {
            agent.isStopped = true;
        }       
    }

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

    private void setTarget() {
        int currentTargetNum = targetNum;
        float mineral_distance;
        if (rock_list[currentTargetNum] == null) {
            currentTargetNum= Random.Range(0, rock_list.Count);
        }
        if (targetNum >= rock_list.Count) {
            targetNum = rock_list.Count - 1;
        }
        mineral_distance = Vector3.Distance(rock_list[targetNum].transform.position,
        rock_list[currentTargetNum].transform.position);
        while (mineral_distance < 1f) {
            targetNum = Random.Range(0, rock_list.Count);
            if (rock_list[targetNum] != null && rock_list[currentTargetNum] != null) {
                mineral_distance = Vector3.Distance(rock_list[targetNum].transform.position,
            rock_list[currentTargetNum].transform.position);
            }
            else {
                mineral_distance = 0;
            }
            
        }
    }

    private IEnumerator steerToOther() {

        setTarget();
        yield return new WaitForSeconds(2f);
        
        //Debug.Log(targetNum);
        target = rock_list[targetNum];
        agent.SetDestination(target.position);
        agent.isStopped = false;
        animator.SetBool("isWalking", true);
    }

    void removeRockFromList(Transform rock) {
        if (rock_list.Contains(rock)) {
            Debug.Log(rock + " removed");
            rock_list.Remove(rock);
            num_of_rocks = rock_list.Count;
        }
    }
}
