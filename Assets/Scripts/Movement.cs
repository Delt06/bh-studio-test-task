using UnityEngine;

[RequireComponent(typeof(IInputProvider))]
public class Movement : MonoBehaviour
{
    [SerializeField] [Min(0f)] private float _speed = 1f;

    private IInputProvider _inputProvider;

    private void Awake()
    {
        _inputProvider = GetComponent<IInputProvider>();
    }

    private void Update()
    {
        var motion = _speed * Time.deltaTime;
        transform.position += _inputProvider.InputDirection * motion;
    }
}