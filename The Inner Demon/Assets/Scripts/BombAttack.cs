using UnityEngine;

public class BombAttack : ProjectileBase
{
    public float radius = 1.5f;
    public int damage = 10;
    public float fuseTime = 2f;

    private bool exploded = false;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        // A los 2 segundos se activa la explosión
        Invoke(nameof(TriggerExplosion), fuseTime);
    }

    void TriggerExplosion()
    {
        if (exploded) return;
        exploded = true;

        // Hacer daño inmediatamente
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (var h in hits)
        {
            Wall w = h.GetComponent<Wall>();
            if (w)
                w.TakeDamage(damage);
        }

        // Activar animación de explosión
        animator.SetTrigger("Explode");

        // IMPORTANTE: NO destruir aquí
        // La animación llamará a DestroyBomb() al terminar
    }

    // Este método lo llamará un Animation Event al final de la animación
    public void DestroyBomb()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Si choca antes de explotar, explota igual
        if (!exploded)
            TriggerExplosion();
    }
}
