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
        Invoke(nameof(TriggerExplosion), fuseTime);
    }

    void TriggerExplosion()
    {
        if (exploded) return;
        exploded = true;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (var h in hits)
        {
            Wall w = h.GetComponent<Wall>();
            if (w) w.TakeDamage(damage);

            Enemy e = h.GetComponent<Enemy>();
            if (e) e.TakeDamage(damage);
        }

        animator.SetTrigger("Explode");
    }

    public void DestroyBomb()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!exploded)
            TriggerExplosion();
    }
}
