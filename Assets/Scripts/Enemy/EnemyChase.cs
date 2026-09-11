#region v1
//using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
//public class EnemyChase : MonoBehaviour
//{
//    [Header("Movement")]
//    [SerializeField] private float moveSpeed = 3f;
//    [SerializeField] private float stopDistance = 1.2f;

//    [Header("Ground Check")]
//    [SerializeField] private Transform groundCheck;
//    [SerializeField] private float groundCheckRadius = 0.2f;
//    [SerializeField] private LayerMask groundLayer;

//    [Header("Animation")]
//    [SerializeField] private Animator animator;

//    private static readonly int SpeedParam = Animator.StringToHash("Speed");
//    private static readonly int IsGroundedParam = Animator.StringToHash("IsGrounded");

//    private Rigidbody2D rb;
//    private Transform target;
//    private bool facingRight = true;
//    private bool isGrounded;

//    private void Awake()
//    {
//        rb = GetComponent<Rigidbody2D>();
//    }

//    private void Start()
//    {
//        FindTarget();
//    }

//    private void FindTarget()
//    {
//        GameObject player = GameObject.FindGameObjectWithTag("Player");

//        if (player != null)
//        {
//            target = player.transform;
//        }
//    }

//    private void Update()
//    {
//        UpdateGroundedState();
//        UpdateFacing();
//        UpdateAnimator();
//    }

//    private void FixedUpdate()
//    {
//        ApplyMovement();
//    }

//    private void ApplyMovement()
//    {
//        if (target == null)
//        {
//            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
//            return;
//        }

//        float offsetX = target.position.x - transform.position.x;

//        if (Mathf.Abs(offsetX) <= stopDistance)
//        {
//            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
//            return;
//        }

//        float xSpeed = Mathf.Sign(offsetX) * moveSpeed;
//        rb.linearVelocity = new Vector2(xSpeed, rb.linearVelocity.y);
//    }

//    private void UpdateGroundedState()
//    {
//        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
//    }

//    private void UpdateFacing()
//    {
//        if (target == null) return;

//        bool shouldFaceRight = target.position.x > transform.position.x;

//        if (shouldFaceRight != facingRight)
//        {
//            Flip();
//        }
//    }

//    private void Flip()
//    {
//        facingRight = !facingRight;
//        Vector3 scale = transform.localScale;
//        scale.x *= -1f;
//        transform.localScale = scale;
//    }

//    private void UpdateAnimator()
//    {
//        float normalizedSpeed = Mathf.Abs(rb.linearVelocity.x) / moveSpeed;
//        animator.SetFloat(SpeedParam, normalizedSpeed);
//        animator.SetBool(IsGroundedParam, isGrounded);
//    }

//    private void OnDrawGizmosSelected()
//    {
//        if (groundCheck == null) return;
//        Gizmos.color = Color.green;
//        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
//    }
//}
#endregion

#region v2
//using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
//public class EnemyChase : MonoBehaviour
//{
//    [Header("Chase")]
//    [SerializeField] private float moveSpeed = 3f;
//    [SerializeField] private float stopDistance = 1.2f;
//    [SerializeField] private float detectionRange = 5f;

//    [Header("Patrol")]
//    [SerializeField] private float patrolSpeed = 1.5f;
//    [SerializeField] private float patrolRange = 2f;

//    [Header("Ground Check")]
//    [SerializeField] private Transform groundCheck;
//    [SerializeField] private float groundCheckRadius = 0.2f;
//    [SerializeField] private LayerMask groundLayer;

//    [Header("Animation")]
//    [SerializeField] private Animator animator;

//    private static readonly int SpeedParam = Animator.StringToHash("Speed");
//    private static readonly int IsGroundedParam = Animator.StringToHash("IsGrounded");

//    private Rigidbody2D rb;
//    private Transform target;
//    private Vector2 spawnPosition;
//    private int patrolDirection = 1;
//    private bool facingRight = true;
//    private bool isGrounded;
//    private bool isChasing;

//    private void Awake()
//    {
//        rb = GetComponent<Rigidbody2D>();
//    }

//    private void Start()
//    {
//        spawnPosition = transform.position;
//        FindTarget();
//    }

//    private void FindTarget()
//    {
//        GameObject player = GameObject.FindGameObjectWithTag("Player");

//        if (player != null)
//        {
//            target = player.transform;
//        }
//    }

//    private void Update()
//    {
//        UpdateGroundedState();
//        UpdateChaseState();
//        UpdateFacing();
//        UpdateAnimator();
//    }

//    private void FixedUpdate()
//    {
//        if (isChasing)
//        {
//            ApplyChaseMovement();
//        }
//        else
//        {
//            ApplyPatrolMovement();
//        }
//    }

//    private void UpdateChaseState()
//    {
//        if (target == null)
//        {
//            isChasing = false;
//            return;
//        }

//        float distanceToTarget = Mathf.Abs(target.position.x - transform.position.x);
//        isChasing = distanceToTarget <= detectionRange;
//    }

//    private void ApplyChaseMovement()
//    {
//        float offsetX = target.position.x - transform.position.x;

//        if (Mathf.Abs(offsetX) <= stopDistance)
//        {
//            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
//            return;
//        }

