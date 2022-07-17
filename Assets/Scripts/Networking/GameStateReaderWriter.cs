using Core;
using JetBrains.Annotations;
using Mirror;

namespace Networking
{
    public static class GameStateReaderWriter
    {
        [UsedImplicitly]
        public static void WriteGameState(this NetworkWriter writer, Game.GameState gameState) =>
            writer.WriteInt((int) gameState);

        [UsedImplicitly]
        public static Game.GameState ReadGameState(this NetworkReader reader) =>
            (Game.GameState) reader.ReadInt();
    }
}