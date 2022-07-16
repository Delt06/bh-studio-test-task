using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ExtraImpulse))]
public class Dash : MonoBehaviour
{
    [SerializeField] [Min(0f)] private float _distance = 1f;
    [SerializeField] [Min(0f)] private float _duration = 1f;

    private ExtraImpulse _extraImpulse;
    private float? _remainingTime;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _extraImpulse = GetComponent<ExtraImpulse>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_remainingTime == null) return;

        _remainingTime -= Time.deltaTime;
        if (_remainingTime > 0f) return;

        _remainingTime = null;
        _extraImpulse.RemoveImpulse(this);
    }

    private void OnDestroy()
    {
        _extraImpulse.RemoveImpulse(this);
    }

    public void TryActivate()
    {
        if (_remainingTime != null) return;
        _remainingTime = _duration;

        var impulse = GetImpulse();
        _extraImpulse.SetImpulse(this, impulse);
    }

    private Vector3 GetImpulse()
    {
        var direction = _rigidbody.rotation * Vector3.forward;
        return direction * _distance / _duration;
    }
}