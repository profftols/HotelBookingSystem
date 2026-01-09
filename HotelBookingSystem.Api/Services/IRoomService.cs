using HotelBookingSystem.Api.Models;
using HotelBookingSystem.Api.Models.Contracts;

namespace HotelBookingSystem.Api.Services;

public interface IRoomService
{
    Task<IEnumerable<Room>> GetAllRoomsAsync();
    Task<Room?> GetRoomByIdAsync(int id);
    Task<Room> CreateRoomAsync(CreateRoomRequest request);
    Task UpdateRoomAsync(int id, UpdateRoomRequest request);
    Task DeleteRoomAsync(int id);
}