using UnityEngine;

public class ShootSystem : MonoBehaviour
{
    private AimTrajectoryRenderer _aimRenderer;
    private AimTargetCalculater _aimTargetCalculater;
    private CharacterRotater _characterRotater;
    private RocketLauncher _launcher;

    private void Awake()
    {
        _aimRenderer = GetComponentInChildren<AimTrajectoryRenderer>();
        _aimTargetCalculater = GetComponent<AimTargetCalculater>();

        _characterRotater = GetComponent<CharacterRotater>();
        _launcher = GetComponentInChildren<RocketLauncher>();
    }

    public void Aim()
    {
        _aimRenderer.StartRendering();
        _aimTargetCalculater.StartRotate(_characterRotater.ChangeTarget);
    }

    public void Aim(Transform target)
    {
        _characterRotater.ChangeTarget(target);
    }

    public void Shoot()
    {
        _aimRenderer.StopRendering();
        _aimTargetCalculater.StopRotate();
        Shoot(_aimTargetCalculater.GetAimDirection());
    }

    public void Shoot(Vector3 direction)
    {
        _characterRotater.SetNeutralRotation();
        _launcher.ShootRocket(direction);
    }
}