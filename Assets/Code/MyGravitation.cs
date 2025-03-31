using System.Collections.Generic;
using UnityEngine;

public class MyGravitation : MonoBehaviour
{
    private Rigidbody rb;
    private const float G = 0.006674f;
    
    [Header("Gravity Settings")]
    public float maxGravityDistance = 100f;
    public float gravityMultiplier = 1f;
    public bool drawGizmos = true;

    [Header("Attraction Settings")]
    public float attractionBoost = 1.0f; // ปรับเพิ่มแรงดูดโดยไม่เกี่ยวกับมวล

    private static List<MyGravitation> activeGravityObjects = new List<MyGravitation>();

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("MyGravitation requires a Rigidbody component!", this);
            enabled = false;
            return;
        }
    }

    void OnEnable()
    {
        if (!activeGravityObjects.Contains(this))
        {
            activeGravityObjects.Add(this);
        }
    }

    void OnDisable()
    {
        activeGravityObjects.Remove(this);
    }

    void FixedUpdate()
    {
        // ใช้ for แทน foreach เพื่อประสิทธิภาพ
        for (int i = 0; i < activeGravityObjects.Count; i++)
        {
            MyGravitation other = activeGravityObjects[i];
            if (other != null && other != this)
            {
                Attract(other);
            }
        }
    }

    void Attract(MyGravitation other)
    {
        if (other == null || other.rb == null) return;

        Rigidbody otherRb = other.rb;
        Vector3 direction = rb.position - otherRb.position;
        float distance = direction.magnitude;

        if (distance <= 0.01f || distance > maxGravityDistance)
            return;

        // คำนวณแรงโน้มถ่วงเดิม
        float forceMagnitude = G * (rb.mass * otherRb.mass) / Mathf.Pow(distance, 2);

        // เพิ่มแรงดูดด้วย attractionBoost
        forceMagnitude *= gravityMultiplier * attractionBoost;

        Vector3 gravityForce = forceMagnitude * direction.normalized;
        otherRb.AddForce(gravityForce, ForceMode.Force);
    }


    void OnDestroy()
    {
        activeGravityObjects.Remove(this);
    }

    void OnDrawGizmosSelected()
    {
        if (!drawGizmos) return;
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxGravityDistance);
    }
}
