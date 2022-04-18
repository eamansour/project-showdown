using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;
    
    [SerializeField]
    private GameObject pauseMenuUI;

	private void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused == true)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
	}

    // Resumes the game
    public void ResumeGame()
    {
        SoundManager.PlaySound("ButtonPress");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    // Pauses the game
    public void PauseGame()
    {
        SoundManager.PlaySound("ButtonPress");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    // Returns the user to the main menu
    public void GoToMenu()
    {
        SoundManager.PlaySound("ButtonPress");
        Time.timeScale = 1f;
        IsPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    // Restarts the current level
    public void RestartGame()
    {
        SoundManager.PlaySound("ButtonPress");
        Time.timeScale = 1f;
        IsPaused = false;
        SceneManager.LoadScene("MainGame");
    }

    // Plays a hover sound when the mouse pointer is over a button
    public void OnMouseOver()
    {
        SoundManager.PlaySound("ButtonHover");
    }
}