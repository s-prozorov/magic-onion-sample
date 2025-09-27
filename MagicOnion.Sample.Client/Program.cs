using Lw.MagicOnion.Sample.Shared;
using Lw.MagicOnion.Sample.Shared.Messages;

class Launcher
{
    static async Task Main()
    {
        var tasks = new[]
        {
            Task.Run(() => RunClientAsync(1, "Alice")),
            Task.Run(() => RunClientAsync(1, "Bob")),
            Task.Run(() => RunClientAsync(1, "Charlie")),
             Task.Run(() => RunClientAsync(2, "Dave")),
             Task.Run(() => RunClientAsync(2, "Eve")),
             Task.Run(() => RunClientAsync(2, "Frank")),
        };

        await Task.WhenAll(tasks);
    }

    static async Task RunClientAsync(int roomId, string name)
    {
        var channel = Grpc.Net.Client.GrpcChannel.ForAddress("https://localhost:7165");
        var hub = await MagicOnion.Client.StreamingHubClient.ConnectAsync<ISampleHub, ISampleHubReceiver>(channel, new ConsoleReceiver(name));

        await hub.JoinRoomAsync(roomId);
        Console.WriteLine($"{name} joined room {roomId}");

        await hub.StartMoveAsync(Direction.Right);
        await Task.Delay(1000);
        await hub.EndMoveAsync();

        await Task.Delay(5000);
        await hub.DisposeAsync();
        Console.WriteLine($"{name} disconnected");
    }
}

class ConsoleReceiver : ISampleHubReceiver
{
    private readonly string _name;
    public ConsoleReceiver(string name) => _name = name;

    public void OnPlayerJoined(PlayerJoinedEvent evt)
    {
        Console.WriteLine($"[{_name}] sees join: {evt.PlayerId} at ({evt.SpawnedAt.X}, {evt.SpawnedAt.Y})");
    }

    public void OnBeginMove(PlayerMoveBegin evt)
    {
        Console.WriteLine($" [{_name}] sees move: {evt.PlayerId} → {evt.Direction}");
    }

    public void OnEndMove(PlayerMoveEnd evt)
    {
        Console.WriteLine($" [{_name}] sees stop: {evt.PlayerId} at ({evt.StoppedAt.X}, {evt.StoppedAt.Y})");
    }
}

