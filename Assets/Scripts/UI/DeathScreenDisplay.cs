using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathScreenDisplay : MonoBehaviour
{
    [SerializeField] private GameObject scoreDisplayDuringPlaying;
    [SerializeField] private TextMeshProUGUI scoreDisplayInDeathScreen;
    [SerializeField] private ScoreManager scoreManager;

    private void OnEnable()
    {
        scoreDisplayDuringPlaying.SetActive(false);
        scoreDisplayInDeathScreen.text = scoreManager.GetCurrentScore().ToString();
    }
}
