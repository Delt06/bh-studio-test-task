using Mirror;
using Names;
using Networking;
using ScoreSystem;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(NetworkIdentity))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private Score _score;
        [SerializeField] private PlayerName _name;

        private NetworkIdentity _networkIdentity;

        public Score Score => _score;

        public PlayerName Name => _name;
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
    }
}