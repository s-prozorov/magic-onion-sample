using Lw.MagicOnion.Sample.Server.Game;
using Lw.MagicOnion.Sample.Shared;
using Lw.MagicOnion.Sample.Shared.Messages;
using MagicOnion.Server.Hubs;

namespace Lw.MagicOnion.Sample.Server.Interop.Hubs;


public class SampleHub : StreamingHubBase<ISampleHub, ISampleHubReceiver>, ISampleHub
{
    private readonly ISampleGameLogic _gameLogic;
    private IGroup<ISampleHubReceiver> _group = null!;
    private Guid? _playerId = null!;

    public SampleHub(ISampleGameLogic gameLogic) =>
        _gameLogic = gameLogic;


    protected override ValueTask OnConnected()
    {
        _playerId = Context.ContextId;
        return base.OnConnected();
    }


    public async Task JoinRoomAsync(int roomId, CancellationToken cancellationToken)
    {
        _group = await Group.AddAsync(roomId.ToString());

        var (x, y) = _gameLogic.PlaceNewPlayer(playerId);
        
        _group.All.OnPlayerJoined(new PlayerJoinedEvent(x, y, playerId));
    }

    public Task StartMoveAsync(Direction direction, CancellationToken cancellationToken)
    {
        if ()
    }

    public Task EndMoveAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}