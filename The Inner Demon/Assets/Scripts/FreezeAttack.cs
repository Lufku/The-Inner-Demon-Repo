using UnityEngine;
using System.Collections;

public class FreezeAttack : ProjectileBase
{
    void OnTriggerEnter2D(Collider2D col)
    {
        Wall w = col.GetComponent<Wall>();
        if (w)
        {
            w.TakeDamage(5);
            StartCoroutine(Freeze(w));
            Destroy(gameObject);
        }
    }

    IEnumerator Freeze(Wall w)
    {
        float originalSpeed = w.speed;
        w.speed *= 0.5f;
        yield return new WaitForSeconds(3);
        w.speed = originalSpeed;
    }
}
