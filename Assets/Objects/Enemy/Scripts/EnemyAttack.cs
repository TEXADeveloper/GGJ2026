using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackArea;
    [SerializeField] private LayerMask playerLayer;
    
    public void DoAttack()
    {
        Collider[] cols = Physics.OverlapSphere(attackPoint.position, attackArea, playerLayer);

        if (cols == null || cols.Length <= 0)
            return;
        
        foreach (Collider col in cols)
        {
            PlayerController pC = col.GetComponent<PlayerController>();
            if (pC != null)
                pC.Hurt();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackArea);
    }
}
