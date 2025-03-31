using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;
    
    [Header("UI References")]
    public Slider healthSlider;
    public Color fullHealthColor = Color.green;
    public Color lowHealthColor = Color.red;
    
    [Header("Events")]
    public UnityEvent onDeath;
    
    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
        
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    
    private void UpdateHealthUI()
    {
        healthSlider.value = (float)currentHealth / maxHealth;
    }
    
    private void Die()
    {
        Destroy(gameObject);
        onDeath.Invoke();
        
        // เรียก Game Over
        GameOverManager gameOver = FindObjectOfType<GameOverManager>();
        if (gameOver != null)
        {
            gameOver.ShowGameOver(currentHealth);
        }
        
        // ปิดการทำงานของตัวละคร
        // GetComponent<PlayerController>().enabled = false;
    }
}