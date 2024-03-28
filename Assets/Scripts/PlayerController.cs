using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : DamageableCharacter
{
    // Unity components
    private SpriteRenderer spriteRenderer;
    
    // Player stats
    [SerializeField]
    private float moveSpeed = 10f;
    private Vector2 moveDirection = Vector2.zero;

    // Animation states
    [SerializeField]
    private string IDLE_ANIMATION;
    [SerializeField]
    private string RUN_ANIMATION;

    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        CURRENT_ANIMATION = IDLE_ANIMATION;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void OnMove(InputValue iv)
    {
        moveDirection = iv.Get<Vector2>();

        if (moveDirection != Vector2.zero) {
            ChangeAnimationState(RUN_ANIMATION);
        }
        else {
            ChangeAnimationState(IDLE_ANIMATION);
        }
    }

    private void Movement()
    {
        rb.velocity = moveDirection * moveSpeed;

        if (moveDirection.x > 0) {
            spriteRenderer.flipX = false;
        } else if (moveDirection.x < 0) {
            spriteRenderer.flipX = true;
        }
    }
}
