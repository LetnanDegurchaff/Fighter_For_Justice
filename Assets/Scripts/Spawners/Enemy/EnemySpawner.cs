using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyController _enemyTemplate;
    [SerializeField] private Transform _playerTransform;
    
    private Pool<EnemyController> _pool;

    public bool TrySpawnEnemy(Vector3 position) =>
        _pool.TrySpawnObject(position, out EnemyController _);
    
    public void InitializePool(int poolCapacity)
    {
        _pool?.Clear();
        
        List<EnemyController> enemies = new List<EnemyController>();

        for (int i = 0; i < poolCapacity; i++)
        {
            enemies.Add(Instantiate(_enemyTemplate));
            enemies[i].Initialize(_playerTransform);
        }
        
        _pool = new Pool<EnemyController>(enemies);
    }
}