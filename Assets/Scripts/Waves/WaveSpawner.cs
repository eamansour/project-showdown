using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnState { SPAWNING, WAITING, COUNTING };

public class WaveSpawner : MonoBehaviour
{
    public static event Action OnWavesComplete;
    public static event Action OnWaveComplete;
    public static event Action<int> OnReward;
    public static event Action OnWaveStart;
    public static SpawnState state { get; private set; }

    public Wave[] waves;
    public int currentWave { get; private set; } = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    public float waveCountdown { get; private set; }

    public GameObject coinChest;
    private List<Transform> spawnedEnemies = new List<Transform>();

    private void Start()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
        SoundManager.StopSound("IncomingMusic");
    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (spawnedEnemies.TrueForAll((Transform t) => t == null))
            {
                StartCoroutine(WaveCompleted());
            }
            return;
        }

        if (waveCountdown <= 0 && state != SpawnState.SPAWNING && currentWave < waves.Length)
        {
            StartCoroutine(SpawnWave(waves[currentWave]));
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    // Marks a wave as having been completed
    private IEnumerator WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves + Time.deltaTime;
        Instantiate(coinChest, coinChest.transform.position, coinChest.transform.rotation);
        yield return Time.deltaTime;

        SoundManager.StopSound("IncomingMusic");
        SoundManager.PlaySound("Music");


        if (currentWave + 1 >= waves.Length)
        {
            // Publish all waves complete
            if (OnWavesComplete != null)
            {
                OnWavesComplete();
            }
        }
        else
        {
            // Publish wave completion
            if (OnWaveComplete != null)
            {
                OnWaveComplete();
            }

            // Publish wave reward
            if (OnReward != null)
            {
                OnReward(waves[currentWave].reward);
            }
            currentWave++;
        }
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        if (OnWaveStart != null)
        {
            OnWaveStart();
        }

        SoundManager.StopSound("Music");
        SoundManager.PlaySound("IncomingMusic");

        state = SpawnState.SPAWNING;

        // Spawn appropriate amount of enemies
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }
        state = SpawnState.WAITING;
    }

    // Spawn an enemy at a random location
    private void SpawnEnemy(Transform enemy)
    {
        Transform sampledSpawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
        Transform clone = Instantiate(enemy, sampledSpawnPoint.position, sampledSpawnPoint.rotation);
        clone.name = enemy.name;
        spawnedEnemies.Add(clone);
    }
}