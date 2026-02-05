using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 20;
    public int strength = 5;

    private int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("PLAYER DEAD");
        // Aquí luego pondremos animación, respawn, etc.
    }
}
