using UnityEngine;

public interface IEnemyFactory
{
    Enemy Create(Vector3 position, Transform parent = null);
}