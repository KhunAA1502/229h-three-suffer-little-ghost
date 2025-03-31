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
        // �Ѻ��һ��� A ��� D ����Ѻ�������͹���
        horizontalInput = Input.GetKey(KeyCode.A) ? -1 : Input.GetKey(KeyCode.D) ? 1 : 0;

        // �Ѻ��ҡ�á��ⴴ (Space)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumpPressed = true;
        }
    }

    private void FixedUpdate()
    {
        // �������͹����ǹ͹
        Vector3 movement = new Vector3(horizontalInput * moveSpeed, rb.velocity.y, 0);
        rb.velocity = movement;

        // ��á��ⴴ
        if (isJumpPressed && Time.time - lastJumpTime > jumpCooldown && jumpsRemaining > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
            jumpsRemaining--;
            lastJumpTime = Time.time;
        }

        // �ӡѴ�������ǵ�
        if (rb.velocity.y < maxFallSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, maxFallSpeed, 0);
        }

        // ���絡�ùѺ���ⴴ������������� (��Ǩ�ͺ�ҡ��������)
        if (rb.velocity.y == 0)
        {
            jumpsRemaining = maxJumps;
        }

        isJumpPressed = false;
    }
}