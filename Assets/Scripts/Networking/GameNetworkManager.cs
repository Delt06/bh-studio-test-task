using System;
using Core;
using Mirror;

namespace Networking
{
    public class GameNetworkManager : NetworkManager
    {
        public static GameNetworkManager Instance => (GameNetworkManager) singleton;

        public void OnSpawnedPlayer(Player player)
        {
            PlayerSpawned?.Invoke(player);
        }

        public void OnDespawnedPlayer(Player player)
        {
            PlayerDespawned?.Invoke(player);
        }

        public event Action<Player> PlayerSpawned;
        public event Action<Player> PlayerDespawned;
    }
}