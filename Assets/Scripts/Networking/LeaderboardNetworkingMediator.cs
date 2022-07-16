using ScoreSystem;
using UnityEngine;

namespace Networking
{
    public class LeaderboardNetworkingMediator : MonoBehaviour
    {
        [SerializeField] private Leaderboard _leaderboard;

        private GameNetworkManager _networkManager;

        private void Start()
        {
            _networkManager = GameNetworkManager.Instance;
            _networkManager.PlayerSpawned += OnPlayerSpawned;
            _networkManager.PlayerDespawned += OnPlayerDespawned;
        }

        private void OnDestroy()
        {
            if (_networkManager == null) return;
            _networkManager.PlayerSpawned -= OnPlayerSpawned;
            _networkManager.PlayerDespawned -= OnPlayerDespawned;
        }

        private void OnPlayerSpawned(Player player)
        {
            _leaderboard.RegisterPlayer(player.Score);
        }

        private void OnPlayerDespawned(Player player)
        {
            _leaderboard.UnregisterPlayer(player.Score);
        }
    }
}