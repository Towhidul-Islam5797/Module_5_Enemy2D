using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyChase : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float stopDistance = 1.2f;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    private static readonly int IsMovingParam = Animator.StringToHash("IsMoving");

    private Rigidbody2D rb;
    private Transform target;
    private bool facingRight = true;
    private bool isMoving;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        FindTarget();
    }

    private void FindTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            target = player.transform;
        }
    }

    private void Update()
    {
        UpdateFacing();
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        if (target == null)
        {
            isMoving = false;
            return;
        }

        float offsetX = target.position.x - transform.position.x;

        if (Mathf.Abs(offsetX) <= stopDistance)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            isMoving = false;
            return;
        }

        float xSpeed = Mathf.Sign(offsetX) * moveSpeed;
        rb.linearVelocity = new Vector2(xSpeed, rb.linearVelocity.y);
        isMoving = true;
    }

    private void UpdateFacing()
    {
        if (target == null) return;

        bool shouldFaceRight = target.position.x > transform.position.x;

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

    private void UpdateAnimator()
    {
        animator.SetBool(IsMovingParam, isMoving);
    }
}