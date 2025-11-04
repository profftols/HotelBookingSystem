using HotelBookingSystem.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Room>> GetRoomById(int id)
    {
        var room = new Room(id, $"Room {id}", RoomStatus.Available);
        return Ok(room);
    }

    [HttpPost]
    public async Task<ActionResult<Room>> CreateRoom(Room newRoom)
    {
        var created = newRoom with { Id = 42 };
        return CreatedAtAction(nameof(GetRoomById), new { id = created.Id }, created);
    }
}