using System.Collections.Concurrent;
using Lw.MagicOnion.Sample.Shared.Messages;

namespace Lw.MagicOnion.Sample.Server.Game;

public class Room : IRoom
{
    private const ushort MapSize = 1000;

    private readonly ConcurrentDictionary<Guid, Location> _positions = new();
    private readonly ConcurrentDictionary<Guid, ConcurrentQueue<Direction>> _moveBuffer = new();

    private readonly Random _random = new();

    public Location PlaceNewPlayer(Guid playerId)
    {
        var x = (ushort)_random.Next(0, MapSize);
        var y = (ushort)_random.Next(0, MapSize);

        var location = new Location(x, y);
        _positions[playerId] = location;

        return location;
    }

    public Location GetPlayerLocation(Guid playerId)
    {
        return _positions.TryGetValue(playerId, out var loc)
            ? loc
            : throw new InvalidOperationException($"Игрок {playerId} не найден");
    }

    public void BeginMove(Guid playerId, Direction direction)
    {
        if (!_positions.ContainsKey(playerId))
            throw new InvalidOperationException($"Игрок {playerId} не найден");

        var queue = _moveBuffer.GetOrAdd(playerId, _ => new ConcurrentQueue<Direction>());
        queue.Enqueue(direction);
    }

    public void EndMove(Guid playerId)
    {
        if (!_positions.TryGetValue(playerId, out var loc))
            throw new InvalidOperationException($"Игрок {playerId} не найден");

        if (!_moveBuffer.TryGetValue(playerId, out var moves) || moves.IsEmpty)
            return;

        var currentLoc = loc;
        while (moves.TryDequeue(out var direction))
        {
            ushort newX = currentLoc.X;
            ushort newY = currentLoc.Y;

            switch (direction)
            {
                case Direction.Left:
                    newX = (ushort)Math.Max(0, currentLoc.X - 1);
                    break;
                case Direction.Right:
                    newX = (ushort)Math.Min(MapSize - 1, currentLoc.X + 1);
                    break;
                case Direction.Up:
                    newY = (ushort)Math.Min(MapSize - 1, currentLoc.Y + 1);
                    break;
                case Direction.Down:
                    newY = (ushort)Math.Max(0, currentLoc.Y - 1);
                    break;
            }

            currentLoc = new Location(newX, newY);
        }

        _positions[playerId] = currentLoc;
    }
}