using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public Transform handPoint;
    public PlayerStats stats;

    [Header("Attack Prefabs")]
    public GameObject normalAttack;
    public GameObject freezeAttack;
    public GameObject bombAttack;
    public GameObject fireball;
    public GameObject laserBeam;

    private int currentAttack = 1;
    private ProjectileBase preparedProjectile;
    private Vector2 shootDirection;

    private Dictionary<int, float> cooldowns = new Dictionary<int, float>();
    private Dictionary<int, float> lastUse = new Dictionary<int, float>();

    void Start()
    {
        cooldowns[1] = 1f;   // Normal
        cooldowns[2] = 5f;   // Freeze
        cooldowns[3] = 5f;   // Bomb
        cooldowns[4] = 5f;   // Fireball
        cooldowns[5] = 15f;  // Laser

        for (int i = 1; i <= 5; i++)
            lastUse[i] = -999f;
    }

    void Update()
    {
        // Cambio de ataque
        if (Input.GetKeyDown(KeyCode.Alpha1)) PrepareAttack(1);
        if (Input.GetKeyDown(KeyCode.Alpha2)) PrepareAttack(2);
        if (Input.GetKeyDown(KeyCode.Alpha3)) PrepareAttack(3);
        if (Input.GetKeyDown(KeyCode.Alpha4)) PrepareAttack(4);
        if (Input.GetKeyDown(KeyCode.Alpha5)) PrepareAttack(5); // Laser

        // Lanzar ataque
        if (Input.GetMouseButtonDown(0))
            ReleasePreparedAttack();

        // Escudo con click derecho
        if (Input.GetMouseButtonDown(1))
            StartCoroutine(ShieldCoroutine());
    }

    void PrepareAttack(int id)
    {
        currentAttack = id;

        // Bomba y láser NO se preparan en la mano
        if (id == 3 || id == 5)
        {
            if (preparedProjectile != null)
                Destroy(preparedProjectile.gameObject);

            preparedProjectile = null;
            return;
        }

        // Preparar proyectil normal
        GameObject prefab = GetProjectilePrefab();
        if (prefab == null) return;

        if (preparedProjectile != null)
            Destroy(preparedProjectile.gameObject);

        GameObject obj = Instantiate(prefab);
        preparedProjectile = obj.GetComponent<ProjectileBase>();
        preparedProjectile.Prepare(handPoint, stats);

        animator.SetTrigger("Attack");
    }

    void ReleasePreparedAttack()
    {
        if (Time.time < lastUse[currentAttack] + cooldowns[currentAttack])
            return;

        lastUse[currentAttack] = Time.time;

        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mousePos - handPoint.position;
        shootDirection = dir.normalized;


        // Láser
        if (currentAttack == 5)
        {
            GameObject laser = Instantiate(laserBeam, handPoint.position, Quaternion.identity);
            laser.GetComponent<LaserBeam>().Init(shootDirection);
            return;
        }

        // Bomba
        if (currentAttack == 3)
        {
            GameObject bomb = Instantiate(bombAttack, handPoint.position, Quaternion.identity);
            bomb.GetComponent<ProjectileBase>().Launch(shootDirection);
            return;
        }

        // Proyectiles normales
        if (preparedProjectile == null) return;

        preparedProjectile.Launch(shootDirection);
        preparedProjectile = null;
    }

    GameObject GetProjectilePrefab()
    {
        switch (currentAttack)
        {
            case 1: return normalAttack;
            case 2: return freezeAttack;
            case 4: return fireball;
            default: return null;
        }
    }

    IEnumerator ShieldCoroutine()
    {
        stats.invulnerable = true;
        yield return new WaitForSeconds(5f);
        stats.invulnerable = false;
    }
}
