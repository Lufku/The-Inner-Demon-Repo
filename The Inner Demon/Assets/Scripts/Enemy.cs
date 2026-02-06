using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 50;
    private int currentHealth;

    [Header("Health Bar")]
    public HealthBar healthBar;

    [Header("Boss Attack")]
    public GameObject fireballPrefab;
    public Transform handPoint;
    public float fireInterval = 1f;
    public float fireballSpeed = 6f;

    private float fireTimer = 0f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip fireballSound;   // ← NUEVO

    [Header("Movement Area")]
    public Vector2 minBounds;
    public Vector2 maxBounds;
    public float moveSpeed = 4f;
    public float waitTime = 0.6f;

    private Vector2 targetPos;
    private float moveTimer = 0f;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.SetValue(currentHealth, maxHealth);

        PickNewTargetPosition();
    }

    void Update()
    {
        HandleMovement();
        HandleAttack();
    }

    void HandleMovement()
    {
        moveTimer += Time.deltaTime;

        if (moveTimer >= waitTime)
            PickNewTargetPosition();

        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPos,
            moveSpeed * Time.deltaTime
        );
    }

    void PickNewTargetPosition()
    {
        moveTimer = 0f;

        float x = Random.Range(minBounds.x, maxBounds.x);
        float y = Random.Range(minBounds.y, maxBounds.y);

        targetPos = new Vector2(x, y);
    }

    void HandleAttack()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireInterval)
        {
            fireTimer = 0f;
            ShootFireball();
        }
    }

    void ShootFireball()
    {
        if (fireballPrefab == null || handPoint == null)
            return;

        PlayAttackSound(); // ← SONIDO AQUÍ

        GameObject obj = Instantiate(fireballPrefab, handPoint.position, Quaternion.identity);

        Vector2 dir = (PlayerPosition() - (Vector2)handPoint.position).normalized;

        FireballDemon fb = obj.GetComponent<FireballDemon>();
        fb.Launch(dir, fireballSpeed);
    }

    void PlayAttackSound()
    {
        if (audioSource != null && fireballSound != null)
            audioSource.PlayOneShot(fireballSound);
    }

    Vector2 PlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return player.transform.position;
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
        SceneManager.LoadScene("Good Ending");
        Destroy(gameObject);
    }
}
