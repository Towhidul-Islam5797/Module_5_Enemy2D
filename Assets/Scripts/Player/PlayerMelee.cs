#region v1
//using UnityEngine;
//using UnityEngine.InputSystem;

//public class PlayerMelee : MonoBehaviour
//{
//    [SerializeField] private float attackCooldown = 0.5f;

//    private float cooldownTimer;

//    public bool IsAttacking { get; private set; }

//    private void OnAttack(InputValue value)
//    {
//        if (!value.isPressed) return;
//        if (cooldownTimer > 0f) return;

//        PerformAttack();
//        cooldownTimer = attackCooldown;
//    }

//    private void Update()
//    {
//        UpdateCooldown();
//    }

//    private void UpdateCooldown()
//    {
//        if (cooldownTimer > 0f)
//        {
//            cooldownTimer -= Time.deltaTime;
//        }
//    }

//    private void PerformAttack()
//    {
//        // Hitbox detection and animation trigger go here once the
//        // attack animations are ready.
//        IsAttacking = true;
//        Debug.Log("Melee attack triggered");
//    }
//}
#endregion

#region v2
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMelee : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private float attackDuration = 0.4f;
    [SerializeField] private Animator animator;

    private static readonly int AttackTrigger = Animator.StringToHash("Attack");

    private float cooldownTimer;
    private float attackTimer;

    public bool IsAttacking { get; private set; }

    private void OnAttack(InputValue value)
    {
        if (!value.isPressed) return;
        if (cooldownTimer > 0f) return;

        PerformAttack();
        cooldownTimer = attackCooldown;
    }

    private void Update()
    {
        UpdateCooldown();
        UpdateAttackState();
    }

    private void UpdateCooldown()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    private void UpdateAttackState()
    {
        if (!IsAttacking) return;

        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            IsAttacking = false;
        }
    }

    private void PerformAttack()
    {
        IsAttacking = true;
        attackTimer = attackDuration;
        animator.SetTrigger(AttackTrigger);
    }
}
#endregion