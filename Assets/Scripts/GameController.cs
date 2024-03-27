using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    protected GameObject[] enemyPrefabs;
    [SerializeField]
    protected float spawnInterval = 2f;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        InvokeRepeating("SpawnEnemy", spawnInterval, spawnInterval);
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = CalculateSpawnPosition();
        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyPrefab = enemyPrefabs[randomEnemyIndex];
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    Vector3 CalculateSpawnPosition()
    {
        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        float spawnX = Random.Range(-cameraWidth, cameraWidth);
        float spawnY = Random.Range(-cameraHeight, cameraHeight);

        // Визначаємо, який бік буде більше відстань до краю камери
        float maxSide = Mathf.Max(Mathf.Abs(spawnX), Mathf.Abs(spawnY));

        // Збільшуємо відстань на 1.5 рази, щоб вороги не з'являлись на межі камери
        float buffer = 1.5f;

        // Перевіряємо, які координати використовувати для збільшення ворогів за межами камери
        if (maxSide == Mathf.Abs(spawnX))
        {
            spawnX = Mathf.Clamp(spawnX * buffer, -cameraWidth, cameraWidth);
            spawnY = Mathf.Clamp(spawnY, -cameraHeight, cameraHeight);
        }
        else
        {
            spawnY = Mathf.Clamp(spawnY * buffer, -cameraHeight, cameraHeight);
            spawnX = Mathf.Clamp(spawnX, -cameraWidth, cameraWidth);
        }

        return new Vector3(spawnX, spawnY, 0f);
    }
}
