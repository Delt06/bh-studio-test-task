using Mirror;
using UnityEngine;

namespace Dash
{
    public class DashTarget : NetworkBehaviour
    {
        [SerializeField] [Min(0f)] private float _invincibilityTime = 1f;
        [SerializeField] private Color _color = Color.red;
        [SerializeField] private Renderer _renderer;
        private Color _defaultColor;

        [SyncVar(hook = nameof(OnInvincibleStatusChangedHook))]
        private bool _isInvincible;

        private Material _material;
        private float _remainingTime;

        private void Awake()
        {
            _material = _renderer.material;
            _defaultColor = _material.color;
        }

        [ServerCallback]
        private void Update()
        {
            if (!_isInvincible) return;

            _remainingTime -= Time.deltaTime;
            if (_remainingTime >= 0) return;
            _isInvincible = false;
        }

        private void OnDestroy()
        {
            Destroy(_material);
        }

        public bool TryHit()
        {
            if (_isInvincible) return false;

            _remainingTime = _invincibilityTime;
            _isInvincible = true;
            return true;
        }

        // ReSharper disable UnusedParameter.Local
        private void OnInvincibleStatusChangedHook(bool old, bool @new)
        {
            _isInvincible = @new;
            _material.color = _isInvincible ? _color : _defaultColor;
        }
        // ReSharper restore UnusedParameter.Local
    }
}