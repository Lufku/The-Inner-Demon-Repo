using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public Transform handPoint;
    public PlayerStats stats;

    [Header("Projectiles")]
    public GameObject normalProjectile;
    public GameObject fireballProjectile;
    public GameObject freezeProjectile;
    public GameObject bombProjectile;
    public GameObject laserBeamPrefab;

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

        animator.SetTrigger("Attack");
    }

    // Llamado desde el frame 25
    public void TriggerAttack()
    {
        Vector2 dir = GetMouseDirection();

        switch (currentAttackType)
        {
            case AttackType.Normal:
                LaunchProjectile(normalProjectile, dir);
                break;

            case AttackType.Fireball:
                LaunchProjectile(fireballProjectile, dir);
                break;

            case AttackType.Freeze:
                LaunchProjectile(freezeProjectile, dir);
                break;

            case AttackType.Bomb:
                LaunchProjectile(bombProjectile, dir);
                break;

            case AttackType.Laser:
                LaserBeam beam = Instantiate(laserBeamPrefab, handPoint.position, Quaternion.identity)
                    .GetComponent<LaserBeam>();
                beam.Init(dir);
                break;
        }
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
