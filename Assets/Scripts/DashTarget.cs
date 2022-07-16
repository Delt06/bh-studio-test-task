using Mirror;
using UnityEngine;

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
        OnInvincibleStatusChanged();
    }

    private void OnDestroy()
    {
        Destroy(_material);
    }

    public void TryHit()
    {
        if (_isInvincible) return;

        _remainingTime = _invincibilityTime;
        _isInvincible = true;
        OnInvincibleStatusChanged();
    }

    // ReSharper disable UnusedParameter.Local
    private void OnInvincibleStatusChangedHook(bool old, bool @new)
    {
        _isInvincible = @new;
        OnInvincibleStatusChanged();
    }
    // ReSharper restore UnusedParameter.Local

    private void OnInvincibleStatusChanged()
    {
        _material.color = _isInvincible ? _color : _defaultColor;
    }
}