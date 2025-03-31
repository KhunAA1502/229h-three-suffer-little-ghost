using UnityEngine;

public class LittleGhost : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float maxFallSpeed = -15f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpCooldown = 0.5f;
    [SerializeField] private int maxJumps = 2;

    private Rigidbody rb;
    private float horizontalInput;
    private bool isJumpPressed;
    private int jumpsRemaining;
    private float lastJumpTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        jumpsRemaining = maxJumps;
    }

    private void Update()
    {
        // รับค่าปุ่ม A และ D สำหรับการเคลื่อนที่
        horizontalInput = Input.GetKey(KeyCode.A) ? -1 : Input.GetKey(KeyCode.D) ? 1 : 0;

        // รับค่าการกระโดด (Space)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumpPressed = true;
        }
    }

    private void FixedUpdate()
    {
        // การเคลื่อนที่แนวนอน
        Vector3 movement = new Vector3(horizontalInput * moveSpeed, rb.velocity.y, 0);
        rb.velocity = movement;

        // การกระโดด
        if (isJumpPressed && Time.time - lastJumpTime > jumpCooldown && jumpsRemaining > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
            jumpsRemaining--;
            lastJumpTime = Time.time;
        }

        // จำกัดความเร็วตก
        if (rb.velocity.y < maxFallSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, maxFallSpeed, 0);
        }

        // รีเซ็ตการนับกระโดดเมื่ออยู่ที่พื้น (ตรวจสอบจากความเร็ว)
        if (rb.velocity.y == 0)
        {
            jumpsRemaining = maxJumps;
        }

        isJumpPressed = false;
    }
}