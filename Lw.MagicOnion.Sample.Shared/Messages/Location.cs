namespace Lw.MagicOnion.Sample.Shared.Messages
{
    public struct Location
    {
        public Location(ushort x, ushort y) => (X, Y) = (x, y);
        
        public ushort X { get; }
        public ushort Y { get; }
    }
}