using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace ScoreSystem
{
    public class LeaderboardView : MonoBehaviour
    {
        private static readonly Comparison<LeaderboardViewItem> ItemComparison = (i1, i2) =>
            -i1.Player.Score.Value.CompareTo(i2.Player.Score.Value);

        [SerializeField] private LeaderboardViewItem _leaderboardViewItemPrefab;
        [SerializeField] private Transform _root;

        private readonly List<LeaderboardViewItem> _orderedPlayerItems = new List<LeaderboardViewItem>();
        private readonly Dictionary<Player, LeaderboardViewItem> _playerItems =
            new Dictionary<Player, LeaderboardViewItem>();

        public void GetPlayers(ICollection<Player> players)
        {
            foreach (var player in _playerItems.Keys)
            {
                players.Add(player);
            }
        }


        public void UpdateItem(Player player)
        {
            if (!_playerItems.TryGetValue(player, out var item))
            {
                item = _playerItems[player] = Instantiate(_leaderboardViewItemPrefab, _root);
                item.Init(player);
                _orderedPlayerItems.Add(item);
            }

            item.SetScore(player.Score.Value);
        }

        public void DestroyItem(Player player)
        {
            if (!_playerItems.TryGetValue(player, out var item)) return;
            Destroy(item.gameObject);
            _playerItems.Remove(player);
            _orderedPlayerItems.Remove(item);
        }

        public void SortItems()
        {
            _orderedPlayerItems.Sort(ItemComparison);

            for (var index = 0; index < _orderedPlayerItems.Count; index++)
            {
                var leaderboardViewItem = _orderedPlayerItems[index];
                leaderboardViewItem.transform.SetSiblingIndex(index);
            }
        }
    }
}