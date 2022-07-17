using System;
using System.Collections;
using Mirror;
using Networking;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class Game : NetworkBehaviour
    {
        public enum GameState
        {
            Playing,
            Finished,
        }

        [SerializeField] [Min(0f)] private float _delayBeforeRestart = 5f;

        [field: SyncVar]
        private GameState State { get; set; }

        private void OnDestroy()
        {
            Time.timeScale = 1f;
        }


        public void OnWon(string winnerName)
        {
            if (State != GameState.Playing) return;
            State = GameState.Finished;
            Time.timeScale = 0f;
            WinRpc(winnerName);
            StartCoroutine(RestartAfterDelay());
        }

        [ClientRpc]
        private void WinRpc(string winnerName)
        {
            Finished?.Invoke(new GameFinishArgs
                {
                    WinnerName = winnerName,
                }
            );
        }

        public event Action<GameFinishArgs> Finished;

        private IEnumerator RestartAfterDelay()
        {
            yield return new WaitForSecondsRealtime(_delayBeforeRestart);
            Restart();
        }

        private static void Restart()
        {
            GameNetworkManager.Instance.ServerChangeScene(SceneManager.GetActiveScene().name);
        }
    }
}