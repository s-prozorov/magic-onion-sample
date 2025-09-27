using Lw.MagicOnion.Sample.Server.Game;
using Lw.MagicOnion.Sample.Shared;
using Lw.MagicOnion.Sample.Shared.Messages;
using MagicOnion.Server.Hubs;

namespace Lw.MagicOnion.Sample.Server.Interop.Hubs;


public class SampleHub : StreamingHubBase<ISampleHub, ISampleHubReceiver>, ISampleHub
{
    private readonly ISampleGameLogic _gameLogic;
    private IGroup<ISampleHubReceiver>? _group;

    public SampleHub(ISampleGameLogic gameLogic) =>
        _gameLogic = gameLogic;


    private bool IsJoined => _group is not null;
    private Guid PlayerId => Context.ContextId;
    

    public async Task JoinRoomAsync(int roomId, CancellationToken cancellationToken)
    {
        if (IsJoined)
            return;
        
        _gameLogic.PlaceNewPlayer(PlayerId);
        
        var spawnedAt = _gameLogic.GetPlayerLocation(PlayerId);
        
        _group = await Group.AddAsync(roomId.ToString());
        _group.All.OnPlayerJoined(new PlayerJoinedEvent(spawnedAt, PlayerId));
    }

    public Task StartMoveAsync(Direction direction, CancellationToken cancellationToken)
    {
        if (!IsJoined)
            return Task.CompletedTask;
        
        _gameLogic.BeginMove(PlayerId, direction);
        _group!.All.OnBeginMove(new PlayerMoveBegin(direction, PlayerId));

        return Task.CompletedTask;
    }

    public Task EndMoveAsync(CancellationToken cancellationToken)
    {
        if (!IsJoined)
            return Task.CompletedTask;
        
        _gameLogic.EndMove(PlayerId);
        
        var stoppedAt = _gameLogic.GetPlayerLocation(PlayerId);
        
        _group!.All.OnEndMove(new PlayerMoveEnd(stoppedAt, PlayerId));

        return Task.CompletedTask;
    }
}