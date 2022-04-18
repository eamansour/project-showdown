using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject minionPrefab;

    [SerializeField]
    private int minionCost = 1;

    private GameObject coinLabel;

    private void Start()
    {
        coinLabel = transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag != "Player") return;
        
        coinLabel.SetActive(true);
    }

    private void Update()
    {
        if (!coinLabel.activeInHierarchy) return;

        if (Input.GetKeyDown(KeyCode.UpArrow) && (CoinCollector.coins - minionCost >= 0))
        {
            GameObject minion = Instantiate(minionPrefab, transform.position, transform.rotation);
            
            CoinCollector.coins -= minionCost;

            SoundManager.PlaySound("MinionSpawn");
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag != "Player") return;

        coinLabel.SetActive(false);
    }
}
