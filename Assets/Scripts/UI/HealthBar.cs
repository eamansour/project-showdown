using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private RectTransform healthBarRect;

    [SerializeField]
    private Text healthText;
    private Gradient g;
    private GradientColorKey[] gck;
    private GradientAlphaKey[] gak;
    private Image healthBarColor;

    private void Awake()
    {
        g = new Gradient();
        gak = new GradientAlphaKey[2];
        gck = new GradientColorKey[3];
        healthBarColor = transform.Find("HealthBar").GetComponent<Image>();

        gck[0].color = Color.green;
        gck[0].time = 0.0f;
        gck[1].color = Color.yellow;
        gck[1].time = 0.5f;
        gck[2].color = Color.red;
        gck[2].time = 1f;
        gak[0].alpha = 1.0f;
        gak[0].time = 0.0f;
        gak[1].alpha = 1.0f;
        gak[1].time = 1f;

        g.SetKeys(gck, gak);
    }

    // Ensure the health bar remains facing the right way
    private void Update()
    {
        transform.localEulerAngles = transform.parent.localEulerAngles;
    }

    // Sets the health bar's length according to given current and maximum health values
    public void SetHealth(int current, int max)
    {
        float value = (float)current / max;

        healthBarRect.localScale = new Vector3(value, healthBarRect.localScale.y, healthBarRect.localScale.z);
        healthText.text = current + " / " + max + " HP";
        
        healthBarColor.color = g.Evaluate(1 - value);
    }
}