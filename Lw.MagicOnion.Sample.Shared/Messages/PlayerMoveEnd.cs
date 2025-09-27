namespace Lw.MagicOnion.Sample.Shared.Messages
{
    public struct PlayerMoveEnd
    {
        public PlayerMoveEnd(Location location, Guid playerId) =>
            (StoppedAt, PlayerId) = (location, playerId);
        
        public Location StoppedAt { get; }
        public Guid PlayerId { get; }
    }
}