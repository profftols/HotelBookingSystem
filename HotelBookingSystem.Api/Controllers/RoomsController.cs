using HotelBookingSystem.Api.Data;
using HotelBookingSystem.Api.Models;
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
    public async Task<ActionResult<Room>> CreateRoom(Room newRoom)
    {
        _context.Rooms.Add(newRoom);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetRoomById), new { id = newRoom.Id }, newRoom);
    }
}