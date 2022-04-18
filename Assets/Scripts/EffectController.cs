using UnityEngine;

public class EffectController : MonoBehaviour
{
    [SerializeField]
    private GameObject effectPrefab;

    [SerializeField]
    private bool playOnSpawn = false;

    private void Start()
    {
        if (playOnSpawn)
        {
            PlayEffect();
        }
    }

    public void PlayEffect()
    {
        GameObject effect = Instantiate(effectPrefab, transform.position, transform.rotation);     
    }
}
