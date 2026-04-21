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
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject bonePrefab;

    private float bossSpawnInterval = 0.3f; 
    private bool isBossWaveActive = false; 
    public bool IsBossWaveActive => isBossWaveActive;

    private float bossWaveDuration = 30f; 
    public float BossWaveDuration => bossWaveDuration; 

    private float bossWaveTimer = 0f; 
    private float spawnBossTimer = 180f; 

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        var projectilePrefabs = new Dictionary<ProjectileType, GameObject>
        {
            { ProjectileType.Bullet, bulletPrefab },
            { ProjectileType.Bone, bonePrefab }
        };
        ServiceLocator.Register<IProjectileFactory>(
            new ProjectileFactory(projectilePrefabs)
        );
        ServiceLocator.Register<IEnemyFactory>(
            new EnemyFactory(enemyPrefabs)
        );
        InvokeRepeating("SpawnEnemy", spawnInterval, spawnInterval);
    }

    private void Update()
    {
        spawnBossTimer -= Time.deltaTime;

        if (isBossWaveActive)
        {
           bossWaveTimer -= Time.deltaTime;

            if (bossWaveTimer <= 0)
            {
                isBossWaveActive = false;
                CancelInvoke("SpawnEnemy");
                InvokeRepeating("SpawnEnemy", spawnInterval, spawnInterval);
                spawnBossTimer = 180f;
            }
        }
        else
        {
            if (spawnBossTimer <= 0)
            {
                isBossWaveActive = true;
                bossWaveTimer = bossWaveDuration;
                CancelInvoke("SpawnEnemy");
                InvokeRepeating("SpawnEnemy", bossSpawnInterval, bossSpawnInterval);
            }
        }
    }

    void SpawnEnemy()
    {
        var factory = ServiceLocator.Get<IEnemyFactory>();
        Vector2[] spawnPositions = CalculateSpawnPosition();

        foreach (var pos in spawnPositions)
        {
            factory.Create(pos, enemyContainer.transform);
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
