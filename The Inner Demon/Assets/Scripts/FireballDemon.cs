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

        PlayerStats p = col.GetComponentInParent<PlayerStats>();
        if (p != null)
        {
            p.TakeDamage(damage);
            Destroy(gameObject);
        }

    }
}
