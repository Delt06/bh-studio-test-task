using Mirror;
using UnityEngine;

[RequireComponent(typeof(IInputProvider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ExtraImpulse))]
public class Movement : NetworkBehaviour
{
    [SerializeField] [Min(0f)] private float _speed = 1f;
    [SerializeField] [Min(0f)] private float _directionThreshold = 0.1f;

    private Vector3 _direction;
    private ExtraImpulse _extraImpulse;
    [SyncVar]
    private Vector3 _impulse;

    private IInputProvider _inputProvider;
    private Rigidbody _rigidbody;

    public Vector3 LastDirection
    {
        get
        {
            if (_direction.sqrMagnitude < _directionThreshold * _directionThreshold)
                return _rigidbody.rotation * Vector3.forward;
            return _direction;
        }
    }

    private void Awake()
    {
        _inputProvider = GetComponent<IInputProvider>();
        _rigidbody = GetComponent<Rigidbody>();
        _extraImpulse = GetComponent<ExtraImpulse>();
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            var direction = _inputProvider.InputDirection;
            _direction = direction;
            SetDirection(direction);
        }

        var newVelocity = _direction * _speed;
        newVelocity.y = _rigidbody.velocity.y;
        if (isServer)
            _impulse = _extraImpulse.ComputeResultingImpulse();
        newVelocity += _impulse;
        _rigidbody.velocity = newVelocity;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        _rigidbody.isKinematic = false;
    }

    [Command]
    private void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }
}