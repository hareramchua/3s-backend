using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _5s.Model;
using _5s.Repositories;
using _5s.Services;
using Moq;
using Xunit;

namespace _5sApiTest.Services
{
    public class RoomServiceTests
    {
        private readonly Mock<IRoomRepository> _roomRepositoryMock;
        private readonly IRoomService _roomService;

        public RoomServiceTests()
        {
            _roomRepositoryMock = new Mock<IRoomRepository>();
            _roomService = new RoomService(_roomRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateRoom_ValidRoom_ReturnsRoomId()
        {
            // Arrange
            var room = new Room { BuildingId = 1, RoomNumber = "101", Image = new byte[] { }, Status = "Red" };
            _roomRepositoryMock.Setup(repo => repo.CreateRoom(It.IsAny<Room>())).ReturnsAsync(1); // Assuming 1 indicates success

            // Act
            var result = await _roomService.CreateRoom(room);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteRoom_ExistingRoomId_DeletesRoom()
        {
            // Arrange
            int roomId = 1;
            _roomRepositoryMock.Setup(repo => repo.DeleteRoom(roomId)).Verifiable();

            // Act
            await _roomService.DeleteRoom(roomId);

            // Assert
            _roomRepositoryMock.Verify(repo => repo.DeleteRoom(roomId), Times.Once);
        }

        [Fact]
        public async Task GetAllRoom_ReturnsListOfRooms()
        {
            // Arrange
            var rooms = new List<Room> { new Room { Id = 1, BuildingId = 1, RoomNumber = "101", Image = new byte[] { }, Status = "Green" } };
            _roomRepositoryMock.Setup(repo => repo.GetAllRooms()).ReturnsAsync(rooms);

            // Act
            var result = await _roomService.GetAllRoom();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result); // Assuming only one room is returned
        }

        [Fact]
        public async Task GetRoomById_ExistingId_ReturnsRoom()
        {
            // Arrange
            int roomId = 1;
            var room = new Room { Id = roomId, BuildingId = 1, RoomNumber = "101", Image = new byte[] { }, Status = "Green" };
            _roomRepositoryMock.Setup(repo => repo.GetRoomById(roomId)).ReturnsAsync(room);

            // Act
            var result = await _roomService.GetRoomById(roomId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(roomId, result.Id);
        }

        [Fact]
        public async Task GetRoomByRoomNumber_ExistingRoomNumber_ReturnsRoom()
        {
            // Arrange
            string roomNumber = "101";
            var room = new Room { Id = 1, BuildingId = 1, RoomNumber = roomNumber, Image = new byte[] { }, Status = "Green" };
            _roomRepositoryMock.Setup(repo => repo.GetRoomByRoomNumber(roomNumber)).ReturnsAsync(room);

            // Act
            var result = await _roomService.GetRoomByRoomNumber(roomNumber);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(roomNumber, result.RoomNumber);
        }

        [Fact]
        public async Task UpdateRooms_ExistingIdAndRoom_ReturnsUpdatedRoomId()
        {
            // Arrange
            int roomId = 1;
            var updatedRoom = new Room { Id = roomId, RoomNumber = "102", Image = new byte[] { }, Status = "Green" };
            _roomRepositoryMock.Setup(repo => repo.UpdateRoom(roomId, It.IsAny<Room>())).ReturnsAsync(1); // Assuming 1 indicates success

            // Act
            var result = await _roomService.UpdateRooms(roomId, updatedRoom);

            // Assert
            Assert.Equal(1, result);
        }
    }
}
