using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    public GameObject wallPrefab;

    public int wallTypes = 5;
    public int wallsPerType = 5;

    public int baseLife = 10;
    public int lifeIncrease = 10;

    public float spawnX = 10f;
    public float spawnYMin = -3f;
    public float spawnYMax = 3f;

    void Start()
    {
        SpawnWalls();
    }

    void SpawnWalls()
    {
        for (int type = 1; type <= wallTypes; type++)
        {
            int amount = (type == wallTypes) ? 1 : wallsPerType;

            for (int i = 0; i < amount; i++)
            {
                Vector2 spawnPos = new Vector2(
                    spawnX,
                    Random.Range(spawnYMin, spawnYMax)
                );

                GameObject wall = Instantiate(wallPrefab, spawnPos, Quaternion.identity);

                Wall wallScript = wall.GetComponent<Wall>();
                wallScript.life = baseLife + (type - 1) * lifeIncrease;

                // Tag opcional
                wall.tag = "Wall" + type;
            }
        }
    }
}
