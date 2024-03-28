using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableCharacter : MonoBehaviour, IDamageable
{
    // Unity components
    protected Animator animator;
    protected Rigidbody2D rb;
    protected Collider2D physicsCollider;

    // Animation states
    protected string CURRENT_ANIMATION;
    [SerializeField]
    protected string HIT_ANIMATION;
    [SerializeField]
    protected string DEATH_ANIMATION;

    // Health controll
    [SerializeField]
    protected float _health;
    public float Health
    {
        set {
            if (value < _health) {
                if (hitSound != null) { 
                    hitSound.Play(); 
                }
                animator.Play(HIT_ANIMATION);
            }
            if (hitSound != null) { 
                hitSound.Stop(); 
            }

            _health = value;

            if (_health <= 0) {
                if (deathSound != null) { 
                    deathSound.Play(); 
                }
                animator.Play(DEATH_ANIMATION);

                Targetable = false;
                if (deathSound != null) {
                    deathSound.Stop();
                }
            }
        }
        get { 
            return _health; 
        }
    }

    // Targetable controll
    public bool _targetable = true;
    public bool Targetable
    {
        set {
            _targetable = value;
            if (disableSimulation) {
                rb.simulated = false;
            }
            physicsCollider.enabled = value;
        }
        get { 
            return _targetable;
        }
    }

    // Invinciable controll
    public float invincibilityTime = 0.25f;
    private float invincibleTimeElapsed = 0;

    public bool _invincible = false;
    public bool Invincible
    {
        set {
            _invincible = value;
            if (_invincible) {
                invincibleTimeElapsed = 0;
            }
        }
        get { 
            return _invincible;
        }
    }

    // Physical interaction On/Off 
    private bool disableSimulation = false;
    private bool isInvicibleEnabled = false;
    private bool canPush = true;

    // Sounds
    [SerializeField]
    private AudioSource hitSound;
    [SerializeField]
    private AudioSource deathSound;

    protected virtual void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        physicsCollider = gameObject.GetComponent<Collider2D>();
    }

    protected virtual void Update()
    {
        if (Invincible) {
            invincibleTimeElapsed += Time.deltaTime;
            if (invincibleTimeElapsed > invincibilityTime) {
                Invincible = false;
            }
        }
    }

    public void OnHit(float damage)
    {
        if (!Invincible) {
            float criticalChance = Random.Range(0f, 1f);
            int crit = (criticalChance > 0.5f) ? 2 : 1;

            Health -= damage * crit;
            if (isInvicibleEnabled) {
                Invincible = true;
            }
        }
    }
    public void OnHit(float damage, Vector2 knockback)
    {
        if (!Invincible) {
            float criticalChance = Random.Range(0f, 1f);
            int crit = (criticalChance > 0.5f) ? 2 : 1;

            Health -= damage * crit;
            rb.AddForce(knockback * rb.mass, ForceMode2D.Impulse);
            if (isInvicibleEnabled) {
                Invincible = true;
            }
        }
    }
    protected void ChangeAnimationState(string newState)
    {
        if (CURRENT_ANIMATION == newState) {
            return;
        }

        animator.Play(newState);
        CURRENT_ANIMATION = newState;
    }
}
