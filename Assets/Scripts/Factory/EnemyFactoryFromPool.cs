using UnityEngine;

public class EnemyFactoryFromPool : IEnemyFactory
{
    private readonly EnemyPool pool;

    public EnemyFactoryFromPool(EnemyPool pool)
    {
        this.pool = pool;
    }

    public Enemy Create(Vector3 position, Transform parent = null)
    {
        var enemy = pool.Get(position);

        if (parent != null)
            enemy.transform.SetParent(parent);

        return enemy;
    }
}