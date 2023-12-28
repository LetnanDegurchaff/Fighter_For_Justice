using UnityEngine;

public class Enemy : Character
{
    private void Awake()
    {
        RocketLauncher launcher = GetComponentInChildren<RocketLauncher>();
        launcher.SetRocketTargetType(typeof(Player));
    }
}