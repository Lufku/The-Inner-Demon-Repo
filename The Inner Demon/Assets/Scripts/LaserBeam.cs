using UnityEngine;
using System.Collections;

public class LaserBeam : MonoBehaviour
{
    public float damagePerSecond = 5f;
    private Vector2 direction;   // ← FALTABA ESTO

    public void Init(Vector2 dir)
    {
        direction = dir.normalized;   // ← Ahora sí existe
        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        float time = 0f;

        while (time < 3f)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);

            if (hit.collider)
            {
                Wall w = hit.collider.GetComponent<Wall>();
                if (w) w.TakeDamage((int)damagePerSecond);
            }

            time += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
