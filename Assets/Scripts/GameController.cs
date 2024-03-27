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
    private float spawnBuffer = 1.5f;

    void Start()
    {
        mainCamera = Camera.main;
        InvokeRepeating("SpawnEnemy", spawnInterval, spawnInterval);
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = CalculateSpawnPositionOutsideCamera();
        if (spawnPosition != Vector3.zero)
        {
            int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyPrefab = enemyPrefabs[randomEnemyIndex];
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    Vector3 CalculateSpawnPositionOutsideCamera()
    {
        Vector3 spawnPosition = Vector3.zero;

        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        float spawnX = Random.Range(-cameraWidth, cameraWidth);
        float spawnY = Random.Range(-cameraHeight, cameraHeight);

        // Checking if spawn position is inside camera bounds
        if (Mathf.Abs(spawnX) < cameraWidth - spawnBuffer && Mathf.Abs(spawnY) < cameraHeight - spawnBuffer)
        {
            float spawnXOffset = spawnX < 0 ? -cameraWidth - spawnBuffer : cameraWidth + spawnBuffer;
            float spawnYOffset = spawnY < 0 ? -cameraHeight - spawnBuffer : cameraHeight + spawnBuffer;
            spawnPosition = new Vector3(spawnXOffset, spawnYOffset, 0f);
        }

        return spawnPosition;
    }
}
