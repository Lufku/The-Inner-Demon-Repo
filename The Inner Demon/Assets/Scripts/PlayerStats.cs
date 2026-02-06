using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 20;
    private int currentHealth;

    public int strength = 5; // ← AÑADIDO

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
            healthBar.SetValue(currentHealth, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (healthBar != null)
            healthBar.SetValue(currentHealth, maxHealth);

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("PLAYER DEAD");
    }
}
