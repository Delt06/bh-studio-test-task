using System;
using Mirror;
using Names;
using Networking;
using ScoreSystem;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(NetworkIdentity))]
    public class Player : NetworkBehaviour
    {
        [SerializeField] private Score _score;

        [SyncVar(hook = nameof(OnNameChanged))]
        private string _name = NameUtils.DefaultName;

        private NetworkIdentity _networkIdentity;

        public Score Score => _score;
        public bool IsLocalPlayer => _networkIdentity.isLocalPlayer;

        public NetworkConnectionToClient ConnectionToClient => _networkIdentity.connectionToClient;

        private void Start()
        {
            _networkIdentity = GetComponent<NetworkIdentity>();
            var gameNetworkManager = GameNetworkManager.Instance;
            gameNetworkManager.OnSpawnedPlayer(this);
        }

        private void OnDestroy()
        {
            var gameNetworkManager = GameNetworkManager.Instance;
            if (gameNetworkManager != null)
                gameNetworkManager.OnDespawnedPlayer(this);
        }

        [Server]
        public void SetName(string newName)
        {
            _name = newName;
            NameChanged?.Invoke();
        }

        public void OnNameChanged(string oldName, string newName)
        {
            if (oldName == newName) return;
            _name = newName;
            NameChanged?.Invoke();
        }

        public event Action NameChanged;

        public string GetName() => _name;
    }
}