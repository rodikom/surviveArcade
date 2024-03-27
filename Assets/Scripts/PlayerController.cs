using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Visible fields
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float moveSpeed = 10f;

    // Invisible fields
    private Vector2 moveDirection = Vector2.zero;

    // animation states
    private string currentState = "player0_idle";
    private string playerIdle = "player0_idle";
    private string playerRun = "player0_run";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void OnMove(InputValue iv)
    {
        moveDirection = iv.Get<Vector2>();

        if (moveDirection != Vector2.zero) {
            ChangeAnimationState(playerRun);
        }
        else {
            ChangeAnimationState(playerIdle);
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

    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) {
            return;
        }

        animator.Play(newState);
        currentState = newState;
    }
}
