using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public enum GameState
    {
        Playing,
        Finished,
    }

    private GameState State { get; set; }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }

    public void OnWon()
    {
        if (State != GameState.Playing) return;
        State = GameState.Finished;
        Time.timeScale = 0f;
        Restart();
    }

    public void Restart()
    {
        NetworkManager.singleton.ServerChangeScene(SceneManager.GetActiveScene().name);
    }
}