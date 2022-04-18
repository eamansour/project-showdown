using UnityEngine.UI;
using UnityEngine;

public class HighScoresUI : MonoBehaviour
{
    public Text[] highScoreText;
    public Text[] highScoreNameText;
    public InputField playerName;
    public Button submitButton;
    private int[] highScoreValues;
    private string[] highScoreNames;

	private void Start()
    {
        highScoreValues = new int[highScoreText.Length];
        highScoreNames = new string[highScoreNameText.Length];

        for (int i = 0; i < highScoreText.Length; i++)
        {
            highScoreValues[i] = PlayerPrefs.GetInt("highScoreValues" + i);
            highScoreNames[i] = PlayerPrefs.GetString("highScoreNames" + i);
        }
        DrawScoreUI();
	}
	
    // Save the high scores leaderboard
    private void SaveHighScores()
    {
        for (int i = 0; i < highScoreText.Length; i++)
        {
            PlayerPrefs.SetInt("highScoreValues" + i, highScoreValues[i]);
            PlayerPrefs.SetString("highScoreNames" + i, highScoreNames[i]);
        }
    }

    // Updates the high score UI
    private void DrawScoreUI()
    {
        for (int i = 0; i < highScoreText.Length; i++)
        {
            highScoreText[i].text = $"{highScoreValues[i]}";
            highScoreNameText[i].text = $"{highScoreNames[i]}";
        }
    }

    // Sorts the high scores list
    public void HighScoreSort(int highScoreValue, string userName)
    {
        if (userName == "") return;
        
        for (int i = 0; i < highScoreText.Length; i++)
        {
            if (highScoreValue > highScoreValues[i])
            {
                for (int j = highScoreText.Length - 1; j > i; j--)
                {
                    highScoreValues[j] = highScoreValues[j - 1];
                    highScoreNames[j] = highScoreNames[j - 1];
                }
                highScoreValues[i] = highScoreValue;

                highScoreNames[i] = userName;
                DrawScoreUI();
                SaveHighScores();
                break;
            }
        }
    }

    // Enables the submit button for the player to enter their details
    public void ReadyForHighScores()
    {
        HighScoreSort(GameManager.score, playerName.text);
        submitButton.gameObject.SetActive(false);
    }
}