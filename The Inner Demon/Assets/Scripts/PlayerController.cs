using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movimiento horizontal
        moveInput = Input.GetAxisRaw("Horizontal");

        // Animaci�n de correr
        animator.SetFloat("Speed", Mathf.Abs(moveInput));

        // Voltear sprite
        if (moveInput != 0)
            spriteRenderer.flipX = moveInput < 0;
    }

    void FixedUpdate()
    {
        // Aplicar movimiento
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }
}
