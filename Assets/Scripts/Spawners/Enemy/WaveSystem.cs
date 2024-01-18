using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    
    private List<Vector3> _spawnPositions = new List<Vector3>();
    
    private EnemySpawner _enemySpawner;
    private Queue<Wave> _waves;

    private Transform _transform;

    private void Awake()
    {
        _enemySpawner = GetComponent<EnemySpawner>();
        _transform = transform;
        
        for (int i = 0; i < _spawnPoints.Length; i++)
            _spawnPositions.Add(_spawnPoints[i].position);
    }

    /////////////////////////////////////////////////////////////////////
    private void OnEnable()
    {
        InitWaves(new Queue<Wave>(
            new Wave[]
                {new Wave(6,1000)}));

        int maxEnemyAmount = 1;

        foreach (Wave wave in _waves)
            if (wave.EnemyAmount > maxEnemyAmount)
                maxEnemyAmount = wave.EnemyAmount;
        
        _enemySpawner.InitializePool(Convert.ToInt32(maxEnemyAmount * 0.7f));
        
        StartNewWave();
    }
    /////////////////////////////////////////////////////////////////////

    public void InitWaves(Queue<Wave> waves) => _waves = waves;

    public void StartNewWave()
    {
        StartCoroutine(Spawning(_waves.Dequeue()));
    }

    private IEnumerator Spawning(Wave wave)
    {
        WaitForSeconds spawningDelay = new WaitForSeconds(wave.SpawnDelay);

        for (int i = 0; i < wave.EnemyAmount + 1; i++)
        {
            if (_enemySpawner.TrySpawnEnemy(GetRandomSpawnPoint()) == false)
                i--;
                
            yield return spawningDelay;
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        return _transform.position;
    }
}