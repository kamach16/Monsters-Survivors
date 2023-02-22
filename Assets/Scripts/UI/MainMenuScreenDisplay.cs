using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScreenDisplay : MonoBehaviour
{
    [SerializeField] private GameObject scoreDisplayDuringPlaying;

    private void OnDisable()
    {
        scoreDisplayDuringPlaying.SetActive(true);
    }
}
