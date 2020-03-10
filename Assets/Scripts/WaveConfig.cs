using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5F;
    [SerializeField] float spawnRandomFactor = 0.3F;
    [SerializeField] int numberOfEnemies = 10;
    [SerializeField] float moveSpd = 2F;

    public GameObject getEnemyPrefab()
    {
        return enemyPrefab;
    }
    public List<Transform> getWaypoints()
    {
        var waveWayPoints = new List<Transform>();
        foreach(Transform child in pathPrefab.transform)
        {
            waveWayPoints.Add(child);
        }
        return waveWayPoints;
    }
    public float getTimeBetweenSpawns()
    {
        return timeBetweenSpawns;
    }
    public float getSpawnRandomFactor()
    {
        return spawnRandomFactor;
    }
    public int getNumberOfEnemies()
    {
        return numberOfEnemies;
    }
    public float getMoveSpd()
    {
        return moveSpd;
    }
}

