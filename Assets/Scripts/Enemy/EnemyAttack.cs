#region v1

//using UnityEngine;

//public class EnemyAttack : MonoBehaviour
//{
//    #region Fields

//    [SerializeField] private float attackRange = 1.2f;
//    [SerializeField] private float attackCooldown = 0.6f;
//    [SerializeField] private Animator animator;

//    private static readonly int AttackTrigger = Animator.StringToHash("Attack");

//    private Transform target;
//    private float cooldownTimer;

//    #endregion

//    #region Unity Lifecycle

//    private void Start()
//    {
//        FindTarget();
//    }

//    private void Update()
//    {
//        UpdateCooldown();
//        TryAttack();
//    }

//    #endregion

//    #region Target Detection

//    private void FindTarget()
//    {
//        GameObject player = GameObject.FindGameObjectWithTag("Player");

//        if (player != null)
//        {
//            target = player.transform;
//        }
//    }

//    #endregion

//    #region Attack

//    private void UpdateCooldown()
//    {
//        if (cooldownTimer > 0f)
//        {
//            cooldownTimer -= Time.deltaTime;
//        }
//    }

//    private void TryAttack()
//    {
//        if (target == null || cooldownTimer > 0f) return;

//        float distanceToTarget = Mathf.Abs(target.position.x - transform.position.x);

//        if (distanceToTarget <= attackRange)
//        {
//            PerformAttack();
//        }
//    }

//    private void PerformAttack()
//    {
//        animator.SetTrigger(AttackTrigger);
//        cooldownTimer = attackCooldown;
//    }

//    #endregion
//}

#endregion

#region v2
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    #region Fields

    [SerializeField] private float attackRange = 1.2f;
    [SerializeField] private float attackCooldown = 0.6f;
    [SerializeField] private int damage = 10;
    [SerializeField] private Animator animator;

    private static readonly int AttackTrigger = Animator.StringToHash("Attack");

    private Transform target;
    private Health targetHealth;
    private float cooldownTimer;

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        FindTarget();
    }

    private void Update()
    {
        UpdateCooldown();
        TryAttack();
    }

    #endregion

    #region Target Detection

    private void FindTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            target = player.transform;
            targetHealth = player.GetComponent<Health>();
        }
    }

    #endregion

    #region Attack

    private void UpdateCooldown()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    private void TryAttack()
    {
        if (target == null || cooldownTimer > 0f) return;

        float distanceToTarget = Mathf.Abs(target.position.x - transform.position.x);

        if (distanceToTarget <= attackRange)
        {
            PerformAttack();
        }
    }

    private void PerformAttack()
    {
        animator.SetTrigger(AttackTrigger);
        cooldownTimer = attackCooldown;

        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
        }
    }

    #endregion
}
#endregion