using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMaster : MonoBehaviour
{
    private void Start()
    {
        SoundManager.PlaySound("Music");
    }

    public void PlayGame()
    {
        SoundManager.PlaySound("ButtonPress");
        SceneManager.LoadScene("MainGame");
    }

    public void QuitGame()
    {
        SoundManager.PlaySound("ButtonPress");
        Application.Quit();
    }

    public void BackToMenu()
    {
        SoundManager.PlaySound("ButtonPress");
        SceneManager.LoadScene("MainMenu");
    }

    public void HelpScreen()
    {
        SoundManager.PlaySound("ButtonPress");
        SceneManager.LoadScene("HelpScreen");
    }

    public void HighScoreScreen()
    {
        SoundManager.PlaySound("ButtonPress");
        SceneManager.LoadScene("HighScores");
    }

    public void OnMouseOver()
    {
        SoundManager.PlaySound("ButtonHover");
    }
}