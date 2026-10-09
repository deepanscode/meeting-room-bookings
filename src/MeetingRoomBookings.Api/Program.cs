using MeetingRoomBookings.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<RoomService>();

var app = builder.Build();

app.MapGet("/health", () => Results.Ok("healthy"));

app.MapGet("/rooms", (RoomService rooms) => rooms.GetRooms());

app.MapGet("/rooms/{id:int}", (int id, RoomService rooms) =>
    rooms.GetRoom(id) is { } room ? Results.Ok(room) : Results.NotFound());

app.Run();

public partial class Program;
