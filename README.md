# Meeting Room Bookings

A small internal API for booking the office meeting rooms.

## Running

Requires the [.NET 8 SDK](https://dotnet.microsoft.com/download).

```bash
dotnet run --project src/MeetingRoomBookings.Api
dotnet test
```

## Conventions

- Authentication is handled upstream by the API gateway. The signed-in user's ID
  is forwarded to this service in the `X-User-Id` request header.
- Data is kept in memory for now; a database will be added later.
