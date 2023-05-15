using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform bulletSpawn;
    [SerializeField] protected Bullet bulletPrefab;

    [SerializeField] protected float reloadTime;
    protected bool isReloaded = true;
    public abstract void Shoot(Vector3 target);
    public bool IsReloaded => isReloaded;
}
