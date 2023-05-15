using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage;

    [SerializeField] private float speed;
    [SerializeField] TrailRenderer trail;

    public bool CanMove = true;
    public void MyStart(Vector3 start, Vector3 target)
    {
        CanMove = true;
        transform.position = start;
        transform.LookAt(target);
        trail.Clear();
        Invoke("Destroy", 8f);
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        Vector3 speedVec = transform.forward * speed * Time.deltaTime;
        Vector3 nextPos = transform.position + speedVec;
        RaycastHit hit;
        if (CanMove && Physics.Linecast(transform.position, nextPos, out hit))
        {
            transform.position = hit.point;
            EnemyRig rig;
            if (hit.collider.TryGetComponent(out rig))
            {
                rig.Hit(damage);
            }
            CanMove = false;
            Invoke("Destroy", 2f);
        }
        if (CanMove)
            transform.position = nextPos;
    }
    private void Destroy()
    {
        gameObject.SetActive(false);
    }
}