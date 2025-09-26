using Lw.MagicOnion.Sample.Shared.Messages;
using MagicOnion;

namespace Lw.MagicOnion.Sample.Shared
{

    public interface ISampleHub : IStreamingHub<ISampleHub, ISampleHubReceiver>
    {
        Task JoinRoomAsync(int roomId, CancellationToken cancellationToken);
        Task StartMoveAsync(Direction direction, CancellationToken cancellationToken);
        Task EndMoveAsync(CancellationToken cancellationToken);
    }
}