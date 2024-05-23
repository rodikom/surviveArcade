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

        int radiusSquared = 20 * 20; // Квадрат радіусу кола

        for (int x = playerCell.x - 20; x <= playerCell.x + 20; x++)
        {
            for (int y = playerCell.y - 20; y <= playerCell.y + 20; y++)
            {
                Vector3Int currentCell = new Vector3Int(x, y, 0);
                int distanceSquared = (playerCell - currentCell).sqrMagnitude;

                if (distanceSquared < radiusSquared) // Перевіряємо, чи клітина знаходиться всередині кола
                {
                    if (!tilemap.HasTile(currentCell)) // Перевіряємо, чи в цій клітині вже є тайл
                    {
                        int i = Mathf.FloorToInt(Random.Range(0f, 6f));
                        tilemap.SetTile(currentCell, tiles[i]);
                    }
                }
                else // Якщо клітина знаходиться поза колом, видаляємо тайл
                {
                    tilemap.SetTile(currentCell, null);
                }
            }
        }
    }

}
