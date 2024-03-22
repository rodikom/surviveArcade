using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Visible fields
    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float detectionDistance;
    [SerializeField]
    protected float healthPoints;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Rigidbody2D rb;

    // Invisible fields
    private SpriteRenderer spriteRenderer;

    private Vector2 spawnPoint;

    public float wanderRadius;
    private Vector2 targetPosition;
    private Vector2 wanderDirection;

    // Animation states
    protected string enemy_idle = "idle_dino";
    protected string enemy_walk = "walk_dino";
    protected string enemy_run = "run_dino";
    protected string enemy_hit = "hit_dino";
    protected string enemy_death = "death_dino";

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.position = spawnPoint;
        rb = GetComponent<Rigidbody2D>();

        spawnPoint = transform.position;

        GetNewWanderTarget();
    }

    void Update()
    {
        if (healthPoints <= 0)
        {
            Debug.Log("Enemy is dead");
            animator.Play(enemy_death);
        }

        if (target != null && Vector2.Distance(transform.position, target.position) <= detectionDistance)
        {
            animator.Play(enemy_run);
            speed = 4f;
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            FlipSprite(target.position - transform.position);
        }
        else
        {
            animator.Play(enemy_walk);
            MoveByDefault();
        }
    }

    void MoveByDefault()
    {
        speed = 1f;
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

    void GetNewWanderTarget()
    {
        animator.Play(enemy_idle);
        float randomAngle = Random.Range(0f, 360f);
        wanderDirection = Quaternion.Euler(0, 0, randomAngle) * Vector2.right;
        targetPosition = (Vector2)transform.position + wanderDirection.normalized * wanderRadius;
    }

    void FlipSprite(Vector2 direction)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            animator.Play(enemy_hit);
            healthPoints -= collision.gameObject.GetComponent<Bullet>().damage;
            Destroy(collision.gameObject);
        }
    }

    protected void Death()
    {
        Debug.Log("Enemy is dead");
        Destroy(gameObject);
    }
}
