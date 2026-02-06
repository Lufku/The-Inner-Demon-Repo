using UnityEngine;

public class Wall : MonoBehaviour
{
    public int life;
    public float speed = 3f;

    [HideInInspector]
    public WallSpawner spawner;

    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    public void SetType(int type, int baseLife, int lifeIncrease)
    {
        life = baseLife + (type - 1) * lifeIncrease;

        float t = (float)(type - 1) / 4f;
        sr.color = Color.Lerp(Color.white, Color.black, t);
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (spawner != null)
            spawner.WallDestroyed();

        Destroy(gameObject);
    }
}
