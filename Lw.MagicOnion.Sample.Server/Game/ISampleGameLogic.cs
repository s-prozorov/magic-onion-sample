using Lw.MagicOnion.Sample.Shared.Messages;

namespace Lw.MagicOnion.Sample.Server.Game;

public interface ISampleGameLogic
{
    (ushort, ushort) PlaceNewPlayer(Guid playerId);
    void BeginMove(Guid playerId, Direction direction);
    void EndMove(Guid playerId);
}