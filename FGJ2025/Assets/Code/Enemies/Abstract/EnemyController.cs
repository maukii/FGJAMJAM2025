using UnityEngine;

[RequireComponent(typeof(Health))]
public abstract class EnemyController : MonoBehaviour
{
    protected EnemyManager enemyManager;
    protected Health health;
    protected Animator animator;

    // For movement
    [SerializeField] protected float mSpeed = 1f;
    [SerializeField] protected float rSpeed = 1f;
    protected Vector3 newPos; // Calculate new position into this before passing it into transform.position
    protected Vector3 target = Vector3.zero; // this is probably always playerpos
    protected bool animMoving;

    // For collision distance calculations
    Vector3 offset;
    float sqrLen;
    float closeDist = 1f;
    [SerializeField] float pushForce = 1f;

    // For attacking
    protected bool attacking = false;

    void OnEnable()
    {
        animator = GetComponentInChildren<Animator>();
        health = GetComponent<Health>();
        health.OnDeath += Die;
    }

    void OnDisable()
    {
        health.OnDeath -= Die;
    }

    protected virtual void Update()
    {
        newPos = transform.position;

        UpdateTargetPos();

        HandleEnemyCollisions();
        if(!health.IsBubbled)
        {
            HandleRotation();
            animMoving = HandleMovement();
            if(CanAttack())
            {
                Attack();
            }
        }else{
            // Animator bool
            animMoving = false;
        }

        UpdateAnimator();

        // Set Y to 0 so the enemy is never under or above ground level
        newPos.y = 0;
        transform.position = newPos;
    }
    
    public void Initialize(EnemyManager em)
    {
        enemyManager = em;
    }

    protected void Die()
    {
        enemyManager.RemoveEnemy(this);
        Destroy(gameObject);
    }

    protected void UpdateAnimator()
    {
        if(!animator)
        {
            return;
        }
        animator.SetBool("Moving", animMoving);
    }

    protected void TriggerAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    protected virtual void HandleEnemyCollisions()
    {
        // Optimization idea: Run this x amount of times per frame from EnemyManager, on x amount of EnemyControllers
        // x = 10, 60fps, 60enemies = 10*60 runs per second, 6 frames (.1 seconds) to run this on every enemy
        // Loop through every enemy and see if there's collisions
        foreach(EnemyController em in enemyManager.EnemyControllers)
        {
            if(em == this)
            {
                continue;
            }
            offset = em.transform.position - transform.position;
            sqrLen = offset.sqrMagnitude;
            if(sqrLen < closeDist * closeDist)
            {
                //Debug.Log("Collision between: " + gameObject.name + " and " + em.gameObject.name);
                newPos += (transform.position - em.transform.position).normalized * pushForce * Time.deltaTime;
            }
        }
    }

    protected virtual void UpdateTargetPos()
    {
        if(enemyManager.PlayerGO)
        {
            target = enemyManager.PlayerGO.transform.position;
        }
    }

    protected abstract void HandleRotation();

    protected abstract bool HandleMovement();

    protected abstract bool CanAttack();

    protected virtual void Attack()
    {
        TriggerAttackAnimation();
    }
}
