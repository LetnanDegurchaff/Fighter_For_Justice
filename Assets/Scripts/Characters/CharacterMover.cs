using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _acceleration = 4;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector2 direction)
    {
        _rigidbody.velocity = Vector3.Lerp(
            _rigidbody.velocity,
            new Vector3(direction.x, direction.y, 0).normalized * _speed,
            _acceleration * Time.deltaTime);
    }

    protected void SetVelocity(Vector2 velocity)
    {
        _rigidbody.velocity = velocity;
    }
}