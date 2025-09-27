using Lw.MagicOnion.Sample.Shared.Messages;

namespace Lw.MagicOnion.Sample.Server.Game;

public interface IRoom
{
    Location PlaceNewPlayer(Guid playerId);
    Location GetPlayerLocation(Guid playerId);
    void BeginMove(Guid playerId, Direction direction);
    void EndMove(Guid playerId);
}