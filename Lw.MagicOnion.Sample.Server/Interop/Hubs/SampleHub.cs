using Lw.MagicOnion.Sample.Server.Game;
using Lw.MagicOnion.Sample.Shared;
using Lw.MagicOnion.Sample.Shared.Messages;
using MagicOnion.Server.Hubs;

namespace Lw.MagicOnion.Sample.Server.Interop.Hubs;


public class SampleHub : StreamingHubBase<ISampleHub, ISampleHubReceiver>, ISampleHub
{
    private readonly IRoomProvider _roomProvider;
    
    private IGroup<ISampleHubReceiver> _group = null!;
    private int? _roomId = null!;

    public SampleHub(IRoomProvider roomProvider) =>
        _roomProvider = roomProvider;


    private bool IsJoined => _roomId.HasValue;
    private Guid PlayerId => Context.ContextId;
    private IRoom Room => _roomProvider.GetRoom(_roomId ?? throw new InvalidOperationException());
    

    public async Task JoinRoomAsync(int roomId)
    {
        if (IsJoined)
            return;
        
        _roomId = roomId;
        
        Room.PlaceNewPlayer(PlayerId);

        var spawnedAt = Room.GetPlayerLocation(PlayerId);
        
        _group = await Group.AddAsync(roomId.ToString());
        _group.All.OnPlayerJoined(new PlayerJoinedEvent(spawnedAt, PlayerId));
    }

    public Task StartMoveAsync(Direction direction)
    {
        if (!IsJoined)
            return Task.CompletedTask;
        
        Room.BeginMove(PlayerId, direction);
        _group!.All.OnBeginMove(new PlayerMoveBegin(direction, PlayerId));
        return Task.CompletedTask;
    }

    public Task EndMoveAsync()
    {
        if (!IsJoined)
            return Task.CompletedTask;
        
        Room.EndMove(PlayerId);
        
        var stoppedAt = Room.GetPlayerLocation(PlayerId);
        
        _group!.All.OnEndMove(new PlayerMoveEnd(stoppedAt, PlayerId));

        return Task.CompletedTask;
    }
}