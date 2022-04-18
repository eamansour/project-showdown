using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    [field: SerializeField]
    public string name { get; private set; }

    public AudioSource source
    {
        get => _source;
        set
        {
            _source = value;
            _source.outputAudioMixerGroup = mixerGroup;
            _source.clip = clip;
            _source.loop = loop;
        }
    }

    [SerializeField]
    private AudioClip clip;

    [SerializeField]
    private AudioMixerGroup mixerGroup;

    [SerializeField]
    private bool loop = false;

    private AudioSource _source;

    /// <summary>
    /// Play the current sound from its audio source.
    /// </summary>
    public void Play()
    {
        source.Play();
    }

    /// <summary>
    /// Stops the current sound.
    /// </summary>
    public void Stop()
    {
        source.Stop();
    }
}
