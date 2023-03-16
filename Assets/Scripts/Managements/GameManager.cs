using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Image fade;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject pauseScreen;

    [HideInInspector] public bool powerUpsContainerShowed = false;

    private bool canPauseGameByESC = true;
    private bool gamePaused = false;

    private void Start()
    {
        SetCursor();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !powerUpsContainerShowed)
        {
            if (canPauseGameByESC) PauseGame(pauseScreen);
            else ResumeGame(pauseScreen);
        }
    }

    public bool GetGamePaused()
    {
        return gamePaused;
    }

    public void SetPowerUpsContainerShowed(bool showed)
    {
        powerUpsContainerShowed = showed;
    }

    private void SetCursor()
    {
        Cursor.SetCursor(cursorTexture, new Vector2(250, 250), CursorMode.Auto);
    }

    public void ResumeGame(GameObject contentToHide)
    {
        ChangeTimeScale(1);
        Fade(false);
        contentToHide.SetActive(false);
        canPauseGameByESC = true;
        gamePaused = false;
    }

    public void PauseGame(GameObject contentToShow)
    {
        if (contentToShow == null) return;
        
        ChangeTimeScale(0);
        Fade(true);
        contentToShow.SetActive(true);
        canPauseGameByESC = false;
        gamePaused = true;
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
        int gameplaySceneIndex = ActiveSceneIndex() + 1;
        SceneManager.LoadScene(gameplaySceneIndex);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(ActiveSceneIndex());
        canPauseGameByESC = true;
    }

    private int ActiveSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    private void Fade(bool isFadingIn)
    {
        fade.gameObject.SetActive(isFadingIn);
    }

    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
        Fade(true);
        canPauseGameByESC = false;
        PlayerPrefs.SetFloat("maxScore", scoreManager.GetMaxScore());
    }
}
