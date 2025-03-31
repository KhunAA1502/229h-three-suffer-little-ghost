using UnityEngine;

public class DamageBox3D : MonoBehaviour
{
    [Header("Damage Settings")]
    public int damageAmount = 10;
    public float pushForce = 5f;
    public Vector3 pushDirection = Vector3.up;
    
    [Header("Cooldown")]
    public float damageCooldown = 1f;
    private float lastDamageTime;
    
    [Header("Effects")]
    public ParticleSystem hitEffect;
    public AudioClip hitSound;

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time - lastDamageTime < damageCooldown) return;
        
        if (other.CompareTag("Player"))
        {
            HealthSystem playerHealth = other.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                lastDamageTime = Time.time;
                
                Rigidbody rb = other.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(pushDirection.normalized * pushForce, ForceMode.Impulse);
                }
                
                if (hitEffect != null) Instantiate(hitEffect, transform.position, Quaternion.identity);
                if (hitSound != null) AudioSource.PlayClipAtPoint(hitSound, transform.position);
            }
        }
    }
}