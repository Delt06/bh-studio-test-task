using UnityEngine;

[RequireComponent(typeof(IInputProvider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ExtraImpulse))]
public class Movement : MonoBehaviour
{
    [SerializeField] [Min(0f)] private float _speed = 1f;
    private ExtraImpulse _extraImpulse;

    private IInputProvider _inputProvider;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _inputProvider = GetComponent<IInputProvider>();
        _rigidbody = GetComponent<Rigidbody>();
        _extraImpulse = GetComponent<ExtraImpulse>();
    }

    private void Update()
    {
        var newVelocity = _inputProvider.InputDirection * _speed;
        newVelocity.y = _rigidbody.velocity.y;
        newVelocity += _extraImpulse.ComputeResultingImpulse();
        _rigidbody.velocity = newVelocity;
    }
}