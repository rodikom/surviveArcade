using UnityEngine;

public class EnemyFactory : IEnemyFactory
{
    private readonly GameObject[] prefabs;

    public EnemyFactory(GameObject[] prefabs)
    {
        this.prefabs = prefabs;
    }

    public Enemy Create(Vector3 position, Transform parent = null)
    {
        int index = Random.Range(0, prefabs.Length);
        var go = SpawnService.Instantiate(prefabs[index], position, Quaternion.identity);

        if (parent != null)
            go.transform.SetParent(parent);

        return go.GetComponent<Enemy>();
    }
}