using Mirror;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(ExtraImpulse))]
public class Dash : NetworkBehaviour
{
    [SerializeField] [Min(0f)] private float _distance = 1f;
    [SerializeField] [Min(0f)] private float _duration = 1f;

    private ExtraImpulse _extraImpulse;
    private Movement _movement;
    private float _remainingTime;

    [field: SyncVar]
    public bool IsActivated { get; private set; }

    [ServerCallback]
    private void Awake()
    {
        _extraImpulse = GetComponent<ExtraImpulse>();
        _movement = GetComponent<Movement>();
    }

    [ServerCallback]
    private void Update()
    {
        if (!IsActivated) return;

        _remainingTime -= Time.deltaTime;
        if (_remainingTime > 0f) return;

        IsActivated = false;
        _extraImpulse.RemoveImpulse(this);
    }

    [ServerCallback]
    private void OnDestroy()
    {
        _extraImpulse.RemoveImpulse(this);
    }

    [Command]
    public void TryActivate()
    {
        if (IsActivated) return;
        _remainingTime = _duration;
        IsActivated = true;
        var impulse = GetImpulse();
        _extraImpulse.SetImpulse(this, impulse);
    }

    private Vector3 GetImpulse()
    {
        var direction = _movement.LastDirection.normalized;
        return direction * _distance / _duration;
    }
}