using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI hpText;
    
    [Header("Sound Effects")]
    public AudioClip gameOverSound;
    public AudioClip buttonClickSound;

    private void Start()
    {
        // ซ่อน Panel ตอนเริ่มเกม
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    // เรียกเมื่อ HP หมด (สุขภาพเป็น 0)
    public void ShowGameOver(int currentHP)
    {
        // แสดง Panel
        gameOverPanel.SetActive(true);
        
        // อัพเดทข้อความ
        if (hpText != null)
        {
            hpText.text = $"Hp: {currentHP}";
        }
        
        // เล่นเสียง
        if (gameOverSound != null)
        {
            AudioSource.PlayClipAtPoint(gameOverSound, Camera.main.transform.position);
        }
        
        // หยุดเกม (ถ้าต้องการ)
        Time.timeScale = 0f;
    }

    // ปุ่ม Restart
    public void RestartGame()
    {
        PlayButtonSound();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ปุ่ม Main Menu
    public void GoToMainMenu()
    {
        PlayButtonSound();
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private void PlayButtonSound()
    {
        if (buttonClickSound != null)
        {
            AudioSource.PlayClipAtPoint(buttonClickSound, Camera.main.transform.position);
        }
    }
}