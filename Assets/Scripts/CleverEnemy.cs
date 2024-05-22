using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleverEnemy : Enemy
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (Health <= 0 && isAlive)
        {
            isAlive = false;
            UIController.killedEnemyCount++;
            Destroy(gameObject, 30);
        }

        if (speed > 0 && isAlive)
        {
            if (target != null)
            {
                Vector2 direction = (target.position - transform.position).normalized;
                Vector2 newVelocity = direction * speed;
                rb.velocity = newVelocity;

                FlipSprite(target.position - transform.position);
            }
        }
        ChangeAnimationState(RUN_ANIMATION);
    }
}
