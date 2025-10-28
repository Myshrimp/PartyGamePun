using UnityEngine;

namespace  Control
{
    using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum CameraState
    {
        FollowPlayer,
        LockTarget,
        Shake
    }

    [Header("Camera References")]
    [SerializeField] private Transform player; // 玩家角色
    [SerializeField] private Camera cam; // 相机组件

    [Header("Follow Player Settings")]
    [SerializeField] private Vector3 followOffset = new Vector3(0, 2, -5); // 跟随偏移
    [SerializeField] private float followSmoothness = 5f; // 跟随平滑度
    [SerializeField] private float rotationSmoothness = 3f; // 旋转平滑度
    [SerializeField] private float minZoom = 3f; // 最小缩放
    [SerializeField] private float maxZoom = 10f; // 最大缩放
    [SerializeField] private float zoomSensitivity = 2f; // 缩放灵敏度
    [SerializeField] private bool followPlayerRotation = true; // 是否跟随玩家旋转

    [Header("Lock Target Settings")]
    [SerializeField] private Transform lockTarget; // 锁定目标
    [SerializeField] private float lockSmoothness = 3f; // 锁定平滑度
    [SerializeField] private Vector3 lockOffset = new Vector3(0, 1, 0); // 锁定偏移
    [SerializeField] private float minFOV = 40f; // 最小视野
    [SerializeField] private float maxFOV = 80f; // 最大视野
    [SerializeField] private float targetPadding = 2f; // 目标边距

    [Header("Shake Settings")]
    [SerializeField] private float shakeIntensity = 0.5f; // 震动强度
    [SerializeField] private float shakeDuration = 0.5f; // 震动持续时间
    [SerializeField] private float shakeDecay = 0.5f; // 震动衰减

    private CameraState currentState = CameraState.FollowPlayer;
    private float currentZoom; // 当前缩放
    private Vector3 originalPosition; // 原始位置
    private float currentShakeIntensity; // 当前震动强度
    private float currentShakeDuration; // 当前震动剩余时间
    private Quaternion targetRotation; // 目标旋转

    private void Start()
    {
        if (cam == null)
            cam = GetComponent<Camera>();
        
        currentZoom = followOffset.magnitude;
        originalPosition = transform.position;
        targetRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        switch (currentState)
        {
            case CameraState.FollowPlayer:
                FollowPlayer();
                HandleZoom();
                break;
            case CameraState.LockTarget:
                LockTarget();
                break;
            case CameraState.Shake:
                CameraShake();
                FollowPlayer(); // 震动时也保持跟随
                break;
        }
    }

    /// <summary>
    /// 跟随玩家状态
    /// </summary>
    private void FollowPlayer()
    {
        if (player == null) return;

        // 计算目标位置
        Vector3 targetPosition = CalculateTargetPosition();
        
        // 平滑移动相机位置
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSmoothness * Time.deltaTime);
        
