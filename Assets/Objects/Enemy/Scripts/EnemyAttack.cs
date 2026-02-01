using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private EnemyEyes eyes;
    
    public void DoAttack()
    {
        eyes.playerCollider.GetComponent<PlayerController>().Hurt();
    }
}
