using UnityEngine;

public static class SpawnService
{
    public static int InstantiateCount;
    public static int DestroyCount;

    public static GameObject Instantiate(GameObject prefab)
    {
        InstantiateCount++;
        return Object.Instantiate(prefab);
    }

    public static GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        InstantiateCount++;
        return Object.Instantiate(prefab, position, rotation);
    }

    public static T Instantiate<T>(T original) where T : Object
    {
        InstantiateCount++;
        return Object.Instantiate(original);
    }

    public static void Destroy(Object obj)
    {
        DestroyCount++;
        Object.Destroy(obj);
    }
    public static void Destroy(Object obj, float delay)
    {
        DestroyCount++;
        Object.Destroy(obj, delay);
    }

    public static void ResetCounters()
    {
        InstantiateCount = 0;
        DestroyCount = 0;
    }
}