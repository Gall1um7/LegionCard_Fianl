using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;    // 跟随目标（小人）
    public float smoothSpeed = 5f;  // 平滑跟随速度
    public Vector3 offset;      // 偏移量（可调整相机位置）

    void LateUpdate()
    {
        if (target == null) return;

        // 目标位置 + 偏移
        Vector3 desiredPosition = target.position + offset;

        // 平滑移动摄像机
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
