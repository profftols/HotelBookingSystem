using HotelBookingSystem.Api.Data;
using HotelBookingSystem.Api.Models;
using HotelBookingSystem.Api.Models.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly AppDbContext _context;

    public RoomsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Room>>> GetAllRooms()
    {
        var rooms = await _context.Rooms.ToListAsync();
        return Ok(rooms);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Room>> GetRoomById(int id)
    {
        var room = await _context.Rooms.FindAsync(id);
        return room is null ? NotFound() : Ok(room);
    }

    [HttpPost]
    public async Task<ActionResult<Room>> CreateRoom(CreateRoomRequest request)
    {
        var roomEntity = new Room
        {
            Name = request.Name,
            Status = request.Status
        };
        
        _context.Rooms.Add(roomEntity);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetRoomById), new { id = roomEntity.Id }, roomEntity);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateRoom(int id, UpdateRoomRequest request)
    {
        var roomInDb = await _context.Rooms.FindAsync(id);

        if (roomInDb is null)
        {
            return NotFound();
        }
        
        roomInDb.Name = request.Name;
        roomInDb.Status = request.Status;

        await _context.SaveChangesAsync();
        return  NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteRoom(int id)
    {
        var room = await _context.Rooms.FindAsync(id);

        if (room is null)
        {
            return  NotFound();
        }
        
        _context.Rooms.Remove(room);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}