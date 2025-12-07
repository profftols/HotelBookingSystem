using HotelBookingSystem.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Api.Data;

public class AppDbContext : DbContext
{
    public DbSet<Room> Rooms => Set<Room>();
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}