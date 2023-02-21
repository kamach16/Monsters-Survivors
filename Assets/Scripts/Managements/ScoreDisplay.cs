using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Animator animator;

    public void UpdateScore(float currentScore) // execute when enemy is killed, score value is changing
    {
        scoreText.text = currentScore.ToString();
        animator.SetTrigger("scoreGained");
    }
}
