using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    
    [Header("Jump Settings")]
    public float jumpForce = 10f;
    public float jumpCooldown = 0.5f;
    public int maxJumps = 2;
    
    private int jumpsRemaining;
    private float lastJumpTime;
    
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("MyGravitation requires a Rigidbody component!", this);
            enabled = false;
            return;
        }
        jumpsRemaining = maxJumps;
    }

    private void Update()
    {
        // การเคลื่อนที่
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3(moveInput * moveSpeed, rb.velocity.y, 0);
        // ระบบกระโดด
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryJump();
        }
    }

    void FixedUpdate()
    {
        // รีเซ็ตการกระโดดเมื่อความเร็วแนวตั้งใกล้ศูนย์
        if (Mathf.Abs(rb.velocity.y) < 0.1f)
        {
            jumpsRemaining = maxJumps;
        }
    }

    void TryJump()
    {
        // ตรวจสอบคูลดาวน์และจำนวนการกระโดดที่เหลือ
        if (Time.time - lastJumpTime < jumpCooldown || jumpsRemaining <= 0)
            return;
            
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // รีเซ็ตความเร็ว y
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        
        jumpsRemaining--;
        lastJumpTime = Time.time;
    }

   
    }



    


