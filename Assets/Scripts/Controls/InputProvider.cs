using UnityEngine;

namespace Controls
{
    public class InputProvider : MonoBehaviour, IInputProvider
    {
        private Transform _cameraTransform;
        private bool _enabled;

        public Vector3 InputDirection
        {
            get
            {
                if (!_enabled) return Vector3.zero;

                var forward = Flatten(_cameraTransform.forward);
                var right = Flatten(_cameraTransform.right);

                var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                input = Vector2.ClampMagnitude(input, 1);

                return forward * input.y + right * input.x;
            }
        }

        private static Vector3 Flatten(Vector3 direction)
        {
            direction.y = 0f;
            direction.Normalize();
            return direction;
        }

        public void Init(Camera cam)
        {
            _cameraTransform = cam.transform;
            _enabled = true;
        }
    }
}