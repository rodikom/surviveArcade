using UnityEngine;
public class Bullet : MonoBehaviour, IPoolable
{
    protected Rigidbody2D rb;

    [SerializeField]
    protected float speed = 10f;

    protected float damage;
    protected float lifeTime;
    protected string targetTag;

    private float lifeTimer;
    private ProjectileType projectileType;

    public void Init(
        float damage,
        float lifeTime,
        string targetTag,
        ProjectileType type
    )
    {
        this.damage = damage;
        this.lifeTime = lifeTime;
        this.targetTag = targetTag;
        this.projectileType = type;
    }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
            ReturnToPool();
    }

    protected virtual void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(targetTag) &&
            collision.gameObject.TryGetComponent(out DamageableCharacter dmg))
        {
            dmg.OnHit(damage);
        }

        ReturnToPool();
    }

    public void OnGetFromPool()
    {
        lifeTimer = lifeTime;
        rb.velocity = Vector2.zero;
        gameObject.SetActive(true);
    }

    public void OnReturnToPool()
    {
        rb.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    private void ReturnToPool()
    {
        ServiceLocator
            .Get<ProjectilePool>()
            .Return(projectileType, this);
    }
}