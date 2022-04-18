using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class CoinChest : MonoBehaviour
{
    public GameObject coinPrefab;
    private Animator anim;
    private EffectController effect;
    private bool readyToSpawn = true;
    private int coinsToSpawn = 5;
    private Collider2D coldr;

    private void Start()
    {
        effect = GetComponent<EffectController>();
        WaveSpawner.OnReward += SetCoinAmount;
        anim = GetComponent<Animator>();
        coldr = GetComponent<Collider2D>();
	}

    private void OnDestroy()
    {
        WaveSpawner.OnReward -= SetCoinAmount;
    }

    private IEnumerator SpawnCoins(int amount)
    {
        anim.SetBool("OpenChest", true);

        // Spawns coin objects every 0.2 seconds until the appropriate number of coins have been dropped
        for (int i = 0; i < coinsToSpawn; i++)
        {
            int randomX = Random.Range(-8, 8);
            int randomY = Random.Range(0, 12);

            GameObject coin = Instantiate(coinPrefab, transform.position, transform.rotation);
            coin.GetComponent<Rigidbody2D>().velocity = new Vector3(randomX, randomY, 0);

            yield return new WaitForSeconds(0.2f);
        }
        effect.PlayEffect();
        Destroy(gameObject);
    }

    private void SetCoinAmount(int amount)
    {
        coinsToSpawn = amount;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        
        // Spawns coins when the player collides with the chest
        if (col.gameObject.tag == "Player" && readyToSpawn)
        {
            StartCoroutine(SpawnCoins(coinsToSpawn));
            readyToSpawn = false;
        }
        else if (col.gameObject.layer == LayerMask.NameToLayer("Friendly"))
        {
            Physics2D.IgnoreCollision(coldr, col.collider);
        }
    }
}