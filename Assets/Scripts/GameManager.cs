using UnityEngine;

public class GameManager : MonoBehaviour
{   
    [SerializeField]
    private GameObject gameOverUI, gameWinUI;
    public static int score { get; private set; }

    private void Awake()
    {
        Time.timeScale = 1f;
        Health.OnDied += KillCharacter;
        WaveSpawner.OnWavesComplete += GameWin;
    }

    private void OnDestroy()
    {
        Health.OnDied -= KillCharacter;
        WaveSpawner.OnWavesComplete -= GameWin;
    }

    private void KillCharacter(CharacterType characterType)
    {
        switch (characterType)
        {
            case CharacterType.PLAYER:
                GameOver();
                break;
            case CharacterType.FRIENDLY:
                score -= 10;
                break;
            case CharacterType.ENEMY:
                score += 10;
                break;
        }
    }
     
    public void GameOver()
    {
        SoundManager.StopSound("IncomingMusic");
        SoundManager.PlaySound("GameOver");
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
    }

    public void GameWin()
    {
        SoundManager.PlaySound("GameWin");
        score += 200;
        Time.timeScale = 0f;
        gameWinUI.SetActive(true);
    }
}