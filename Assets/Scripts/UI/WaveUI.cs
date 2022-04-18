using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    [SerializeField]
    private WaveSpawner spawner;

    [SerializeField]
    private Animator waveAnimator;

    [SerializeField]
    private Text waveCountdownText;

    [SerializeField]
    private Text waveCountText;

    private void Awake()
    {
        WaveSpawner.OnWaveComplete += SetCountingUI;
        WaveSpawner.OnWaveStart += SetSpawningUI;
        SetCountingUI();
    }

    private void FixedUpdate()
    {
        waveCountdownText.text = $"{(int)spawner.waveCountdown}";
        waveCountText.text = $"{spawner.currentWave + 1}";
    }

    private void OnDestroy()
    {
        WaveSpawner.OnWaveComplete -= SetCountingUI;
        WaveSpawner.OnWaveStart -= SetSpawningUI;
    }

    private void SetCountingUI()
    {
        waveAnimator.SetBool("WaveIncoming", false);
        waveAnimator.SetBool("WaveCountdown", true);
    }

    private void SetSpawningUI()
    {
        waveAnimator.SetBool("WaveCountdown", false);
        waveAnimator.SetBool("WaveIncoming", true);
    }
}