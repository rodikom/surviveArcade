using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private string IDLE_ANIMATION = "IDLE";
    private string RUN_ANIMATION = "RUN";

    [SerializeField]
    private GameObject playerAim;

    private int restorHPCount = 5;

    [SerializeField]
    private float healPower = 10f;

    public int RestorHPCount
    {
        get {
            return restorHPCount;
        }
        set {
            restorHPCount = value;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();

        CURRENT_ANIMATION = IDLE_ANIMATION;
    }

    protected override void Update()
    {
        if (Health <= 0) {
            playerAim.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void OnMove(InputValue iv)
    {
        if (Health <= 0) {
            moveDirection = Vector2.zero;
            return;
        }

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

    private void OnHealing()
    {
        if (restorHPCount <= 0) {
            return;
        }

        Health = Mathf.Min(MaxHealth, Health + healPower);
        restorHPCount--;
    }

    public void Death()
    {
        Loader.Loading(Loader.Scene.Death);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("RestorHP")) {
            restorHPCount++;
            SpawnService.Destroy(collision.gameObject);
        }
    }
}
