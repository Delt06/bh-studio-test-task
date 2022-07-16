using System.Collections.Generic;
using UnityEngine;

public class ExtraImpulse : MonoBehaviour
{
    private readonly Dictionary<object, Vector3> _impulses = new Dictionary<object, Vector3>();

    public void SetImpulse(object owner, Vector3 impulse)
    {
        _impulses[owner] = impulse;
    }

    public void RemoveImpulse(object owner) => _impulses.Remove(owner);

    public Vector3 ComputeResultingImpulse()
    {
        var result = Vector3.zero;

        foreach (var impulse in _impulses.Values)
        {
            result += impulse;
        }

        return result;
    }
}