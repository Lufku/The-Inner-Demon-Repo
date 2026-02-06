using UnityEngine;
using UnityEngine.SceneManagement;

public class LaserBeam : MonoBehaviour
{
    [Header("Damage")]
    public float damageInterval = 0.1f;
    public int damage = 1;

    [Header("Raycast")]
    public LayerMask hitMask;
    public float laserWorldLength = 6f;

    [Header("Collision Object")]
    public GameObject collisionPrefab;

    private float timer = 0f;
    private bool active = false;
    private bool allowLaser = false; // ← Solo BossFight

    private Animator animator;
    private SpriteRenderer sr;

    private GameObject collisionObj;
    private BoxCollider2D col;

    void Awake()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        // SOLO FUNCIONA EN BOSSFIGHT
        allowLaser = SceneManager.GetActiveScene().name == "BossFight";
    }

    public void Init(Vector2 dir, PlayerStats stats)
    {
        if (!allowLaser) return;

        damage = stats.strength;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Update()
    {
        if (!allowLaser) return;
        if (!active) return;

        timer += Time.deltaTime;
        if (timer >= damageInterval)
        {
            timer = 0f;
            DoDamage();
        }
    }

    void DoDamage()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, laserWorldLength, hitMask);

        if (hit.collider != null)
        {
            Wall w = hit.collider.GetComponent<Wall>();
            if (w != null) w.TakeDamage(damage);

            Enemy e = hit.collider.GetComponent<Enemy>();
            if (e != null) e.TakeDamage(damage);
        }
    }

    public void ActivateLaser()
    {
        if (!allowLaser) return;

        active = true;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, laserWorldLength, hitMask);
        float distance = hit.collider ? hit.distance : laserWorldLength;

        float spriteWidth = sr.sprite.bounds.size.x;
        float scaleX = distance / spriteWidth;

        transform.localScale = new Vector3(scaleX, 1, 1);

        collisionObj = Instantiate(collisionPrefab);
        collisionObj.transform.position = transform.position;
        collisionObj.transform.rotation = transform.rotation;

        col = collisionObj.GetComponent<BoxCollider2D>();
        col.size = new Vector2(distance, 1);
        col.offset = new Vector2(distance * 0.5f, 0);
    }

    public void DestroyLaser()
    {
        if (collisionObj != null)
            Destroy(collisionObj);

        Destroy(gameObject);
    }
}
