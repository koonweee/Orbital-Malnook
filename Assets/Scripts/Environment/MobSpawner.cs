using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MobSpawner : MonoBehaviour
{
    public int maxMobs;
    public TileGenerator tileGenerator;
    public Tilemap tilemap;
    public GameObject[] enemies;
    private int mobCount;

    void Start()
    {
        mobCount = 0;
    }

    void Update()
    {
        if (mobCount < maxMobs)
        {
            SpawnRandomMob();
        }
    }

    void SpawnRandomMob()
    {
        ++mobCount;

        GameObject chosenMob = enemies[Random.Range(0, enemies.Length)];

        int randX = Random.Range(0, tileGenerator.width);
        int randY = Random.Range(0, tileGenerator.height);

        Vector3 pos = tilemap.CellToWorld(new Vector3Int(randX, randY, 0));

        GameObject spawnedMob = Instantiate(chosenMob, pos, Quaternion.identity);
        spawnedMob.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
    }

    public void DecrementMobCount()
    {
        --mobCount;
    }
}
