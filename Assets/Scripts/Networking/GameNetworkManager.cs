using System;
using System.Collections.Generic;
using Core;
using Mirror;
using Names;
using UnityEngine;

namespace Networking
{
    public class GameNetworkManager : NetworkRoomManager
    {
        private readonly Dictionary<int, string> _playerNamesByConnectionId = new Dictionary<int, string>();

        private bool _showStartButton;

        public static GameNetworkManager Instance => (GameNetworkManager) singleton;

        public HashSet<Player> Players { get; } = new HashSet<Player>();

        public override void OnGUI()
        {
            base.OnGUI();

            if (allPlayersReady && _showStartButton && GUI.Button(new Rect(150, 300, 120, 20), "START GAME"))
            {
                // set to false to hide it in the game scene
                _showStartButton = false;

                ServerChangeScene(GameplayScene);
            }
        }

        public void OnSpawnedPlayer(Player player)
        {
            Players.Add(player);

            if (NetworkServer.active)
            {
                var playerName =
                    _playerNamesByConnectionId.TryGetValue(player.ConnectionToClient.connectionId, out var storedName)
                        ? storedName
                        : NameUtils.DefaultName;
                player.Name.Set(playerName);
            }

            PlayersChanged?.Invoke();
        }

        public void OnDespawnedPlayer(Player player)
        {
            Players.Remove(player);
            PlayersChanged?.Invoke();
        }

        public event Action PlayersChanged;

        [Server]
        public void SetPlayerName(NetworkConnectionToClient connection, string newName)
        {
            _playerNamesByConnectionId[connection.connectionId] = newName;
        }

        public override void OnRoomServerPlayersReady()
        {
            // calling the base method calls ServerChangeScene as soon as all players are in Ready state.
#if UNITY_SERVER
            base.OnRoomServerPlayersReady();
#else
            _showStartButton = true;
#endif
        }
    }
}