namespace Lw.MagicOnion.Sample.Server.Game;

public interface IRoomProvider
{
    IRoom GetRoom(int id);
}