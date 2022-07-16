using Networking;
using ScoreSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Score _score;

    public Score Score => _score;

    private void Start()
    {
        var gameNetworkManager = GameNetworkManager.Instance;
        gameNetworkManager.OnSpawnedPlayer(this);
    }

    private void OnDestroy()
    {
        var gameNetworkManager = GameNetworkManager.Instance;
        if (gameNetworkManager != null)
            gameNetworkManager.OnDespawnedPlayer(this);
    }
}