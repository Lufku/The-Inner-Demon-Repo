using UnityEngine;
using System.Collections;

public class Fireball : ProjectileBase
{
    void OnTriggerEnter2D(Collider2D col)
    {
        Wall w = col.GetComponent<Wall>();
        if (w)
        {
            StartCoroutine(Burn(w));
            Destroy(gameObject);
        }
    }

    IEnumerator Burn(Wall w)
    {
        w.TakeDamage(5);
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1);
            w.TakeDamage(3);
        }
    }
}
