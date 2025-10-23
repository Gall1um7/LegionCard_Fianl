using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;           // 移动速度
    private Rigidbody2D rb;                // 刚体组件
    private Vector2 moveInput;             // 移动输入

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // 获取刚体
    }

    void Update()
    {
        // 获取玩家输入（WASD / 方向键）
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize(); // 防止对角线加速
    }

    void FixedUpdate()
    {
        // 物理移动
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}
