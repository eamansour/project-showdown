using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CoinCounterUI : MonoBehaviour
{
    private Text coinsText;

	private void Awake() 
    {
        coinsText = GetComponent<Text>();
	}
	
	private void Update() 
    {
        coinsText.text = ": " + (CoinCollector.coins).ToString();
	}
}