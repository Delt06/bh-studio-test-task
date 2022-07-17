using System.Linq;
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
            _networkManager.PlayersChanged += OnPlayersChanged;
        }

        private void OnDestroy()
        {
            if (_networkManager == null) return;
            _networkManager.PlayersChanged -= OnPlayersChanged;
        }

        private void OnPlayersChanged()
        {
            foreach (var leaderboardPlayer in _leaderboard.Players.ToArray())
            {
                if (!_networkManager.Players.Contains(leaderboardPlayer))
                    _leaderboard.UnregisterPlayer(leaderboardPlayer);
            }

            foreach (var player in _networkManager.Players)
            {
                _leaderboard.RegisterPlayer(player);
            }
        }
    }
}