using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public Transform handPoint;
    public PlayerStats stats;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip normalSound;
    public AudioClip fireballSound;
    public AudioClip freezeSound;
    public AudioClip bombSound;
    public AudioClip laserSound;

    [Header("Projectiles")]
    public GameObject normalProjectile;
    public GameObject fireballProjectile;
    public GameObject freezeProjectile;
    public GameObject bombProjectile;
    public GameObject laserBeamPrefab;

    [Header("Cooldowns")]
    public float normalCooldown = 1f;
    public float fireballCooldown = 5f;
    public float freezeCooldown = 5f;
    public float bombCooldown = 7f;
    public float laserCooldown = 30f;

    private float normalTimer = 0f;
    private float fireballTimer = 0f;
    private float freezeTimer = 0f;
    private float bombTimer = 0f;
    private float laserTimer = 0f;

    private AttackType currentAttackType = AttackType.None;

    public enum AttackType
    {
        None,
        Normal,
        Fireball,
        Freeze,
        Bomb,
        Laser
    }

    void Update()
    {
        normalTimer += Time.deltaTime;
        fireballTimer += Time.deltaTime;
        freezeTimer += Time.deltaTime;
        bombTimer += Time.deltaTime;
        laserTimer += Time.deltaTime;

        HandleAttackSelection();
        HandleAttackExecution();
    }

    void HandleAttackSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) currentAttackType = AttackType.Normal;
        if (Input.GetKeyDown(KeyCode.Alpha2)) currentAttackType = AttackType.Fireball;
        if (Input.GetKeyDown(KeyCode.Alpha3)) currentAttackType = AttackType.Freeze;
        if (Input.GetKeyDown(KeyCode.Alpha4)) currentAttackType = AttackType.Bomb;
        if (Input.GetKeyDown(KeyCode.Alpha5)) currentAttackType = AttackType.Laser;
    }

    void HandleAttackExecution()
    {
        if (!Input.GetMouseButtonDown(0) || currentAttackType == AttackType.None)
            return;

        bool canAttack =
            (currentAttackType == AttackType.Normal && normalTimer >= normalCooldown) ||
            (currentAttackType == AttackType.Fireball && fireballTimer >= fireballCooldown) ||
            (currentAttackType == AttackType.Freeze && freezeTimer >= freezeCooldown) ||
            (currentAttackType == AttackType.Bomb && bombTimer >= bombCooldown) ||
            (currentAttackType == AttackType.Laser && laserTimer >= laserCooldown);

        if (!canAttack)
            return;

        if (currentAttackType == AttackType.Laser)
            animator.SetTrigger("LaserBeamAttack");
        else
            animator.SetTrigger("Attack");
    }

    // Llamado desde AnimationRelay
    public void TriggerAttack()
    {
        Vector2 dir = GetMouseDirection();

        PlayAttackSound(); // ← SONIDO AQUÍ

        switch (currentAttackType)
        {
            case AttackType.Normal:
                normalTimer = 0f;
                LaunchProjectile(normalProjectile, dir);
                break;

            case AttackType.Fireball:
                fireballTimer = 0f;
                LaunchProjectile(fireballProjectile, dir);
                break;

            case AttackType.Freeze:
                freezeTimer = 0f;
                LaunchProjectile(freezeProjectile, dir);
                break;

            case AttackType.Bomb:
                bombTimer = 0f;
                LaunchProjectile(bombProjectile, dir);
                break;

            case AttackType.Laser:
                laserTimer = 0f;
                GameObject obj = Instantiate(laserBeamPrefab, handPoint.position, Quaternion.identity);
                LaserBeam beam = obj.GetComponent<LaserBeam>();
                beam.Init(dir, stats);
                break;
        }
    }

    void PlayAttackSound()
    {
        if (audioSource == null)
            return;

        AudioClip clip = null;

        switch (currentAttackType)
        {
            case AttackType.Normal: clip = normalSound; break;
            case AttackType.Fireball: clip = fireballSound; break;
            case AttackType.Freeze: clip = freezeSound; break;
            case AttackType.Bomb: clip = bombSound; break;
            case AttackType.Laser: clip = laserSound; break;
        }

        if (clip != null)
            audioSource.PlayOneShot(clip);
    }

    void LaunchProjectile(GameObject prefab, Vector2 dir)
    {
        GameObject proj = Instantiate(prefab, handPoint.position, Quaternion.identity);

        ProjectileBase pb = proj.GetComponent<ProjectileBase>();
        pb.Prepare(handPoint, stats);
        pb.Launch(dir);
    }

    Vector2 GetMouseDirection()
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return (mouseWorld - handPoint.position).normalized;
    }
}
