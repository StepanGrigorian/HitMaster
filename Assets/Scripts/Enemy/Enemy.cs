using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    private LevelSegment segment;
    private Animator animator;

    [SerializeField] private Rigidbody[] rigidbodies;
    [SerializeField] private EnemyUI ui;

    [SerializeField]
    private float health;
    private float currentHealth;

    private bool isDead = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        ToggleRagdoll(true);
        currentHealth = health;
        ui.SetHealth(health);
    }
    public void SetSegment(LevelSegment levelSegment)
    {
        segment = levelSegment;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Death();
        }
        ui.SetCurrentHealth(currentHealth);
    }
    private void Death()
    {
        if (!isDead)
        {
            isDead = true;
            ToggleRagdoll(false);
            segment.RemoveEnemyFromList(this);
        }
    }
    public void ToggleRagdoll(bool active)
    {
        foreach (var a in rigidbodies)
        {
            a.isKinematic = active;
        }
        animator.enabled = active;
    }
}