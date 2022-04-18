using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public static int coins { get; set; }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Coin") return;
        
        Destroy(col.gameObject);
        coins++;
        SoundManager.PlaySound("Coin");
    }
}
