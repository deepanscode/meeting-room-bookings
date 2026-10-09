using MeetingRoomBookings.Api.Models;
using MeetingRoomBookings.Api.Services;

namespace MeetingRoomBookings.Tests;

public class BookingServiceTests
{
    private readonly BookingService _service = new(new RoomService());

    private static readonly DateTime NineAm = new(2030, 1, 15, 9, 0, 0);
    private static readonly DateTime TenAm = new(2030, 1, 15, 10, 0, 0);

    [Fact]
    public void Create_ReturnsBooking_WhenRoomIsFree()
    {
        var booking = _service.Create("alice", new CreateBookingRequest(1, NineAm, TenAm));

        Assert.Equal(1, booking.RoomId);
        Assert.Equal("alice", booking.UserId);
    }

    [Fact]
    public void Create_Throws_WhenRoomIsAlreadyBooked()
    {
        _service.Create("alice", new CreateBookingRequest(1, NineAm, TenAm));

        Assert.Throws<InvalidOperationException>(() =>
            _service.Create("bob", new CreateBookingRequest(1, NineAm, TenAm)));
    }

    [Fact]
    public void Create_Throws_WhenEndIsBeforeStart()
    {
        Assert.Throws<ArgumentException>(() =>
            _service.Create("alice", new CreateBookingRequest(1, TenAm, NineAm)));
    }

    [Fact]
    public void Create_Throws_WhenOutsideBusinessHours()
    {
        var sevenPm = new DateTime(2030, 1, 15, 19, 0, 0);

        Assert.Throws<ArgumentException>(() =>
            _service.Create("alice", new CreateBookingRequest(1, sevenPm, sevenPm.AddHours(1))));
    }

    [Fact]
    public void Cancel_RemovesBooking()
    {
        var booking = _service.Create("alice", new CreateBookingRequest(1, NineAm, TenAm));

        Assert.True(_service.Cancel(booking.Id));
        Assert.False(_service.Cancel(booking.Id));
    }
}
