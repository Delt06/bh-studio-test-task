using Mirror;
using Networking;
using ScoreSystem;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(NetworkIdentity))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private Score _score;

        private NetworkIdentity _networkIdentity;

        public Score Score => _score;
        public bool IsLocalPlayer => _networkIdentity.isLocalPlayer;

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

        public string GetName() => _networkIdentity.netId.ToString();
    }
}