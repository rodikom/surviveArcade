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
    private bool isThrowing = false; // Прапорець для перевірки стану корутини
    [SerializeField]
    private Transform boneStartPos;

    protected override void Update()
    {
        base.Update();

        if (isAlive && target != null && Vector2.Distance(transform.position, target.position) <= detectionDistance) {
            if (!isThrowing) // Перевіряємо, чи корутина вже запущена
            {
                isThrowing = true; // Встановлюємо прапорець, щоб показати, що корутина запущена
                StartCoroutine(ThrowBoneEverySecond());
            }
        }
    }

    IEnumerator ThrowBoneEverySecond()
    {
        while (true) {
            // Викликаємо метод
            ThrowBone();
            // Чекаємо 5 секунд
            yield return new WaitForSeconds(0.0001f);
        }
    }

    void CalculateRotation()
    {
        Vector3 aimDirection = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        var eulernAngles = new Vector3(0, 0, angle);

        boneStartPos.eulerAngles = eulernAngles;
        
        float distance = (boneStartPos.position - transform.position).magnitude;

        // Нове положення boneStartPos на цій відстані в напрямку до гравця
        boneStartPos.position = transform.position + aimDirection * distance;
    }


    private void ThrowBone()
    {
        //var boneAngle = CalculateRotation();
        CalculateRotation();
        var shootedBone = Instantiate(bonePrefab, boneStartPos.position, boneStartPos.rotation);

        shootedBone.GetComponent<Bullet>().TargetTag = "Player";
        shootedBone.GetComponent<Bullet>().Damage = damage;
        shootedBone.GetComponent<Bullet>().LifeTime = range;
    }
}
