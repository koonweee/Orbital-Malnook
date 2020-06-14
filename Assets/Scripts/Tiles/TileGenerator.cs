using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerator : MonoBehaviour
{
    public int height, width, numOfTileTypes, numOfTilesInType, numOfObstacles;
    public Tilemap[] tilemaps;
    public Tile[,] tiles;
    public float[] percents;
    private int[,] grid;

    void Start()
    {
        grid = new int[height, width];
        tiles = new Tile[numOfTileTypes, numOfTilesInType];
        InitGrid();
        PlantSeeds();
        DrawGrid();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Start();
        }
    }

    void InitGrid()
    {
        foreach (Tilemap tm in tilemaps)
        {
            tm.ClearAllTiles();
        }

        for (int type = 0; type < numOfTileTypes; ++type)
        {
            for (int tile = 0; tile < numOfTilesInType; ++tile)
            {
                Tile t = Resources.Load<Tile>(type + "/" + tile);
                tiles[type, tile] = t;
            }
        }

        for (int r = 0; r < height; ++r)
        {
            for (int c = 0; c < width; ++c)
            {
                // 4 is filler tile.
                tilemaps[0].SetTile(new Vector3Int(c, r, 0), tiles[0, 4]);
            }
        }
    }

    void PlantSeeds()
    {
        for (int r = 0; r < height; ++r)
        {
            for (int c = 0; c < width; ++c)
            {
                float roll = Random.Range(0f, 1f);
                for (int i = 0; i < percents.Length; ++i)
                {
                    if (roll <= percents[i])
                    {
                        grid[r, c] = i;
                        break;
                    }
                }
            }
        }
    }

    void DrawGrid()
    {
        for (int r = 0; r < height; ++r)
        {
            for (int c = 0; c < width; ++c)
            {
                int type = grid[r, c];
                if (type == 0) continue;
                Tilemap currentTM = tilemaps[type];
                currentTM.SetTile(new Vector3Int(c, r, 0), tiles[type, ChooseTile(r, c)]);
            }
        }
    }

    bool SomethingHere(int r, int c)
    {
        if (r < 0 || r >= height || c < 0 || c >= width) return false;
        if (grid[r, c] == 0 || grid[r, c] == 6) return false;
        return true;
    }

    int ChooseTile(int r, int c)
    {
        // 6 is obstacle. Choose random.
        if (grid[r, c] == 6) return Random.Range(0, numOfObstacles);

        bool up = SomethingHere(r + 1, c);
        bool down = SomethingHere(r - 1, c);
        bool left = SomethingHere(r, c - 1);
        bool right = SomethingHere(r, c + 1);

        if (up && down && left && right) return 4;
        if (up && down && left && !right) return 4;
        if (up && down && !left && right) return 4;
        if (up && down && !left && !right) return 4;
        if (up && !down && left && right) return 5;
        if (up && !down && left && !right) return 3;
        if (up && !down && !left && right) return 2;
        if (up && !down && !left && !right) return 6;
        if (!up && down && left && right) return 4;
        if (!up && down && left && !right) return 10;
        if (!up && down && !left && right) return 9;
        if (!up && down && !left && !right) return 1;
        if (!up && !down && left && right) return 5;
        if (!up && !down && left && !right) return 8;
        if (!up && !down && !left && right) return 7;
        if (!up && !down && !left && !right) return 0;
        return -1;
    }
}
