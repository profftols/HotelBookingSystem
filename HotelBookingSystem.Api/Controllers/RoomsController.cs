using HotelBookingSystem.Api.Models;
using HotelBookingSystem.Api.Models.Contracts;
using HotelBookingSystem.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController(IRoomService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Room>>> GetAllRooms()
    {
        var rooms = await service.GetAllRoomsAsync();
        return Ok(rooms);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Room>> GetRoomById(int id)
    {
        var room = await service.GetRoomByIdAsync(id);
        return room is null ? NotFound() : Ok(room);
    }

    [HttpPost]
    public async Task<ActionResult<Room>> CreateRoom(CreateRoomRequest request)
    {
        var createdRoom = await service.CreateRoomAsync(request);
        return CreatedAtAction(nameof(GetRoomById), new { id = createdRoom.Id }, createdRoom);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateRoom(int id, UpdateRoomRequest request)
    {
        var existingRoom = await service.GetRoomByIdAsync(id);

        if (existingRoom is null)
        {
            return NotFound();
        }

        await service.UpdateRoomAsync(id, request);
        return  NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteRoom(int id)
    {
        var existingRoom = await service.GetRoomByIdAsync(id);

        if (existingRoom is null)
        {
            return NotFound();
        }
        
        await  service.DeleteRoomAsync(id);
        return NoContent();
    }
}