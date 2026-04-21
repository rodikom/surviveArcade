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

    void Start()
    {
        grid = GetComponent<Grid>();
        RandomFillMap();
    }

    private void Update()
    {
        RandomFillMap();
    }

    private void RandomFillMap()
    {
        Vector3Int playerCell = grid.WorldToCell(playerPos.position);

        int radiusSquared = 20 * 20; 

        for (int x = playerCell.x - 20; x <= playerCell.x + 20; x++)
        {
            for (int y = playerCell.y - 20; y <= playerCell.y + 20; y++)
            {
                Vector3Int currentCell = new Vector3Int(x, y, 0);
                int distanceSquared = (playerCell - currentCell).sqrMagnitude;

                if (distanceSquared < radiusSquared) 
                {
                    if (!tilemap.HasTile(currentCell)) 
                    {
                        int i = Mathf.FloorToInt(Random.Range(0f, 6f));
                        tilemap.SetTile(currentCell, tiles[i]);
                    }
                }
                else 
                {
                    tilemap.SetTile(currentCell, null);
                }
            }
        }
    }

}
