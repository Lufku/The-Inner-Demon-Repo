using UnityEngine;

public class BombAttack : ProjectileBase
{
    public float radius = 1.5f;
    public int damage = 10;
    public float fuseTime = 2f; // tiempo de mecha antes de explotar

    private bool exploded = false;

    void Start()
    {
        // Empieza la cuenta atrás de la mecha
        Invoke(nameof(Explode), fuseTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!launched || exploded) return;

        // Si impacta contra algo, explota inmediatamente
        Explode();
    }

    void Explode()
    {
        if (exploded) return;
        exploded = true;

        // Daño en área
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (var h in hits)
        {
            Wall w = h.GetComponent<Wall>();
            if (w)
                w.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

    // Para ver el radio en el editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
