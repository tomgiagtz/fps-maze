using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent agent;
    EnemyController enemyController;
    public Transform[] wayPoints;
    public int currentWayPoint = 0;
    public float leaveRange = 30f;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyController = GetComponent<EnemyController>();
        agent.destination = wayPoints[currentWayPoint].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.pathPending && agent.remainingDistance > leaveRange) {
            enemyController.engaged = false;
            agent.destination = wayPoints[currentWayPoint].position;
        }
    }



    public void OnEnterWayPoint() {
        if (enemyController.engaged) return;
        //dont change target if chasing
        //loop points
        currentWayPoint++;
        currentWayPoint =  currentWayPoint >= wayPoints.Length ? 0 : currentWayPoint;
        //set destination
        agent.destination = wayPoints[currentWayPoint].position;
    }

    public void OnDetect(Vector3 target) {
        enemyController.engaged = true;
        Debug.Log("target" + target.ToString());
        agent.destination = target;
    }
}
