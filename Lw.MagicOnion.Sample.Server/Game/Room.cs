using Lw.MagicOnion.Sample.Shared.Messages;

namespace Lw.MagicOnion.Sample.Server.Game;

public class Room : IRoom
{
    public Location PlaceNewPlayer(Guid playerId)
    {
        throw new NotImplementedException();
    }

    public Location GetPlayerLocation(Guid playerId)
    {
        throw new NotImplementedException();
    }

    public void BeginMove(Guid playerId, Direction direction)
    {
        throw new NotImplementedException();
    }

    public void EndMove(Guid playerId)
    {
        throw new NotImplementedException();
    }
}