using UnityEngine;

public class FloorMove : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float moveDistance = 3f;
    public bool moveHorizontal = true; // เลื่อนแนวนอนหรือแนวตั้ง

    private Vector3 startPosition;
    private float direction = 1;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        // คำนวณตำแหน่งใหม่
        float movement = moveSpeed * Time.deltaTime * direction;

        if (moveHorizontal)
        {
            transform.Translate(movement, 0, 0);
            // เปลี่ยนทิศทางเมื่อถึงจุดสิ้นสุด
            if (transform.position.x > startPosition.x + moveDistance ||
                transform.position.x < startPosition.x - moveDistance)
            {
                direction *= -1;
            }
        }
        else
        {
            transform.Translate(0, movement, 0);
            // เปลี่ยนทิศทางเมื่อถึงจุดสิ้นสุด
            if (transform.position.y > startPosition.y + moveDistance ||
                transform.position.y < startPosition.y - moveDistance)
            {
                direction *= -1;
            }
        }
    }

    // เมื่อวัตถุอื่นชนกับแผ่นเลื่อน
    private void OnCollisionEnter(Collision collision)
    {
        // ทำให้วัตถุที่ชนเป็นลูกของแผ่นเลื่อน
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // ยกเลิกการเป็นลูกเมื่อออกจากแผ่นเลื่อน
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
