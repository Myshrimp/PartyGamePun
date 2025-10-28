using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask groundLayerMask = ~0; // 默认为所有层

    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool isGrounded;
    private bool isJumping;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckGrounded();
        HandleRotation();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
    }

    /// <summary>
    /// 设置移动方向和速度
    /// </summary>
    /// <param name="direction">移动方向</param>
    /// <param name="speed">移动速度</param>
    public void Move(Vector3 direction, float speed)
    {
        moveDirection = direction.normalized;
        moveSpeed = speed;
    }

    /// <summary>
    /// 跳跃函数
    /// </summary>
    public void Jump()
    {
        if (isGrounded)
        {
            isJumping = true;
        }
    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }

    /// <summary>
    /// 处理移动逻辑
    /// </summary>
    private void HandleMovement()
    {
        if (moveDirection != Vector3.zero)
        {
            Vector3 targetVelocity = moveDirection * moveSpeed;
            targetVelocity.y = rb.velocity.y; // 保持Y轴速度不变
            rb.velocity = targetVelocity;
        }
    }

    /// <summary>
    /// 处理旋转逻辑
    /// </summary>
    private void HandleRotation()
    {
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// 处理跳跃逻辑
    /// </summary>
    private void HandleJump()
    {
        if (isJumping)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            isJumping = false;
        }
    }

    /// <summary>
    /// 检测是否着地
    /// </summary>
    private void CheckGrounded()
    {
        // 使用射线检测判断是否接触地面
        RaycastHit hit;
        Vector3 rayStart = transform.position + Vector3.up * 0.1f;
        
        if (Physics.Raycast(rayStart, Vector3.down, out hit, groundCheckDistance + 0.1f, groundLayerMask))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    // 在编辑器中显示地面检测射线
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Vector3 rayStart = transform.position + Vector3.up * 0.1f;
        Gizmos.DrawLine(rayStart, rayStart + Vector3.down * (groundCheckDistance + 0.1f));
    }
}