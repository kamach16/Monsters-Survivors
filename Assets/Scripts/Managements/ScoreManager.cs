using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] public float currentScore;
    [SerializeField] public float maxScore;
    [SerializeField] private ScoreDisplay scoreDisplay;

    private void Awake()
    {
        maxScore = PlayerPrefs.GetFloat("maxScore");

        if (scoreDisplay != null) scoreDisplay.UpdateScore(0);
    }

    public float GetCurrentScore()
    {
        return currentScore;
    }

    public float GetMaxScore()
    {
        return maxScore;
    }

    public void GainScore(float scoreToGain)
    {
        currentScore += scoreToGain;

        if (currentScore > maxScore)
        {
            maxScore = currentScore;
        }

        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreDisplay.UpdateScore(currentScore);
    }
}
