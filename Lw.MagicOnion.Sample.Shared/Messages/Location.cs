using MessagePack;

namespace Lw.MagicOnion.Sample.Shared.Messages
{
    [MessagePackObject]
    public struct Location
    {
        public Location(ushort x, ushort y) => (X, Y) = (x, y);
        [Key(0)]
        public ushort X { get; }
        [Key(1)]
        public ushort Y { get; }
    }
}