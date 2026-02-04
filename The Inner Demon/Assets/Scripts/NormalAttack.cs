using UnityEngine;

public class NormalAttack : ProjectileBase
{
    void OnTriggerEnter2D(Collider2D col)
    {
        Wall w = col.GetComponent<Wall>();
        if (w)
        {
            // El daño viene del PlayerStats asignado en Prepare()
            w.TakeDamage(stats.strength);
            Destroy(gameObject);
        }
    }
}
