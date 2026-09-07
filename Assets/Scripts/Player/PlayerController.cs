#region v1
//using UnityEngine;
//using UnityEngine.InputSystem;


//[RequireComponent(typeof(Rigidbody2D))]
//public class PlayerController : MonoBehaviour
//{
//    [Header("Movement")]
//    [SerializeField] private float moveSpeed = 6f;
//    [SerializeField] private float jumpForce = 12f;

//    [Header("Ground Check")]
//    [SerializeField] private Transform groundCheck;
//    [SerializeField] private float groundCheckRadius = 0.2f;
//    [SerializeField] private LayerMask groundLayer;

//    [Header("Animation")]
//    [SerializeField] private Animator animator;

//    private static readonly int SpeedParam = Animator.StringToHash("Speed");
//    private static readonly int IsGroundedParam = Animator.StringToHash("IsGrounded");

//    private Rigidbody2D rb;
//    private float moveInput;
//    private bool jumpRequested;
//    private bool isGrounded;
//    private bool facingRight = true;

//    public bool FacingRight => facingRight;

//    private void Awake()
//    {
//        rb = GetComponent<Rigidbody2D>();
//    }

//    private void OnMove(InputValue value)
//    {
//        moveInput = value.Get<Vector2>().x;
//    }

//    private void OnJump(InputValue value)
//    {
//        if (value.isPressed)
//        {
//            jumpRequested = true;
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
//        ApplyJump();
//    }

//    private void UpdateGroundedState()
//    {
//        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
//    }

//    private void ApplyMovement()
//    {
//        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
//    }

//    private void ApplyJump()
//    {
//        if (jumpRequested && isGrounded)
//        {
//            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
//        }

//        jumpRequested = false;
//    }

//    private void UpdateFacing()
//    {
//        if (moveInput > 0f && !facingRight)
//        {
//            Flip();
//        }
//        else if (moveInput < 0f && facingRight)
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
//        animator.SetFloat(SpeedParam, Mathf.Abs(moveInput));
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
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpForce = 12f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    private static readonly int SpeedParam = Animator.StringToHash("Speed");
    private static readonly int IsGroundedParam = Animator.StringToHash("IsGrounded");

    private Rigidbody2D rb;
    private PlayerMelee playerMelee;
    private float moveInput;
    private bool jumpRequested;
    private bool isGrounded;
    private bool facingRight = true;

    public bool FacingRight => facingRight;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMelee = GetComponent<PlayerMelee>();
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>().x;
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            jumpRequested = true;
        }
    }

    private void Update()
    {
        UpdateGroundedState();
        UpdateFacing();
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        ApplyJump();
    }

    private void UpdateGroundedState()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void ApplyMovement()
    {
        bool isAttacking = playerMelee != null && playerMelee.IsAttacking;
        float horizontalVelocity = isAttacking ? 0f : moveInput * moveSpeed;
        rb.linearVelocity = new Vector2(horizontalVelocity, rb.linearVelocity.y);
    }

    private void ApplyJump()
    {
        if (jumpRequested && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        jumpRequested = false;
    }

    private void UpdateFacing()
    {
        if (moveInput > 0f && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0f && facingRight)
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
        animator.SetFloat(SpeedParam, Mathf.Abs(moveInput));
        animator.SetBool(IsGroundedParam, isGrounded);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
#endregion