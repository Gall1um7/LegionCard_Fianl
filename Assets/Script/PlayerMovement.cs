using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;           // �ƶ��ٶ�
    private Rigidbody2D rb;                // �������
    private Vector2 moveInput;             // �ƶ�����

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // ��ȡ����
    }

    void Update()
    {
        // ��ȡ������루WASD / �������
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize(); // ��ֹ�Խ��߼���
    }

    void FixedUpdate()
    {
        // �����ƶ�
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}
