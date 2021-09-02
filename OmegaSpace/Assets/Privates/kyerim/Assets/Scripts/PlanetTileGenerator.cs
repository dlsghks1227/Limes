using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlanetTileGenerator : MonoBehaviour
{
    public int tileXScale = 10;
    public int tileZScale = 10;

    private void Awake()
    {
        GenerateTile();
    }

    private void GenerateTile()
    {
        Vector3 tileCenterPos;
        Vector3 tileScale;
        tileXScale = 10;
        tileZScale = 10;
        tileCenterPos.x = tileXScale / 2 - 0.5f;
        tileCenterPos.y = 0;
        tileCenterPos.z = tileZScale / 2 - 0.5f;
        tileScale.x = tileXScale / 10;
        tileScale.y = 1;
        tileScale.z = tileZScale / 10;
        GameObject tile = GameObject.Find("Tile");
        (Instantiate(tile, tileCenterPos, Quaternion.identity)).transform.localScale = tileScale;
    }
}