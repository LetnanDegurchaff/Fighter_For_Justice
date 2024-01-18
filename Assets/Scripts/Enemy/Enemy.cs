using UnityEngine;

public class Enemy : Character
{
    private void Awake()
    {
        GetComponentInChildren<RocketLauncher>()
            .SetRocket(Resources.Load<Rocket>("Prefabs/EnemyRocket"));
    }
}