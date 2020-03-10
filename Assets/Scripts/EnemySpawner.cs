﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    int startingWave = 0;
    // Start is called before the first frame update
    void Start()
    {
        var currentWave = waveConfigs[startingWave];
        StartCoroutine(SpawnAllEnemiesInWave(currentWave));
    }
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int i = 0; i < waveConfig.getNumberOfEnemies(); i++)
        {
            Instantiate(waveConfig.getEnemyPrefab(), waveConfig.getWaypoints()[0].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(waveConfig.getTimeBetweenSpawns());
        }
    }
}
