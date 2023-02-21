using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] public float currentScore;
    [SerializeField] private ScoreDisplay scoreDisplay;

    private void Awake()
    {
        scoreDisplay.UpdateScore(0);
    }

    public void GainScore(float scoreToGain)
    {
        currentScore += scoreToGain;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreDisplay.UpdateScore(currentScore);
    }
}
