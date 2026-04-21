using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyHeadstone : Enemy
{
    [SerializeField]
    protected float range = 7f;
    [SerializeField]
    protected GameObject bonePrefab;
    private bool isThrowing = false; 
    private bool shouldThrow = false; 
    [SerializeField]
    private Transform boneStartPos;

    protected override void Update()
    {
        base.Update();

        if (isAlive && target != null && Vector2.Distance(transform.position, target.position) <= detectionDistance) {
            if (!isThrowing) 
            {
                isThrowing = true;
                shouldThrow = true; 
                StartCoroutine(ThrowBoneEverySecond());
            }
        } else {
            shouldThrow = false; 
            isThrowing = false; 
        }
    }

    IEnumerator ThrowBoneEverySecond()
    {
        while (shouldThrow) {
            ThrowBone();
            yield return new WaitForSeconds(1f);
        }
        isThrowing = false; 
    }

    void CalculateRotation()
    {
        Vector3 aimDirection = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        var eulernAngles = new Vector3(0, 0, angle);

        boneStartPos.eulerAngles = eulernAngles;

        float distance = (boneStartPos.position - transform.position).magnitude;

        boneStartPos.position = transform.position + aimDirection * distance;
    }

    private void ThrowBone()
    {
        CalculateRotation();

        var factory = ServiceLocator.Get<IProjectileFactory>();

        factory.Create(
            ProjectileType.Bone,
            boneStartPos.position,
            boneStartPos.rotation,
            damage,
            range,
            "Player",
            transform.parent
        );
    }
}
