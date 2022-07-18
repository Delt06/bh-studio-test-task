using Core;
using TMPro;
using UnityEngine;

namespace ScoreSystem
{
    public class LeaderboardViewItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private Color _localPlayerColor = Color.green;

        public Player Player { get; private set; }

        public void UpdateScore(int score)
        {
            _scoreText.SetText("{0:0}", score);
        }

        public void UpdateName(string playerName)
        {
            _nameText.text = playerName;
        }

        public void Init(Player player)
        {
            Player = player;
        }

        public void MarkAsLocal()
        {
            SetTextColor(_localPlayerColor);
        }

        private void SetTextColor(Color color)
        {
            _nameText.color = color;
            _scoreText.color = color;
        }
    }
}