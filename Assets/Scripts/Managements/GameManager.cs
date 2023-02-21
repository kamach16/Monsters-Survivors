using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Image fade;
    [SerializeField] private GameObject deathScreen;

    private void Start()
    {
        SetCursor();
    }

    private void SetCursor()
    {
        Cursor.SetCursor(cursorTexture, new Vector2(250, 250), CursorMode.Auto);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        fade.gameObject.SetActive(false);
    }

    public void StopGame()
    {
        Time.timeScale = 0;
        fade.gameObject.SetActive(true);
    }

    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
    }
}
