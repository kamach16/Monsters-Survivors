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
        Invoke("UpdateMaxScore", 0.001f);
    }

    public void UpdateMaxScore()
    {
        maxScoreText.text = scoreManager.GetMaxScore().ToString();
    }
}
