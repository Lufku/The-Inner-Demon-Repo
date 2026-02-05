using UnityEngine;

public class NormalAttack : ProjectileBase
{
    void OnTriggerEnter2D(Collider2D col)
    {
        Wall w = col.GetComponent<Wall>();
        if (w)
        {
            w.TakeDamage(stats.strength);
            Destroy(gameObject);
            return;
        }

        Enemy e = col.GetComponent<Enemy>();
        if (e)
        {
            e.TakeDamage(stats.strength);
            Destroy(gameObject);
        }
    }
}