//        float xSpeed = Mathf.Sign(offsetX) * moveSpeed;
//        rb.linearVelocity = new Vector2(xSpeed, rb.linearVelocity.y);
//    }

//    private void ApplyPatrolMovement()
//    {
//        float offsetFromSpawn = transform.position.x - spawnPosition.x;

//        if (offsetFromSpawn >= patrolRange)
//        {
//            patrolDirection = -1;
//        }
//        else if (offsetFromSpawn <= -patrolRange)
//        {
//            patrolDirection = 1;
//        }

//        rb.linearVelocity = new Vector2(patrolDirection * patrolSpeed, rb.linearVelocity.y);
//    }

//    private void UpdateGroundedState()
//    {
//        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
//    }

//    private void UpdateFacing()
//    {
//        bool shouldFaceRight = isChasing && target != null
//            ? target.position.x > transform.position.x
//            : patrolDirection > 0;

//        if (shouldFaceRight != facingRight)
//        {
//            Flip();
//        }
//    }

//    private void Flip()
//    {
//        facingRight = !facingRight;
//        Vector3 scale = transform.localScale;
//        scale.x *= -1f;
//        transform.localScale = scale;
//    }

//    private void UpdateAnimator()
//    {
//        bool isMoving = Mathf.Abs(rb.linearVelocity.x) > 0.01f;
//        animator.SetFloat(SpeedParam, isMoving ? 1f : 0f);
//        animator.SetBool(IsGroundedParam, isGrounded);
//    }

//    private void OnDrawGizmosSelected()
//    {
//        if (groundCheck != null)
//        {
//            Gizmos.color = Color.green;
//            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
//        }

//        Gizmos.color = Color.yellow;
//        Gizmos.DrawWireSphere(transform.position, detectionRange);

//        Gizmos.color = Color.cyan;
//        Vector3 patrolCenter = Application.isPlaying ? (Vector3)spawnPosition : transform.position;
//        Gizmos.DrawLine(patrolCenter + Vector3.left * patrolRange, patrolCenter + Vector3.right * patrolRange);
//    }
//}
#endregion

#region v3
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyChase : MonoBehaviour
{
    #region Fields

    [Header("Chase")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float stopDistance = 1.2f;
    [SerializeField] private float detectionRange = 5f;

    [Header("Patrol")]
    [SerializeField] private float patrolSpeed = 1.5f;
    [SerializeField] private float patrolRange = 2f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    private static readonly int SpeedParam = Animator.StringToHash("Speed");
    private static readonly int IsGroundedParam = Animator.StringToHash("IsGrounded");

    private Rigidbody2D rb;
    private Transform target;
    private Vector2 spawnPosition;
    private int patrolDirection = 1;
    private bool facingRight = true;
    private bool isGrounded;
    private bool isChasing;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        spawnPosition = transform.position;
        FindTarget();
    }

    private void Update()
    {
        UpdateGroundedState();
        UpdateChaseState();
        UpdateFacing();
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        if (isChasing)
        {
            ApplyChaseMovement();
        }
        else
        {
            ApplyPatrolMovement();
        }
    }

    #endregion

    #region Target Detection

    private void FindTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            target = player.transform;
        }
    }

    private void UpdateChaseState()
    {
        if (target == null)
        {
            isChasing = false;
            return;
        }

        float distanceToTarget = Mathf.Abs(target.position.x - transform.position.x);
        isChasing = distanceToTarget <= detectionRange;
    }

    #endregion

    #region Movement

    private void ApplyChaseMovement()
    {
        float offsetX = target.position.x - transform.position.x;

        if (Mathf.Abs(offsetX) <= stopDistance)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }

        float xSpeed = Mathf.Sign(offsetX) * moveSpeed;
        rb.linearVelocity = new Vector2(xSpeed, rb.linearVelocity.y);
    }

    private void ApplyPatrolMovement()
    {
        float offsetFromSpawn = transform.position.x - spawnPosition.x;

        if (offsetFromSpawn >= patrolRange)
        {
            patrolDirection = -1;
        }
        else if (offsetFromSpawn <= -patrolRange)
        {
            patrolDirection = 1;
        }

        rb.linearVelocity = new Vector2(patrolDirection * patrolSpeed, rb.linearVelocity.y);
    }

    private void UpdateGroundedState()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    #endregion

    #region Facing

    private void UpdateFacing()
    {
        bool shouldFaceRight = isChasing && target != null
            ? target.position.x > transform.position.x
            : patrolDirection > 0;

        if (shouldFaceRight != facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }

    #endregion

    #region Animation

    private void UpdateAnimator()
    {
        bool isMoving = Mathf.Abs(rb.linearVelocity.x) > 0.01f;
        animator.SetFloat(SpeedParam, isMoving ? 1f : 0f);
        animator.SetBool(IsGroundedParam, isGrounded);
    }

    #endregion

    #region Gizmos

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.cyan;
        Vector3 patrolCenter = Application.isPlaying ? (Vector3)spawnPosition : transform.position;
        Gizmos.DrawLine(patrolCenter + Vector3.left * patrolRange, patrolCenter + Vector3.right * patrolRange);
    }

    #endregion
}
#endregion