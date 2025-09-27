using Lw.MagicOnion.Sample.Server.Game;
using Lw.MagicOnion.Sample.Server.Interop.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IRoomProvider, RoomProvider>();
builder.Services.AddMagicOnion();

var app = builder.Build();

app.MapMagicOnionService<SampleHub>();

app.Run();