using System;
using Mirror;

namespace Names
{
    public class PlayerName : NetworkBehaviour
    {
        [SyncVar(hook = nameof(OnNameChanged))]
        private string _name = NameUtils.DefaultName;

        [Server]
        public void Set(string newName)
        {
            _name = newName;
            Changed?.Invoke();
        }

        private void OnNameChanged(string oldName, string newName)
        {
            if (oldName == newName) return;
            _name = newName;
            Changed?.Invoke();
        }

        public event Action Changed;

        public string Get() => _name;
    }
}