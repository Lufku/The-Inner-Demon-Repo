using UnityEngine;
using System.Collections;

public class FreezeAttack : ProjectileBase
{
    public int baseDamage = 5;
    public float slowMultiplier = 0.5f;
    public float slowDuration = 3f;

    void OnTriggerEnter2D(Collider2D col)
    {
        Wall w = col.GetComponent<Wall>();
        if (w)
        {
            w.TakeDamage(baseDamage);
            StartCoroutine(Freeze(w));
            Destroy(gameObject);
        }
    }

    IEnumerator Freeze(Wall w)
    {
        float originalSpeed = w.speed;
        w.speed *= slowMultiplier;
        yield return new WaitForSeconds(slowDuration);
        w.speed = originalSpeed;
    }
}
