using UnityEngine;

public class Player : Character
{
    private void Awake()
    {
        GetComponentInChildren<RocketLauncher>()
            .SetRocket(Resources.Load<Rocket>("Prefabs/PlayerRocket"));
    }
}