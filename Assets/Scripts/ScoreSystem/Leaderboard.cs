using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace ScoreSystem
{
    public class Leaderboard : NetworkBehaviour
    {
        [SerializeField] [Min(0)] private int _scoreThreshold = 3;
        [SerializeField] private Game _game;

        private readonly HashSet<Score> _scores = new HashSet<Score>();

        public void RegisterPlayer(Score score)
        {
            if (_scores.Contains(score)) return;
            _scores.Add(score);
            score.ValueChanged += OnValueChanged;
        }

        public void UnregisterPlayer(Score score)
        {
            if (!_scores.Contains(score)) return;
            _scores.Remove(score);
            score.ValueChanged -= OnValueChanged;
        }

        private void OnValueChanged(object sender, EventArgs args)
        {
            var score = (Score) sender;
            var id = score.GetComponent<NetworkIdentity>().netId;
            Debug.Log($"Score[{id}]={score.Value}");

            if (isServer)
                CheckWinner();
        }

        private void CheckWinner()
        {
            foreach (var score in _scores)
            {
                if (score.Value >= _scoreThreshold)
                {
                    _game.OnWon();
                    Debug.Log(score + " wins");
                    break;
                }
            }
        }
    }
}