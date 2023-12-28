using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterMover))]
[RequireComponent(typeof(CharacterRotater))]
[RequireComponent(typeof(ShootSystem))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _aimingTime = 1;
    [SerializeField] private float _reloadingTime = 2;

    private CharacterMover _characterMover;
    private CharacterRotater _characterRotater;
    private ShootSystem _shootSystem;

    private Transform _transform;

    private Coroutine _coroutine;
    private WaitForSeconds _aimingTimer;
    private WaitForSeconds _reloadingTimer;

    private void Awake()
    {
        _characterMover = GetComponent<CharacterMover>();
        _characterRotater = GetComponent<CharacterRotater>();
        _shootSystem = GetComponent<ShootSystem>();
        _transform = transform;

        _aimingTimer = new WaitForSeconds(_aimingTime);
        _reloadingTimer = new WaitForSeconds(_reloadingTime);
    }

    private void OnEnable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Shooting());
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private void Start()
    {
        _characterRotater.ChangeTarget(_target);
    }

    private void FixedUpdate()
    {
        _characterMover.Move(_target.position - _transform.position);
    }

    private IEnumerator Shooting()
    {
        yield return null;

        while (true)
        {
            _shootSystem.Aim(_target);
            yield return _aimingTimer;

            _shootSystem.Shoot(_target.position - _transform.position);
            yield return _reloadingTimer;
        }
    }
}