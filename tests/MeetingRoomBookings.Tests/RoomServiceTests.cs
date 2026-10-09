using MeetingRoomBookings.Api.Services;

namespace MeetingRoomBookings.Tests;

public class RoomServiceTests
{
    private readonly RoomService _service = new();

    [Fact]
    public void GetRooms_ReturnsAllRooms()
    {
        Assert.Equal(3, _service.GetRooms().Count);
    }

    [Fact]
    public void GetRoom_ReturnsRoom_WhenRoomExists()
    {
        var room = _service.GetRoom(2);

        Assert.NotNull(room);
        Assert.Equal("Bridge", room.Name);
    }

    [Fact]
    public void GetRoom_ReturnsNull_WhenRoomDoesNotExist()
    {
        Assert.Null(_service.GetRoom(99));
    }
}
