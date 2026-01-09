using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Api.Models.Contracts;

public record UpdateRoomRequest(
    [Required]
    [StringLength(100)]
    string Name, 
    
    [Required]
    RoomStatus Status);