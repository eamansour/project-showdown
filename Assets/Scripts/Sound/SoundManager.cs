using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    private static List<Sound> s_sounds = new List<Sound>();

    [SerializeField]
    private List<Sound> sounds = new List<Sound>();

    private void Awake()
    {
        // Persist a single instance throughout the game
        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        // Create a hierarchy of GameObjects with audio sources attached
        s_sounds = sounds;
        for (int i = 0; i < s_sounds.Count; i++)
        {
            GameObject go = new GameObject($"Sound{i}_{s_sounds[i].name}");
            go.transform.SetParent(transform);
            s_sounds[i].source = go.AddComponent<AudioSource>();
        }
    }

    /// <summary>
    /// Plays a sound, given by its name.
    /// </summary>
    public static void PlaySound(string name)
    {
        for (int i = 0; i < s_sounds.Count; i++)
        {
            if (s_sounds[i].name == name)
            {
                s_sounds[i].Play();
                return;
            }
        }
    }

    /// <summary>
    /// Stop a sound, given by its name.
    /// </summary>
    public static void StopSound(string name)
    {
        for (int i = 0; i < s_sounds.Count; i++)
        {
            if (s_sounds[i].name == name)
            {
                s_sounds[i].Stop();
                return;
            }
        }
    }

    /// <summary>
    /// Gets a given sound's audio source if the sound exists, null otherwise.
    /// </summary>
    public static AudioSource GetSource(string name)
    {
        for (int i = 0; i < s_sounds.Count; i++)
        {
            if (s_sounds[i].name == name)
            {
                return s_sounds[i].source;
            }
        }
        return null;
    }
}