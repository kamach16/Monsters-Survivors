using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Image fade;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject mainMenuScreen;

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
        ChangeTimeScale(1);
        Fade(false);
    }

    public void PauseGame(GameObject contentToShow)
    {
        ChangeTimeScale(0);
        Fade(true);
        contentToShow.SetActive(true);
    }

    private void ChangeTimeScale(float newTimeScale)
    {
        Time.timeScale = newTimeScale;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        int gameplaySceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(gameplaySceneIndex);
    }

    public void PlayAgain()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    private void Fade(bool isFadingIn)
    {
        fade.gameObject.SetActive(isFadingIn);
    }

    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
        Fade(true);
    }
}
