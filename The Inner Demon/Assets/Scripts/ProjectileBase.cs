using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    protected Vector2 direction;
    protected PlayerStats stats;
    protected bool launched = false;

    public float speed = 8f;

    public virtual void Prepare(Transform hand, PlayerStats playerStats)
    {
        stats = playerStats;
        transform.SetParent(hand);
        transform.localPosition = Vector3.zero;
        launched = false;
    }

    public virtual void Launch(Vector2 dir)
    {
        transform.SetParent(null);
        direction = dir.normalized;
        launched = true;
    }

    protected virtual void Update()
    {
        if (!launched) return;

        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
}
