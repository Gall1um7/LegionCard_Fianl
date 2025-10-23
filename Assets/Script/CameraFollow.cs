using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;    // ����Ŀ�꣨С�ˣ�
    public float smoothSpeed = 5f;  // ƽ�������ٶ�
    public Vector3 offset;      // ƫ�������ɵ������λ�ã�

    void LateUpdate()
    {
        if (target == null) return;

        // Ŀ��λ�� + ƫ��
        Vector3 desiredPosition = target.position + offset;

        // ƽ���ƶ������
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
