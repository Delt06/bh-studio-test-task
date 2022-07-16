using UnityEngine;

namespace Controls
{
    public interface IInputProvider
    {
        Vector3 InputDirection { get; }
    }
}