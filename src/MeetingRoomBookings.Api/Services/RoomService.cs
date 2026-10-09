using MeetingRoomBookings.Api.Models;

namespace MeetingRoomBookings.Api.Services;

public class RoomService
{
    private readonly List<Room> _rooms =
    [
        new Room(1, "Harbour", 4),
        new Room(2, "Bridge", 8),
        new Room(3, "Opera", 12),
    ];

    public IReadOnlyList<Room> GetRooms() => _rooms;

    public Room? GetRoom(int id) => _rooms.FirstOrDefault(r => r.Id == id);
}
