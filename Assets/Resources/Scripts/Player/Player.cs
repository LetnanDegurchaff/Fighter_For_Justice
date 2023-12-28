using UnityEngine;

public class Player : Character
{
    private void Awake()
    {
        RocketLauncher launcher = GetComponentInChildren<RocketLauncher>();
        launcher.SetRocketTargetType(typeof(Enemy));
    }
}