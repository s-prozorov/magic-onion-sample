using Lw.MagicOnion.Sample.Shared.Messages;

namespace Lw.MagicOnion.Sample.Shared
{
    public interface ISampleHubReceiver
    {
        void OnPlayerJoined(PlayerJoinedEvent evt);
        void OnBeginMove(PlayerMoveBegin evt);
        void OnEndMove(PlayerMoveEnd evt);
    }   
}