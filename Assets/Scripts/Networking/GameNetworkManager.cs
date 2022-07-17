using System;
using System.Collections.Generic;
using Core;
using Mirror;

namespace Networking
{
    public class GameNetworkManager : NetworkManager
    {
        public static GameNetworkManager Instance => (GameNetworkManager) singleton;

        public HashSet<Player> Players { get; } = new HashSet<Player>();

        public void OnSpawnedPlayer(Player player)
        {
            Players.Add(player);
            PlayersChanged?.Invoke();
        }

        public void OnDespawnedPlayer(Player player)
        {
            Players.Remove(player);
            PlayersChanged?.Invoke();
        }

        public event Action PlayersChanged;
    }
}