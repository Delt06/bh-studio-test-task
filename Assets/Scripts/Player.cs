using Mirror;
using Networking;
using ScoreSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Score _score;

    public Score Score => _score;

    private void Start()
    {
        var gameNetworkManager = (GameNetworkManager) NetworkManager.singleton;
        gameNetworkManager.OnSpawnedPlayer(this);
    }

    private void OnDestroy()
    {
        var gameNetworkManager = (GameNetworkManager) NetworkManager.singleton;
        if (gameNetworkManager != null)
            gameNetworkManager.OnDespawnedPlayer(this);
    }
}