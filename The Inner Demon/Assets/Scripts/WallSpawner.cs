using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WallSpawner : MonoBehaviour
{
    public GameObject wallPrefab;

    public int wallTypes = 5;
    public int wallsPerType = 5;

    public int baseLife = 10;
    public int lifeIncrease = 10;

    public float spawnDelay = 0.5f;

    private int destroyedWalls = 0;   // ← CONTADOR DE PAREDES DESTRUIDAS
    public int wallsNeeded = 12;      // ← CUÁNTAS NECESITAS PARA CAMBIAR DE ESCENA

    void Start()
    {
        StartCoroutine(SpawnWalls());
    }

    IEnumerator SpawnWalls()
    {
        for (int type = 1; type <= wallTypes; type++)
        {
            int amount = (type == wallTypes) ? 1 : wallsPerType;

            for (int i = 0; i < amount; i++)
            {
                Vector2 spawnPos = transform.position;

                GameObject wall = Instantiate(wallPrefab, spawnPos, Quaternion.identity);

                Wall wallScript = wall.GetComponent<Wall>();
                wallScript.SetType(type, baseLife, lifeIncrease);

                // IMPORTANTE: asignar el spawner
                wallScript.spawner = this;

                wall.tag = "Wall" + type;

                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }

    public void WallDestroyed()
    {
        destroyedWalls++;

        if (destroyedWalls >= wallsNeeded)
        {
            SceneManager.LoadScene("PreFinalBoss");
        }
    }
}
