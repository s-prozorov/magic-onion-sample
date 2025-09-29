using Lw.MagicOnion.Sample.Shared.Messages;
using MagicOnion;

namespace Lw.MagicOnion.Sample.Shared
{

    public interface ISampleHub : IStreamingHub<ISampleHub, ISampleHubReceiver>
    {
        Task JoinRoomAsync(int roomId);
        Task StartMoveAsync(Direction direction);
        Task EndMoveAsync();
    }
}