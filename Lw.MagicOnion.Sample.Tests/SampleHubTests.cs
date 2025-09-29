using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Lw.MagicOnion.Sample.Shared;
using Lw.MagicOnion.Sample.Shared.Messages;
using MagicOnion.Client;
using Xunit;

public class SampleHubTests
{
    [Fact]
    public async Task CanJoinAndMove()
    {
        var channel = GrpcChannel.ForAddress("https://localhost:7165");

        var receiver1 = new TestReceiver();
        var hub1 = await StreamingHubClient.ConnectAsync<ISampleHub, ISampleHubReceiver>(channel, receiver1);

        var receiver2 = new TestReceiver();
        var hub2 = await StreamingHubClient.ConnectAsync<ISampleHub, ISampleHubReceiver>(channel, receiver2);

        await hub1.JoinRoomAsync(1);
        await hub2.JoinRoomAsync(1);

        await hub1.StartMoveAsync(Direction.Right);
        await hub1.EndMoveAsync();

        await Task.Delay(500);

        Assert.True(receiver2.EndMoveReceived);
    }

    private class TestReceiver : ISampleHubReceiver
    {
        public bool EndMoveReceived { get; private set; }

        public void OnEndMove(PlayerMoveEnd evt)
        {
            EndMoveReceived = true;
        }

        public void OnBeginMove(PlayerMoveBegin evt)
        {
        }

        public void OnPlayerJoined(PlayerJoinedEvent evt)
        {
        }
    }
}