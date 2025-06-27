using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    protected float currentHealth;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0f)
            Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
