using Mirror;
using ScoreSystem;
using UnityEngine;

namespace Dash
{
    [RequireComponent(typeof(Dash))]
    public class DashCollisionHandler : NetworkBehaviour
    {
        [SerializeField] private Score _score;

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

            if (dashTarget.TryHit())
                _score.Value++;
        }
    }
}