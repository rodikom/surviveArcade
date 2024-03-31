using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    [SerializeField]
    private TileBase[] tiles;
    [SerializeField]
    private Transform playerPos;
    [SerializeField]
    private Tilemap tilemap;

    private Grid grid;

    private int width = 400;
    private int height = 400;

    private Vector3Int startPos;

    void Start()
    {
        grid = GetComponent<Grid>();
        startPos = grid.WorldToCell(playerPos.position) - new Vector3Int(width / 2, height / 2, 0);
        RandomFillMap();
    }

    private void RandomFillMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int i = Mathf.FloorToInt(Random.Range(0f, 6f));
                tilemap.SetTile(new Vector3Int(x, y, 0) + startPos, tiles[i]);
            }
        }  
    }
}
