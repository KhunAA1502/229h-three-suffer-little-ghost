using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("UI Settings")]
    public Button startButton; // ปุ่ม Start ที่จะเชื่อมใน Inspector
    public string nextSceneName = "GameScene"; // ชื่อ Scene ต่อไป
    
    [Header("Effects")]
    public AudioClip clickSound; // เสียงเมื่อคลิกปุ่ม
    public float sceneChangeDelay = 0.5f; // เวลาหน่วงก่อนเปลี่ยน Scene

    private void Start()
    {
        // เชื่อมฟังก์ชันกับปุ่ม Start
        if (startButton != null)
        {
            startButton.onClick.AddListener(StartGame);
        }
        else
        {
            Debug.LogError("Start Button is not assigned in the Inspector!");
        }
    }

    public void StartGame()
    {
        // เล่นเสียงเมื่อมี
        if (clickSound != null)
        {
            AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
        }
        
        // หน่วงเวลาก่อนเปลี่ยน Scene ถ้าต้องการ
        Invoke("LoadNextScene", sceneChangeDelay);
        
        // ปิดปุ่มชั่วคราวเพื่อป้องกันการคลิกซ้ำ
        startButton.interactable = false;
    }

    private void LoadNextScene()
    {
        // ตรวจสอบว่ามี Scene นี้อยู่หรือไม่
        if (Application.CanStreamedLevelBeLoaded(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Scene '" + nextSceneName + "' does not exist!");
            startButton.interactable = true; // เปิดปุ่มกลับถ้าโหลดไม่ได้
        }
    }
}