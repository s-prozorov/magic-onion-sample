namespace Lw.MagicOnion.Sample.Shared.Messages
{
    public struct PlayerJoinedEvent
    {
        public PlayerJoinedEvent(int x, int y, Guid playerId)
        {
            X = x;
            Y = y;
            PlayerId = playerId;
        }

        public int X { get; }
        public int Y { get; }
        public Guid PlayerId { get; }
    }
}