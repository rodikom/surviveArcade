using UnityEngine;

public class EnemyFactory : IFactory<Enemy>
{
    private readonly GameObject[] prefabs;
    private readonly Transform defaultParent;

    public EnemyFactory(GameObject[] prefabs, Transform parent)
    {
        this.prefabs = prefabs;
        this.defaultParent = parent;
    }

    public Enemy Create()
    {
        int index = Random.Range(0, prefabs.Length);
        var go = Object.Instantiate(prefabs[index], defaultParent);
        return go.GetComponent<Enemy>();
    }
}