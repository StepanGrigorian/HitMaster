using UnityEngine;

public class EnemyRig : MonoBehaviour
{
    [SerializeField] private Enemy parent;
    public void Hit(float damage)
    {
        parent.TakeDamage(damage);
    }
}