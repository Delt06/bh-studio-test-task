using System;
using System.Collections.Generic;
using Core;
using Mirror;
using UnityEngine;

namespace ScoreSystem
{
    public class Leaderboard : NetworkBehaviour
    {
        [SerializeField] [Min(0)] private int _scoreThreshold = 3;
        [SerializeField] private Game _game;

        public HashSet<Player> Players { get; } = new HashSet<Player>();

        public void RegisterPlayer(Player player)
        {
            if (Players.Contains(player)) return;
            Players.Add(player);
            player.Score.ValueChanged += OnValueChanged;
            Changed?.Invoke();
        }

        public void UnregisterPlayer(Player player)
        {
            if (!Players.Contains(player)) return;
            Players.Remove(player);
            player.Score.ValueChanged -= OnValueChanged;
            Changed?.Invoke();
        }

        private void OnValueChanged(object sender, EventArgs args)
        {
            Changed?.Invoke();

            if (isServer)
                CheckWinner();
        }

        public event Action Changed;

        private void CheckWinner()
        {
            foreach (var player in Players)
            {
                if (player.Score.Value < _scoreThreshold) continue;

                _game.OnWon(player.GetName());
                break;
            }
        }
    }
}