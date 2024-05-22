using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadstone : Enemy
{
    [SerializeField]
    protected float range = 7f;
    [SerializeField]
    protected GameObject bonePrefab;

    protected override void Update()
    {
        if (Health>0 && isAlive && target != null && Vector2.Distance(transform.position, target.position) <= detectionDistance)
        {
            InvokeRepeating("Throw", 10f, 10f);
        }
    }

    private void Throw()
    {
        var shootedBone = Instantiate(bonePrefab, transform.position, transform.rotation);

        shootedBone.GetComponent<Bone>().damage = damage;
        shootedBone.GetComponent<Bone>().LifeTime = range;
    }
}
