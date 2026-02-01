using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

public class EnemyEyes : MonoBehaviour
{
    [Header("General")]
    [SerializeField, Range(0f, 360f)] public float angle;
    [SerializeField] public float maxDistance;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask obstacleLayer;
    
    [Header("Player")]
    [SerializeField] public float proximityRealization;
    [SerializeField] public Collider playerCollider;
    [HideInInspector] public bool canSeePlayer = false;

    [Header("Light")]
    [SerializeField] public Collider lightCollider;
    [HideInInspector] public bool canSeeLight = false;

    void FixedUpdate()
    {
        Collider[] nearColliders = Physics.OverlapSphere(transform.position, proximityRealization, playerLayer);
        
        if (nearColliders.Length > 0 && nearColliders.Contains(playerCollider))
        {
            Vector3 directionToTarget = (playerCollider.transform.position + Vector3.up - transform.position).normalized;

            shootPlayerRaycast(directionToTarget);
            return;
        }
        
        Collider[] furtherColliders = Physics.OverlapSphere(transform.position, maxDistance, playerLayer);

        if (furtherColliders.Length <= 0)
        {
            if (canSeePlayer || canSeeLight)
            {
                canSeePlayer = false;
                canSeeLight = false;
            }
            return;
        }

        if (furtherColliders.Contains(playerCollider))
        {
            Vector3 directionToTarget = (playerCollider.transform.position + Vector3.up - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                shootPlayerRaycast(directionToTarget);
            }
            else
                canSeePlayer = false;
        } else
            canSeePlayer = false;


        if (!canSeePlayer && furtherColliders.Contains(lightCollider))
        {
            Vector3 directionToTarget = (lightCollider.transform.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                shootLightRaycast(directionToTarget);
            }
            else
                canSeeLight = false;
        } else
            canSeeLight = false;
    }

    private void shootPlayerRaycast(Vector3 direction)
    {
        float distanceToTarget = Vector3.Distance(transform.position, playerCollider.transform.position);

        if (!Physics.Raycast(transform.position, direction, distanceToTarget, obstacleLayer))
            canSeePlayer = true;
        else
            canSeePlayer = false;
    }

    private void shootLightRaycast(Vector3 direction)
    {
        float distanceToTarget = Vector3.Distance(transform.position, lightCollider.transform.position);

        if (!Physics.Raycast(transform.position, direction, distanceToTarget, obstacleLayer))
            canSeeLight = true;
        else
            canSeeLight = false;
    }
}
