using UnityEngine;
public class BulletPool : MonoBehaviour
{
    private Pool<Bullet> pool;
    [SerializeField] private int poolCount;
    [SerializeField] private Bullet prefab;

    public static BulletPool instance;
    private void Awake()
    {
        pool = new(prefab, poolCount, transform);
        if (instance == null)
            instance = this;
        else
            Debug.LogError("Multiple singleton");
    }
    public Bullet CreateBullet()
    {
        return pool.GetElement();
    }
}