        // 处理相机旋转
        HandleCameraRotation();
    }

    /// <summary>
    /// 计算目标位置
    /// </summary>
    private Vector3 CalculateTargetPosition()
    {
        if (followPlayerRotation)
        {
            // 考虑玩家旋转的偏移
            Vector3 rotatedOffset = player.rotation * followOffset.normalized * currentZoom;
            return player.position + rotatedOffset;
        }
        else
        {
            // 不考虑玩家旋转的偏移
            return player.position + followOffset.normalized * currentZoom;
        }
    }

    /// <summary>
    /// 处理相机旋转
    /// </summary>
    private void HandleCameraRotation()
    {
        if (followPlayerRotation)
        {
            // 跟随玩家旋转
            targetRotation = Quaternion.LookRotation(player.position - transform.position);
        }
        else
        {
            // 始终看向玩家
            targetRotation = Quaternion.LookRotation(player.position - transform.position);
        }
        
        // 平滑旋转相机
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothness * Time.deltaTime);
    }

    /// <summary>
    /// 处理缩放
    /// </summary>
    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= scroll * zoomSensitivity;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
    }

    /// <summary>
    /// 锁定目标状态
    /// </summary>
    private void LockTarget()
    {
        if (player == null || lockTarget == null) return;

        // 计算两个目标之间的中点
        Vector3 midPoint = (player.position + lockTarget.position) / 2f;
        
        // 计算两个目标之间的距离
        float distance = Vector3.Distance(player.position, lockTarget.position);
        
        // 根据距离动态调整相机位置和视野
        AdjustCameraForTargets(midPoint, distance);
        
        // 看向中点
        transform.LookAt(midPoint + lockOffset);
    }

    /// <summary>
    /// 根据目标调整相机
    /// </summary>
    private void AdjustCameraForTargets(Vector3 midPoint, float distance)
    {
        // 动态调整相机距离
        float targetDistance = Mathf.Clamp(distance * 1.5f, minZoom, maxZoom * 1.5f);
        Vector3 desiredPosition = midPoint - transform.forward * targetDistance + lockOffset;
        
        transform.position = Vector3.Lerp(transform.position, desiredPosition, lockSmoothness * Time.deltaTime);

        // 动态调整视野
        float targetFOV = Mathf.Clamp(distance * 2f + minFOV, minFOV, maxFOV);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, lockSmoothness * Time.deltaTime);
    }

    /// <summary>
    /// 相机震动效果
    /// </summary>
    private void CameraShake()
    {
        if (currentShakeDuration > 0)
        {
            // 随机震动偏移
            Vector3 shakeOffset = Random.insideUnitSphere * currentShakeIntensity;
            transform.position += shakeOffset;
            
            // 衰减震动
            currentShakeIntensity -= shakeDecay * Time.deltaTime;
            currentShakeDuration -= Time.deltaTime;
            
            // 确保震动强度不为负
            currentShakeIntensity = Mathf.Max(0, currentShakeIntensity);
        }
        else
        {
            // 震动结束，自动切换回跟随状态
            SwitchState(CameraState.FollowPlayer);
        }
    }

    /// <summary>
    /// 切换相机状态
    /// </summary>
    /// <param name="newState">新状态</param>
    public void SwitchState(CameraState newState)
    {
        currentState = newState;
        
        // 状态切换时的初始化
        switch (newState)
        {
            case CameraState.FollowPlayer:
                // 重置视野到默认值
                if (cam != null)
                    cam.fieldOfView = 60f;
                break;
                
            case CameraState.Shake:
                // 初始化震动参数
                currentShakeIntensity = shakeIntensity;
                currentShakeDuration = shakeDuration;
                break;
        }
    }

    /// <summary>
    /// 设置锁定目标
    /// </summary>
    /// <param name="target">要锁定的目标</param>
    public void SetLockTarget(Transform target)
    {
        lockTarget = target;
        if (target != null)
        {
            SwitchState(CameraState.LockTarget);
        }
        else
        {
            SwitchState(CameraState.FollowPlayer);
        }
    }

    /// <summary>
    /// 触发相机震动
    /// </summary>
    /// <param name="intensity">震动强度</param>
    /// <param name="duration">震动持续时间</param>
    public void TriggerShake(float intensity = 0.5f, float duration = 0.5f)
    {
        shakeIntensity = intensity;
        shakeDuration = duration;
        SwitchState(CameraState.Shake);
    }

    /// <summary>
    /// 设置玩家目标
    /// </summary>
    /// <param name="playerTransform">玩家Transform</param>
    public void SetPlayer(Transform playerTransform)
    {
        player = playerTransform;
    }

    /// <summary>
    /// 设置是否跟随玩家旋转
    /// </summary>
    /// <param name="follow">是否跟随</param>
    public void SetFollowPlayerRotation(bool follow)
    {
        followPlayerRotation = follow;
    }

    /// <summary>
    /// 获取当前相机状态
    /// </summary>
    /// <returns>当前相机状态</returns>
    public CameraState GetCurrentState()
    {
        return currentState;
    }

    // 在编辑器中显示调试信息
    private void OnDrawGizmos()
    {
        if (player != null && lockTarget != null && currentState == CameraState.LockTarget)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(player.position, lockTarget.position);
            Gizmos.DrawWireSphere((player.position + lockTarget.position) / 2f, 0.5f);
        }
        
        // 显示相机跟随位置
        if (player != null && currentState == CameraState.FollowPlayer)
        {
            Gizmos.color = Color.blue;
            Vector3 targetPos = CalculateTargetPosition();
            Gizmos.DrawLine(player.position, targetPos);
            Gizmos.DrawWireSphere(targetPos, 0.3f);
        }
    }
}
}
