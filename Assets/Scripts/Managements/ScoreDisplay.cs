using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private Animator animator;

    private void Awake()
    {
        SetVariables();
    }

    private void SetVariables()
    {
        animator = GetComponent<Animator>();
    }

    public void UpdateScore(float currentScore) // execute when enemy is killed, score value is changing
    {
        scoreText.text = currentScore.ToString();
        //animator.SetTrigger("scoreGained");
    }
}
