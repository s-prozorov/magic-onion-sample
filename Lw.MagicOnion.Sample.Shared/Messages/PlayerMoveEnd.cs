using MessagePack;

namespace Lw.MagicOnion.Sample.Shared.Messages
{
    [MessagePackObject]
    public struct PlayerMoveEnd
    {
        public PlayerMoveEnd(Location location, Guid playerId) =>
            (StoppedAt, PlayerId) = (location, playerId);
        [Key(0)]
        public Location StoppedAt { get; }
        [Key(1)]
        public Guid PlayerId { get; }
    }
}