using Controls;
using Mirror;
using UnityEngine;

namespace Animations
{
    public class MovementAnimation : NetworkBehaviour
    {
        private static readonly int IsRunningId = Animator.StringToHash("IsRunning");

        [SerializeField] private Animator _animator;
        [SerializeField] private Movement _movement;
        [SerializeField] [Min(0f)] private float _minSpeed = 0.1f;

        [ClientCallback]
        private void Update()
        {
            if (!isLocalPlayer) return;
            var isRunning = _movement.CurrentSpeedXZ >= _minSpeed;
            _animator.SetBool(IsRunningId, isRunning);
        }
    }
}