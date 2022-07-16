using Mirror;
using UnityEngine;

[RequireComponent(typeof(Dash))]
public class DashCollisionHandler : NetworkBehaviour
{
    private Dash _dash;

    private void Awake()
    {
        _dash = GetComponent<Dash>();
    }

    [ServerCallback]
    private void OnCollisionEnter(Collision collision)
    {
        if (!_dash.IsActivated) return;
        if (collision.rigidbody == null) return;
        if (!collision.rigidbody.TryGetComponent(out DashTarget dashTarget)) return;

        dashTarget.TryHit();
    }
}