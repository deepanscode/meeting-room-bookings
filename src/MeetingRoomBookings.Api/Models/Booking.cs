namespace MeetingRoomBookings.Api.Models;

public record Booking(Guid Id, int RoomId, string UserId, DateTime Start, DateTime End);

public record CreateBookingRequest(int RoomId, DateTime Start, DateTime End);
