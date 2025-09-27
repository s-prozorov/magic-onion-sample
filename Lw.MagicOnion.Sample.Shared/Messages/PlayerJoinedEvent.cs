using MessagePack;

namespace Lw.MagicOnion.Sample.Shared.Messages
{
    [MessagePackObject]
    public readonly record struct PlayerJoinedEvent
    {
        [SerializationConstructor]
        public PlayerJoinedEvent(Location location, Guid playerId) =>
            (SpawnedAt, PlayerId) = (location, playerId);

        [Key(0)] public Location SpawnedAt { get; }
        [Key(1)] public Guid PlayerId { get; }
    }
}