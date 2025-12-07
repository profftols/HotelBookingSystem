namespace HotelBookingSystem.Api.Models.Contracts;

public record CreateRoomRequest(string Name, RoomStatus Status);