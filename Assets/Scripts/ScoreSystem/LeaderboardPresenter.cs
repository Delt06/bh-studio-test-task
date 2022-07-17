using System.Collections.Generic;
using Core;
using UnityEngine;

namespace ScoreSystem
{
    public class LeaderboardPresenter : MonoBehaviour
    {
        [SerializeField] private Leaderboard _leaderboard;
        [SerializeField] private LeaderboardView _leaderboardView;

        private readonly List<Player> _players = new List<Player>();

        private void OnEnable()
        {
            Refresh();
            _leaderboard.Changed += Refresh;
        }

        private void OnDisable()
        {
            _leaderboard.Changed -= Refresh;
        }

        private void Refresh()
        {
            RemoveStaleItems();
            UpdateRelevantItems();
            Sort();
        }

        private void RemoveStaleItems()
        {
            _leaderboardView.GetPlayers(_players);

            foreach (var player in _players)
            {
                if (_leaderboard.Players.Contains(player)) continue;
                _leaderboardView.DestroyItem(player);
            }

            _players.Clear();
        }

        private void UpdateRelevantItems()
        {
            foreach (var player in _leaderboard.Players)
            {
                _leaderboardView.UpdateItem(player);
            }
        }

        private void Sort() => _leaderboardView.SortItems();
    }
}