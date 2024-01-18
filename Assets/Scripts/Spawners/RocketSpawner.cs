using System.Collections.Generic;
using UnityEngine;

public class RocketSpawner : MonoBehaviour
{
    [SerializeField] private Rocket _rocketTemplate;
    
    private Pool<Rocket> _pool;

    public void TrySpawnRocket(Vector3 position, Quaternion quaternion, Vector3 direction, out Rocket rocket)
    {
        if (_pool.TrySpawnObject(position, out rocket))
        {
            rocket.Initialize(direction, quaternion);
        }
    }
    
    public void InitializePool(int poolCapacity)
    {
        _pool?.Clear();
        
        List<Rocket> rockets = new List<Rocket>();

        for (int i = 0; i < poolCapacity; i++)
        {
            rockets.Add(Instantiate(_rocketTemplate));
        }
        
        _pool = new Pool<Rocket>(rockets);
    }
}