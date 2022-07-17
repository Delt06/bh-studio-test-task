using Core;
using UnityEngine;

namespace Win
{
    public class WinScreenPresenter : MonoBehaviour
    {
        [SerializeField] private Game _game;
        [SerializeField] private WinScreenView _view;

        private void OnEnable()
        {
            _game.Finished += OnFinished;
        }

        private void OnDisable()
        {
            _game.Finished -= OnFinished;
        }

        private void OnFinished(GameFinishArgs args)
        {
            _view.Open(args.WinnerName);
        }
    }
}