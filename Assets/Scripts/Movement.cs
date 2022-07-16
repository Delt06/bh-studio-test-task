using UnityEngine;

[RequireComponent(typeof(IInputProvider))]
[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] [Min(0f)] private float _speed = 1f;

    private IInputProvider _inputProvider;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _inputProvider = GetComponent<IInputProvider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var newVelocity = _inputProvider.InputDirection * _speed;
        newVelocity.y = _rigidbody.velocity.y;
        _rigidbody.velocity = newVelocity;
    }
}