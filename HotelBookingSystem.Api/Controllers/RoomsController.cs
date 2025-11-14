using HotelBookingSystem.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private static readonly List<Room> _rooms = new();
    private static int _next = 1;
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Room>> GetRoomById(int id)
    {
        var room = _rooms.FirstOrDefault(r => r.Id == id);
        return room is null ? NotFound() : Ok(room);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Room>>> GetAllRooms()
    {
        return Ok(_rooms);
    }

    [HttpPost]
    public async Task<ActionResult<Room>> CreateRoom(Room newRoom)
    {
        var created = newRoom with { Id = 42 };
        _rooms.Add(created);
        return CreatedAtAction(nameof(GetRoomById), new { id = created.Id }, created);
    }
}