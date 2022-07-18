using Mirror;
using Names;
using UnityEngine;

namespace Networking
{
    public class GameNetworkRoomPlayer : NetworkRoomPlayer
    {
        [SyncVar]
        private string _name = NameUtils.DefaultName;

        public override void OnGUI()
        {
            if (!showRoomGUI)
                return;

            var room = NetworkManager.singleton as NetworkRoomManager;
            if (room)
            {
                if (!room.showRoomGUI)
                    return;

                if (!NetworkManager.IsSceneActive(room.RoomScene))
                    return;

                DrawPlayerReadyState();
                DrawPlayerReadyButton();
            }
        }

        private void DrawPlayerReadyState()
        {
            GUILayout.BeginArea(new Rect(20f + index * 100, 200f, 90f, 130f));

            if (isLocalPlayer)
            {
                var newName = GUILayout.TextField(_name);
                if (newName != _name)
                {
                    CmdNameChanged(newName);
                    _name = newName;
                }
            }
            else
            {
                GUILayout.Label(_name);
            }


            if (readyToBegin)
                GUILayout.Label("Ready");
            else
                GUILayout.Label("Not Ready");

            if ((isServer && index > 0 || isServerOnly) && GUILayout.Button("REMOVE"))
                // This button only shows on the Host for all players other than the Host
                // Host and Players can't remove themselves (stop the client instead)
                // Host can kick a Player this way.
                GetComponent<NetworkIdentity>().connectionToClient.Disconnect();

            GUILayout.EndArea();
        }

        [Command]
        private void CmdNameChanged(string newName, NetworkConnectionToClient connection = null)
        {
            _name = newName;
            GameNetworkManager.Instance.SetPlayerName(connection, newName);
        }

        private void DrawPlayerReadyButton()
        {
            if (NetworkClient.active && isLocalPlayer)
            {
                GUILayout.BeginArea(new Rect(20f, 300f, 120f, 20f));

                if (readyToBegin)
                {
                    if (GUILayout.Button("Cancel"))
                        CmdChangeReadyState(false);
                }
                else
                {
                    if (GUILayout.Button("Ready"))
                        CmdChangeReadyState(true);
                }

                GUILayout.EndArea();
            }
        }
    }
}