using MeetingRoomBookings.Api.Models;
using MeetingRoomBookings.Api.Services;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<RoomService>();
builder.Services.AddSingleton<BookingService>();

var app = builder.Build();

// Global error handler so the client always gets a clear error message
app.UseExceptionHandler(errorApp => errorApp.Run(async context =>
{
    var error = context.Features.Get<IExceptionHandlerFeature>()?.Error;

    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
    await context.Response.WriteAsJsonAsync(new
    {
        message = error?.Message,
        details = error?.ToString()
    });
}));

app.MapGet("/health", () => Results.Ok("healthy"));

app.MapGet("/rooms", (RoomService rooms) => rooms.GetRooms());

app.MapGet("/rooms/{id:int}", (int id, RoomService rooms) =>
    rooms.GetRoom(id) is { } room ? Results.Ok(room) : Results.NotFound());

app.MapPost("/bookings", (CreateBookingRequest request, HttpContext context, BookingService bookings) =>
{
    var userId = context.Request.Headers["X-User-Id"].ToString();
    var booking = bookings.Create(userId, request);

    return Results.Created($"/bookings/{booking.Id}", booking);
});

app.MapDelete("/bookings/{id:guid}", (Guid id, BookingService bookings) =>
    bookings.Cancel(id) ? Results.NoContent() : Results.NotFound());

app.Run();

public partial class Program;
