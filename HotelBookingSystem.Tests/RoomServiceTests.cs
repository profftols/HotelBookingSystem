using HotelBookingSystem.Api.Data;
using HotelBookingSystem.Api.Models;
using HotelBookingSystem.Api.Models.Contracts;
using HotelBookingSystem.Api.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HotelBookingSystem.Tests;

public class RoomServiceTests
{
    private DbContextOptions<AppDbContext> CreateNewContextOptions()
    {
        return new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public async Task UpdateRoomAsync_Should_SaveChanges_When_RoomExists()
    {
        // Arrange
        var options = CreateNewContextOptions();

        // Seed database
        using (var context = new AppDbContext(options))
        {
            context.Rooms.Add(new Room { Id = 1, Name = "Old Name", Status = RoomStatus.Available });
            await context.SaveChangesAsync();
        }

        using (var context = new AppDbContext(options))
        {
            var service = new RoomService(context);
            var updateRequest = new UpdateRoomRequest("New Name", RoomStatus.Occupied);

            // Act
            await service.UpdateRoomAsync(1, updateRequest);
        }

        // Assert
        using (var context = new AppDbContext(options))
        {
            var room = await context.Rooms.FindAsync(1);
            Assert.NotNull(room);
            Assert.Equal("New Name", room.Name);
            Assert.Equal(RoomStatus.Occupied, room.Status);
        }
    }

    [Fact]
    public async Task CreateRoomAsync_Should_CreateRoom()
    {
        var options = CreateNewContextOptions();

        using (var context = new AppDbContext(options))
        {
            var service = new RoomService(context);
            var request = new CreateRoomRequest("Room 101", RoomStatus.Available);

            // Act
            var createdRoom = await service.CreateRoomAsync(request);

            // Assert
            Assert.NotNull(createdRoom);
            Assert.NotEqual(0, createdRoom.Id);
            Assert.Equal("Room 101", createdRoom.Name);
        }

        // Verify Persistence
        using (var context = new AppDbContext(options))
        {
            Assert.Equal(1, await context.Rooms.CountAsync());
        }
    }

    [Fact]
    public async Task GetRoomByIdAsync_Should_ReturnRoom_When_Exists()
    {
        var options = CreateNewContextOptions();
        using (var context = new AppDbContext(options))
        {
            context.Rooms.Add(new Room { Id = 1, Name = "Room 101", Status = RoomStatus.Available });
            await context.SaveChangesAsync();
        }

        using (var context = new AppDbContext(options))
        {
            var service = new RoomService(context);
            var room = await service.GetRoomByIdAsync(1);

            Assert.NotNull(room);
            Assert.Equal(1, room.Id);
        }
    }

    [Fact]
    public async Task DeleteRoomAsync_Should_DeleteRoom_When_Exists()
    {
        var options = CreateNewContextOptions();
        using (var context = new AppDbContext(options))
        {
            context.Rooms.Add(new Room { Id = 1, Name = "Room 101", Status = RoomStatus.Available });
            await context.SaveChangesAsync();
        }

        using (var context = new AppDbContext(options))
        {
            var service = new RoomService(context);
            await service.DeleteRoomAsync(1);
        }

        using (var context = new AppDbContext(options))
        {
            var room = await context.Rooms.FindAsync(1);
            Assert.Null(room);
        }
    }
}
