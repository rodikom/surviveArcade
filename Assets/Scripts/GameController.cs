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

    private float bossSpawnInterval = 0.3f; // Інтервал спауну під час "бос-хвилі"
    private bool isBossWaveActive = false; // Флаг, що вказує на активність "бос-хвилі"
    private float bossWaveDuration = 30f; // Тривалість "бос-хвилі" в секундах
    private float bossWaveTimer = 0f; // Таймер для відстеження часу "бос-хвилі"
    private float spawnBossTimer = 180f; // Таймер для відстеження часу до початку "бос-хвилі"

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        InvokeRepeating("SpawnEnemy", spawnInterval, spawnInterval);
    }

    private void Update()
    {
        spawnBossTimer -= Time.deltaTime;

        if (isBossWaveActive)
        {
            // Зменшуємо таймер "бос-хвилі"
            bossWaveTimer -= Time.deltaTime;

            if (bossWaveTimer <= 0)
            {
                // Завершення "бос-хвилі": повертаємо нормальний інтервал спауну
                isBossWaveActive = false;
                CancelInvoke("SpawnEnemy");
                InvokeRepeating("SpawnEnemy", spawnInterval, spawnInterval);
                spawnBossTimer = 180f;
            }
        }
        else
        {
            // Перевіряємо, чи час розпочати "бос-хвилю"
            if (spawnBossTimer <= 0)
            {
                // Активуємо "бос-хвилю": змінюємо інтервал спауну на інтервал "бос-хвилі"
                isBossWaveActive = true;
                bossWaveTimer = bossWaveDuration;
                CancelInvoke("SpawnEnemy");
                InvokeRepeating("SpawnEnemy", bossSpawnInterval, bossSpawnInterval);
            }
        }
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
