using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyPrefabs;
    [SerializeField]
    private float spawnInterval = 1f;
    [SerializeField]
    private GameObject enemyContainer;


    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        InvokeRepeating("SpawnEnemy", spawnInterval, spawnInterval);
    }

    void SpawnEnemy()
    {
        Vector2[] spawnPositions = CalculateSpawnPosition();
        foreach (Vector2 spawnPosition in spawnPositions)
        {
            int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyPrefab = enemyPrefabs[randomEnemyIndex];
            
            var enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemy.transform.SetParent(enemyContainer.transform);
        }
    }

    Vector2 [] CalculateSpawnPosition()
    {
        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;
         
        float camX = mainCamera.transform.position.x;
        float camY = mainCamera.transform.position.y;

        float spawnX;
        float spawnY;
        float spawnRandX = camX + Random.Range(-cameraWidth-4f, cameraWidth+4f);
        float spawnRandY = camY + Random.Range(-cameraHeight-4f, cameraHeight+4f);

        float rand = Random.Range(3f, 6f);

        Vector2[] spawnPositions = new Vector2[4];

        spawnY = camY + cameraHeight + rand;
        spawnPositions[0] = new Vector2(spawnRandX, spawnY);

        spawnY = camY - cameraHeight - rand;
        spawnPositions[1] = new Vector2(spawnRandX, spawnY);

        spawnX = camX + cameraWidth + rand;
        spawnPositions[2] = new Vector2(spawnX, spawnRandY);

        spawnX = camX - cameraWidth - rand;
        spawnPositions[3] = new Vector2(spawnX, spawnRandY);

        return spawnPositions;
    }
}
