using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Health _health;
    private PlayerMover _dashSystem;

    private HealthBar _healthBar;
    private DashBar _dashBar;

    private void Awake()
    {
        _health = _player.GetComponent<Health>();
        _dashSystem = _player.GetComponent<PlayerMover>();

        _healthBar = GetComponentInChildren<HealthBar>();
        _dashBar = GetComponentInChildren<DashBar>();
        _dashBar.Initialization(_dashSystem.MaxDashAmout);
    }

    private void OnEnable()
    {
        _health.ValueChanged += _healthBar.UpdateView;
        _dashSystem.DashAmoutChenged += _dashBar.OnDashAmoutChenged;
    }

    private void OnDisable()
    {
        _health.ValueChanged -= _healthBar.UpdateView;
        _dashSystem.DashAmoutChenged -= _dashBar.OnDashAmoutChenged;
    }
}