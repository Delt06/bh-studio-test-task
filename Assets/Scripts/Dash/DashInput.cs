using UnityEngine;

namespace Dash
{
    [RequireComponent(typeof(Dash))]
    public class DashInput : MonoBehaviour
    {
        private Dash _dash;

        private void Awake()
        {
            _dash = GetComponent<Dash>();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
                _dash.TryActivate();
        }
    }
}