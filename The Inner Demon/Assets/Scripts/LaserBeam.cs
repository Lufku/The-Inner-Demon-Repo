using UnityEngine;
using System.Collections;

public class LaserBeam : MonoBehaviour
{
    public float damagePerSecond = 5f;
    public float animationLength = 1f; // duración total de la animación en segundos

    private Vector2 direction;
    private float startDamageTime;
    private float endDamageTime;

    public void Init(Vector2 dir)
    {
        direction = dir.normalized;

        float frameTime = animationLength / 240f;
        startDamageTime = frameTime * 78f;
        endDamageTime = frameTime * 240f;

        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        float time = 0f;

        while (time < animationLength)
        {
            if (time >= startDamageTime && time <= endDamageTime)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
                if (hit.collider)
                {
                    Wall w = hit.collider.GetComponent<Wall>();
                    if (w)
                        w.TakeDamage((int)damagePerSecond);
                }
            }

            time += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
