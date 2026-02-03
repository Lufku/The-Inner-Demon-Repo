using UnityEngine;

public class Wall : MonoBehaviour
{
    public int life;
    public float speed = 3f;

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

        // Blanco → Negro
        float t = (float)(type - 1) / 4f; // 0 a 1
        sr.color = Color.Lerp(Color.white, Color.black, t);
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
