namespace Lw.MagicOnion.Sample.Shared.Messages
{
    public readonly record struct PlayerJoinedEvent
    {
        public PlayerJoinedEvent(Location location, Guid playerId) => 
            (SpawnedAt, PlayerId) = (location, playerId);

        public Location SpawnedAt { get; }
        public Guid PlayerId { get; }
    }
}