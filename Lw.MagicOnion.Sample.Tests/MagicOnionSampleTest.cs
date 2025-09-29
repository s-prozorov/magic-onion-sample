using Lw.MagicOnion.Sample.Server.Game;
using Lw.MagicOnion.Sample.Shared.Messages;

namespace Lw.MagicOnion.Sample.Tests;

public class MagicOnionSampleTest
{
    [Fact]
    public void PlaceNewPlayer_ShouldAssignPosition()
    {
        var room = new Room();
        var playerId = Guid.NewGuid();

        var location = room.PlaceNewPlayer(playerId);
        var stored = room.GetPlayerLocation(playerId);

        Assert.Equal(location.X, stored.X);
        Assert.Equal(location.Y, stored.Y);
    }

    [Fact]
    public void EndMove_ShouldApplyAllDirections()
    {
        var room = new Room();
        var playerId = Guid.NewGuid();

        var start = room.PlaceNewPlayer(playerId);

        room.BeginMove(playerId, Direction.Right);
        room.BeginMove(playerId, Direction.Up);
        room.BeginMove(playerId, Direction.Up);
        room.EndMove(playerId);

        var end = room.GetPlayerLocation(playerId);

        Assert.Equal((ushort)(start.X + 1), end.X);
        Assert.Equal((ushort)(start.Y + 2), end.Y);
    }

    [Fact]
    public void EndMove_WithNoMoves_ShouldNotChangePosition()
    {
        var room = new Room();
        var playerId = Guid.NewGuid();

        var start = room.PlaceNewPlayer(playerId);
        room.EndMove(playerId);
        var end = room.GetPlayerLocation(playerId);

        Assert.Equal(start.X, end.X);
        Assert.Equal(start.Y, end.Y);
    }
}