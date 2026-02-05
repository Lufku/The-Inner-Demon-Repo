using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int health = 50;

    [Header("Boss Attack")]
    public GameObject fireballPrefab;   // FireballDemon
    public Transform handPoint;         // Mano del boss
    public float fireInterval = 1f;     // Dispara cada 1 segundo
    public float fireballSpeed = 6f;

    private float fireTimer = 0f;

    [Header("Movement Area")]
    public Vector2 minBounds;   // esquina inferior izquierda
    public Vector2 maxBounds;   // esquina superior derecha
    public float moveSpeed = 3f;
    public float waitTime = 2f; // tiempo entre cambios de destino

    private Vector2 targetPos;
    private float moveTimer = 0f;

    void Start()
    {
        PickNewTargetPosition();
    }

    void Update()
    {
        HandleMovement();
        HandleAttack();
    }

    // -----------------------------
    // MOVIMIENTO ALEATORIO
    // -----------------------------
    void HandleMovement()
    {
        moveTimer += Time.deltaTime;

        // Cambia de destino cada cierto tiempo, aunque no haya llegado
        if (moveTimer >= waitTime)
        {
            PickNewTargetPosition();
        }

        // Movimiento continuo hacia el objetivo
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




    // -----------------------------
    // ATAQUE
    // -----------------------------
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

        GameObject obj = Instantiate(fireballPrefab, handPoint.position, Quaternion.identity);

        Vector2 dir = (PlayerPosition() - (Vector2)handPoint.position).normalized;

        FireballDemon fb = obj.GetComponent<FireballDemon>();
        fb.Launch(dir, fireballSpeed);
    }

    Vector2 PlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return player.transform.position;
    }

    // -----------------------------
    // DAÑO Y MUERTE
    // -----------------------------
    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
