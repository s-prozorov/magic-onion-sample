namespace Lw.MagicOnion.Sample.Shared.Messages
{
    public struct PlayerMoveBegin
    {
        public PlayerMoveBegin(Direction direction, Guid playerId) =>
            (Direction, PlayerId) = (direction, playerId);
        
        public Direction Direction { get; }
        public Guid PlayerId { get; }
    }
}