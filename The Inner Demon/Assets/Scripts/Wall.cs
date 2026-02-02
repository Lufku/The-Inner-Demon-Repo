using UnityEngine;

public class Wall : MonoBehaviour
{
    public int life;
    public float speed = 3f;

    void Update()
    {
        // Movimiento de derecha a izquierda
        transform.Translate(Vector2.left * speed * Time.deltaTime);
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
