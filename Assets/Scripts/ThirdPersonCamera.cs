using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Vector3 _initialOffset;
    [SerializeField] private float _yawSensitivity = 1f;
    [SerializeField] private float _pitchSensitivity = 1f;
    [SerializeField] [Range(-90f, 90f)] private float _pitchMinAngle = -60f;
    [SerializeField] [Range(-90f, 90f)] private float _pitchMaxAngle = 60f;

    private Vector3 _offsetFromTarget;
    [CanBeNull]
    private Transform _target;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void LateUpdate()
    {
        if (_target == null) return;

        HandleInput();
        ApplyLimits();

        Assert.IsNotNull(_target);
        _transform.position = _target.position + _offsetFromTarget;
        _transform.forward = -_offsetFromTarget.normalized;
    }

    private void HandleInput()
    {
        var xInput = Input.GetAxis("Mouse X");
        _offsetFromTarget = Quaternion.AngleAxis(xInput * _yawSensitivity, Vector3.up) * _offsetFromTarget;

        var yInput = Input.GetAxis("Mouse Y");
        var flatForward = -_offsetFromTarget.normalized;
        flatForward.y = 0f;
        flatForward.Normalize();
        var yInputAxis = Vector3.Cross(flatForward, Vector3.up);
        _offsetFromTarget = Quaternion.AngleAxis(yInput * _pitchSensitivity, yInputAxis) * _offsetFromTarget;
    }

    private void ApplyLimits()
    {
        var direction = _offsetFromTarget.normalized;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        var eulerAngles = rotation.eulerAngles;
        eulerAngles.x = Mathf.Clamp(ToSymmetricRange(eulerAngles.x), _pitchMinAngle, _pitchMaxAngle);
        var newRotation = Quaternion.Euler(eulerAngles);
        _offsetFromTarget = newRotation * Vector3.forward * _offsetFromTarget.magnitude;
    }

    private static float ToSymmetricRange(float angle)
    {
        while (angle > 180f)
        {
            angle -= 360f;
        }

        while (angle < -180f)
        {
            angle += 360f;
        }

        return angle;
    }

    public void BindTo(Transform target)
    {
        _target = target;
        _offsetFromTarget = _initialOffset;
    }
}