using HotelBookingSystem.Api.Data;
using HotelBookingSystem.Api.Models;
using HotelBookingSystem.Api.Models.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Api.Services;

public class RoomService : IRoomService
{
    private readonly AppDbContext _context;

    public RoomService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Room>> GetAllRoomsAsync()
    {
        return await _context.Rooms.ToListAsync();
    }

    public async Task<Room?> GetRoomByIdAsync(int id)
    {
        return await _context.Rooms.FindAsync(id);
    }

    public async Task<Room> CreateRoomAsync(CreateRoomRequest request)
    {
        var room = new Room()
        {
            Name = request.Name,
            Status = request.Status
        };
        
        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();
        return room;
    }

    public async Task UpdateRoomAsync(int id, UpdateRoomRequest request)
    {
        var room = await _context.Rooms.FindAsync(id);

        if (room != null)
        {
            room.Name = request.Name;
            room.Status = request.Status;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteRoomAsync(int id)
    {
        var room = await _context.Rooms.FindAsync(id);

        if (room != null)
        {
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
        }
    }
}