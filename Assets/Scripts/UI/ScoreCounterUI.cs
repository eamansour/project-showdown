using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Text))]
public class ScoreCounterUI : MonoBehaviour
{
    private Text scoreText;

	private void Awake()
	{
        scoreText = GetComponent<Text>();
	}

	private void Update()
	{
        scoreText.text = "SCORE: " + (GameManager.score).ToString();
	}
}
