using UnityEngine;

public class FireballDemon : MonoBehaviour
{
    public int damage = 5;
    private Vector2 direction;
    private float speed;

    public void Launch(Vector2 dir, float spd)
    {
        direction = dir;
        speed = spd;
    }

    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Wall w = col.GetComponent<Wall>();
        if (w)
        {
            w.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        Enemy e = col.GetComponent<Enemy>();
        if (e)
        {
            e.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        PlayerStats p = col.GetComponent<PlayerStats>();
        if (p)
        {
            p.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
