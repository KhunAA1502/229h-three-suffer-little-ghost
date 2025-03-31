using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [Header("Score Settings")]
    public int scoreValue = 10;          // แต้มที่ได้เมื่อเก็บ
    public bool destroyOnCollect = true; // ทำลายวัตถุเมื่อเก็บหรือไม่

    [Header("Visual Effects")]
    public ParticleSystem collectEffect; // เอฟเฟกต์เมื่อเก็บ
    public GameObject model;            // ส่วนแสดงผล (จะหายไปเมื่อเก็บ)

    [Header("Sound Effects")]
    public AudioClip collectSound;      // เสียงเมื่อเก็บ
    public float soundVolume = 1f;      // ความดังเสียง

    [Header("Animation")]
    public float rotateSpeed = 50f;     // ความเร็วการหมุน
    public float floatAmplitude = 0.5f; // ความสูงการลอยขึ้นลง
    public float floatFrequency = 1f;   // ความถี่การลอยขึ้นลง

    private Vector3 startPosition;
    private Collider itemCollider;

    private void Start()
    {
        startPosition = transform.position;
        itemCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        // เอฟเฟกต์การหมุน
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);

        // เอฟเฟกต์การลอยขึ้นลง
        if (floatAmplitude > 0)
        {
            Vector3 tempPos = startPosition;
            tempPos.y += Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
            transform.position = tempPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectItem();
        }
    }

    private void CollectItem()
    {
        // เพิ่มคะแนน
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(scoreValue);
        }

        // เล่นเอฟเฟกต์
        if (collectEffect != null)
        {
            Instantiate(collectEffect, transform.position, collectEffect.transform.rotation);
        }

        // เล่นเสียง
        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position, soundVolume);
        }

        // ซ่อนวัตถุ
        if (model != null)
        {
            model.SetActive(false);
        }

        // ปิด Collider
        if (itemCollider != null)
        {
            itemCollider.enabled = false;
        }

        // ทำลายวัตถุ
        if (destroyOnCollect)
        {
            Destroy(gameObject, collectSound != null ? collectSound.length : 0.1f);
        }
        else
        {
            // หรือเรียกฟังก์ชันรีเซ็ตถ้าไม่ทำลาย
            Invoke("ResetItem", 3f); // รีเซ็ตหลังจาก 3 วินาที
        }
    }

    private void ResetItem()
    {
        if (model != null) model.SetActive(true);
        if (itemCollider != null) itemCollider.enabled = true;
    }
}
