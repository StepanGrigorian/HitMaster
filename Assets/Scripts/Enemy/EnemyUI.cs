using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Canvas))]
public class EnemyUI : MonoBehaviour
{
    private float health;
    private float currentHealth;

    [SerializeField] private Slider healthbar;
    private void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
    public void SetHealth(float health)
    {
        this.health = health;
    }
    public void SetCurrentHealth(float health)
    {
        currentHealth = health;
        healthbar.value = ((float)currentHealth / this.health);
        if (currentHealth == 0)
        {
            healthbar.gameObject.SetActive(false);
        }
        else
        {
            healthbar.gameObject.SetActive(true);
        }
    }
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }
}