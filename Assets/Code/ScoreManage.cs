using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton pattern
    
    [Header("UI References")]
    public TMPro.TMP_Text scoreText;
    private int currentScore = 0;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreUI();
        
        // เอฟเฟกต์เมื่อได้แต้ม (Optional)
        StartCoroutine(ScoreEffect());
    }

    private void UpdateScoreUI()
    {
        scoreText.text = $"Souls: {currentScore}";
    }

    private void SaveHighScore()
    {
        PlayerPrefs.Save();
    }

    // เอฟเฟกต์เมื่อได้แต้ม (Optional)
    private IEnumerator ScoreEffect()
    {
        scoreText.transform.localScale = Vector3.one * 1.2f;
        scoreText.color = Color.yellow;
        yield return new WaitForSeconds(0.3f);
        scoreText.transform.localScale = Vector3.one;
        scoreText.color = Color.white;
    }

    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreUI();
    }

}
