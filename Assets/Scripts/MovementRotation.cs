using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementRotation : MonoBehaviour
{
    [SerializeField] [Min(0f)] private float _speed = 1f;
    [SerializeField] [Min(0f)] private float _velocityThreshold = 0.1f;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        var direction = _rigidbody.velocity;
        direction.y = 0f;
        if (direction.sqrMagnitude < _velocityThreshold * _velocityThreshold) return;

        var targetRotation = Quaternion.LookRotation(direction);
        _rigidbody.rotation = Damp(_rigidbody.rotation, targetRotation, _speed, Time.deltaTime);
    }

    // Modified from: https://www.rorydriscoll.com/2016/03/07/frame-rate-independent-damping-using-lerp/
    private static Quaternion Damp(Quaternion a, Quaternion b, float lambda, float dt) =>
        Quaternion.Slerp(a, b, 1 - Mathf.Exp(-lambda * dt));
}