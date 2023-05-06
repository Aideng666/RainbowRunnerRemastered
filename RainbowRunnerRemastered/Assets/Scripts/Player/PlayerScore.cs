using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] int scoreMultiplier;
    [SerializeField] TextMeshProUGUI scoreText;
    float currentScore = 0;

    public int ScoreMultiplier { get { return scoreMultiplier; } set { scoreMultiplier = value; } }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameStarted)
        {
            AddScore(Time.deltaTime * scoreMultiplier);

            scoreText.text = $"Score: {(int)currentScore}";
        }
    }

    void AddScore(float score)
    {
        currentScore += score;
    }
}
