using System;
using Mirror;

namespace ScoreSystem
{
    public class Score : NetworkBehaviour
    {
        [field: SyncVar(hook = nameof(ValueHook))]

        public int Value { get; set; }

        private void ValueHook(int oldValue, int newValue)
        {
            if (oldValue == newValue) return;
            Value = newValue;
            OnValueChanged();
        }

        private void OnValueChanged()
        {
            ValueChanged?.Invoke();
        }

        public event Action ValueChanged;
    }
}