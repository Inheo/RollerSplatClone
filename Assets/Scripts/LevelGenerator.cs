using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator
{
    private Color colorWall = Color.white;
    private Color colorRoad = Color.black;

    private List<RoadTile> _roadTiles = new List<RoadTile>();

    public int RoadTilesCount => _roadTiles.Count;

    public void Generate(WallTile prefabWallTile, RoadTile prefabRoadTile, Texture2D levelTexture, Transform tilesParent)
    {
        float unitPerPixel = prefabWallTile.transform.lossyScale.x;
        float halfUnitPerPixel = unitPerPixel / 2f;

        float width = levelTexture.width;
        float height = levelTexture.height;

        Vector3 offset = (new Vector3(width / 2f, 0f, height / 2f) * unitPerPixel) - new Vector3(halfUnitPerPixel, 0f, halfUnitPerPixel);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color pixelColor = levelTexture.GetPixel(x, y);

                Vector3 spawnPos = ((new Vector3(x, 0f, y) * unitPerPixel) - offset);

                if (pixelColor == colorWall)
                    Spawn(prefabWallTile, spawnPos, tilesParent);
                else if (pixelColor == colorRoad)
                    _roadTiles.Add(Spawn(prefabRoadTile, spawnPos, tilesParent));
            }
        }
    }

    public RoadTile GetRoadTile(int index)
    {
        return _roadTiles[index];
    }

    private T Spawn<T>(T prefabTile, Vector3 position, Transform parent) where T : MonoBehaviour
    {
        position.y = prefabTile.transform.position.y;

        T obj = GameObject.Instantiate(prefabTile, position, Quaternion.identity, parent);
        return obj;
    }
}