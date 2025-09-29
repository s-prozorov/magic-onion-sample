using MessagePack;

namespace Lw.MagicOnion.Sample.Shared.Messages
{
    [MessagePackObject]
    public struct PlayerMoveBegin
    {
        public PlayerMoveBegin(Direction direction, Guid playerId) =>
            (Direction, PlayerId) = (direction, playerId);

        [Key(0)] public Direction Direction { get; }
        [Key(1)] public Guid PlayerId { get; }
    }
}