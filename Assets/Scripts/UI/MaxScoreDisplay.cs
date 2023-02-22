using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MaxScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI maxScoreText;
    [SerializeField] private ScoreManager scoreManager;

    private void OnEnable()
    {
        UpdateMaxScore(scoreManager.GetMaxScore());
    }

    public void UpdateMaxScore(float maxScore) // execute when enemy is killed, score value is changing
    {
        maxScoreText.text = maxScore.ToString();
    }
}
