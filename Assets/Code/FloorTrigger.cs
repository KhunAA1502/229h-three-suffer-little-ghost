using UnityEngine;
using TMPro;

public class FloorTrigger : MonoBehaviour
{
    [Header("UI Settings")]
    public GameObject triggerUI; // UI ที่จะแสดง
    public float displayTime = 3f; // เวลาแสดงผล (วินาที)
    public string displayText = "พบพื้นที่พิเศษ!"; // ข้อความที่จะแสดง

    [Header("Sound Effects")]
    public AudioClip triggerSound;

    private TextMeshProUGUI uiText;
    private bool hasTriggered = false;

    private void Start()
    {
        // ซ่อน UI ตอนเริ่มเกม
        if (triggerUI != null)
        {
            triggerUI.SetActive(false);
            uiText = triggerUI.GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            ShowUI();
        }
    }

    private void ShowUI()
    {
        // ตั้งค่าข้อความ
        if (uiText != null)
        {
            uiText.text = displayText;
        }

        // แสดง UI
        if (triggerUI != null)
        {
            triggerUI.SetActive(true);
            
        }

        // เล่นเสียง
        if (triggerSound != null)
        {
            AudioSource.PlayClipAtPoint(triggerSound, transform.position);
        }
    }

    private void HideUI()
    {
        if (triggerUI != null)
        {
            triggerUI.SetActive(false);
        }
        
    }
}