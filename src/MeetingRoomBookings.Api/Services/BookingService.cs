using MeetingRoomBookings.Api.Models;

namespace MeetingRoomBookings.Api.Services;

public class BookingService(RoomService rooms)
{
    private readonly List<Booking> _bookings = [];

    public Booking Create(string userId, CreateBookingRequest request)
    {
        var room = rooms.GetRoom(request.RoomId);

        if (request.End <= request.Start)
            throw new ArgumentException("End time must be after start time.");

        // Bookings must be within business hours (8am - 6pm)
        if (request.Start.Hour < 8 || request.Start.Hour >= 18)
            throw new ArgumentException("Bookings must be within business hours (8am - 6pm).");

        // Prevent double-booking: reject if the room is already taken at this time
        var isTaken = _bookings.Any(b =>
            b.RoomId == room.Id &&
            request.Start >= b.Start && request.Start < b.End);

        if (isTaken)
            throw new InvalidOperationException($"Room '{room.Name}' is already booked for this time.");

        var booking = new Booking(Guid.NewGuid(), room.Id, userId, request.Start, request.End);
        _bookings.Add(booking);

        return booking;
    }

    public bool Cancel(Guid bookingId)
    {
        var booking = _bookings.FirstOrDefault(b => b.Id == bookingId);
        if (booking is null)
            return false;

        _bookings.Remove(booking);
        return true;
    }
}
