using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : DamageableCharacter
{
    // Unity components
    protected SpriteRenderer spriteRenderer;

    // Enemy stats
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float damage = 1f;

    // Find player
    [SerializeField]
    protected Transform target;
    [SerializeField]
    protected float detectionDistance;
    [SerializeField]
    protected float knockbackForce = 1f;
    [SerializeField]
    private float wanderRadius;

    protected Vector2 spawnPoint;

    private Vector2 targetPosition;
    private Vector2 wanderDirection;

    protected bool isAlive = true;

    // Animation states
    protected string RUN_ANIMATION = "RUN";

    protected override void Awake()
    {
        base.Awake();
        if(UIController.killedEnemyCount != 0)
            MaxHealth += (float)UIController.killedEnemyCount / 70;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();

        CURRENT_ANIMATION = RUN_ANIMATION;
        spawnPoint = transform.position;

        target = GameObject.FindAnyObjectByType<PlayerController>().transform;

        GetNewWanderTarget();
    }

    protected override void Update()
    {
        base.Update();

        if (Health <= 0 && isAlive)
        {
            isAlive = false;
            UIController.killedEnemyCount++;
            Destroy(gameObject, 30);
        }

        if (speed > 0 && isAlive)
        {
            float newSpeed = 2 * speed;

            if (target != null && Vector2.Distance(transform.position, target.position) <= detectionDistance)
            {
                //transform.position = Vector2.MoveTowards(transform.position, target.position, newSpeed * Time.deltaTime);
                transform.position = Vector2.MoveTowards(transform.position, target.position, newSpeed * Time.deltaTime * 2);
                FlipSprite(target.position - transform.position);
            }
            else
            {
                MoveByDefault();
            }
        }
        ChangeAnimationState(RUN_ANIMATION);

    }

    private void MoveByDefault()
    {
        if (Vector2.Distance(transform.position, spawnPoint) <= 0.001f)
        {
            GetNewWanderTarget();
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) <= 0.001f)
        {
            targetPosition = spawnPoint;
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        FlipSprite(targetPosition - (Vector2)transform.position);
    }

    private void GetNewWanderTarget()
    {
        float randomAngle = Random.Range(0f, 360f);
        wanderDirection = Quaternion.Euler(0, 0, randomAngle) * Vector2.right;
        targetPosition = (Vector2)transform.position + wanderDirection.normalized * wanderRadius;
    }

    protected void FlipSprite(Vector2 direction)
    {
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") &&
            collision.gameObject.TryGetComponent<DamageableCharacter>(out var damageableObject)
            )
        {
            Vector2 direction = (collision.gameObject.transform.position - transform.position).normalized;
            Vector2 knockback = direction * knockbackForce;
            damageableObject.OnHit(damage, knockback);
        }
    }
}