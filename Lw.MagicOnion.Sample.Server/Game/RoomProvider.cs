using System.Collections.Concurrent;

namespace Lw.MagicOnion.Sample.Server.Game;

public class RoomProvider : IRoomProvider
{
    private ConcurrentDictionary<int, IRoom> _rooms = new ();
    
    public IRoom GetRoom(int id) => _rooms.GetOrAdd(id, _ => new Room());
}