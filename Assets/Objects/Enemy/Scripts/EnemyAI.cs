using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    private Transform currentTarget;

    [SerializeField] private Transform[] patrolPoints;
    [SerializeField, Range(0f, 2f)] private float minDistanceToTarget;
    [SerializeField, Range(0f, 10f)] private float minWaitingTime;
    [SerializeField, Range(0f, 10f)] private float maxWaitingTime;
    private float waitTimer;

    private bool isPlayerLocated = false;

    void Start()
    {
        currentTarget = patrolPoints[Random.Range(0, patrolPoints.Length)];
    }

    void LateUpdate()
    {
        if (isPlayerLocated)
        {
            //Do Something
        }
        else
        {
            patrol();
        }
        //* if player is seen follow him
        //* if light is seen follow the light
    }

    private void patrol()
    {
        //*  Follow points Randomly
        if (currentTarget == null)
            return;
        if (waitTimer <= 0)
        {
            //? Set new Objective
            Transform newTarget = null;
            while (newTarget == null || newTarget == currentTarget) 
            {
                newTarget = patrolPoints[Random.Range(0, patrolPoints.Length)];
            }
            currentTarget = newTarget;

            //? Start moving
            agent.SetDestination(currentTarget.position);
            agent.isStopped = false;

            //? Set Timer
            waitTimer = Random.Range(minWaitingTime, maxWaitingTime);
        } else 
        {
            //? Stop Moving
            if (distanceToTarget(transform.position, currentTarget.position) <= minDistanceToTarget)
            {
                agent.isStopped = true;
                waitTimer -= Time.deltaTime;
            }
        }
    }

    private float distanceToTarget(Vector3 from, Vector3 to)
    {
        Vector3 horizontalFrom = from - from.y * Vector3.up;
        Vector3 horizontalTo = to - to.y * Vector3.up;

        return Vector3.Distance(horizontalFrom, horizontalTo);
    }
}
