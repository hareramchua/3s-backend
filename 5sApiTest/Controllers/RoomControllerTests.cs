using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _5s.Controllers;
using _5s.Model;
using _5s.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace _5sApiTest.Controllers
{
    public class RoomControllerTests
    {
        private readonly Mock<IRoomService> _roomServiceMock;
        private readonly RoomController _roomController;

        public RoomControllerTests()
        {
            _roomServiceMock = new Mock<IRoomService>();
            _roomController = new RoomController(_roomServiceMock.Object);
        }

        [Fact]
        public async Task CreateRoom_ValidRoom_ReturnsCreatedAtRouteResult()
        {
            // Arrange
            var newRoom = new Room
            {
                Id = 1,
                BuildingId = 1,
                RoomNumber = "101",
                Image = new byte[] { 0x12, 0x34, 0x56, 0x78 },
                Status = "Active"
            };

            _roomServiceMock.Setup(service => service.CreateRoom(newRoom))
                .ReturnsAsync(1);

            // Act
            var result = await _roomController.CreateRoom(newRoom) as CreatedAtRouteResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("GetRoomById", result.RouteName);
            Assert.Equal(1, result.RouteValues["id"]);
            Assert.Equal(201, result.StatusCode);
            Assert.NotNull(result.Value);
            Assert.IsType<int>(result.Value);
        }

        [Fact]
        public async Task CreateRoom_InvalidModelState_ReturnsBadRequest()
        {
            // Arrange
            var room = new Room
            {
                Id = 1,
                RoomNumber = null,
            };

            var controller = new RoomController(_roomServiceMock.Object);
            controller.ModelState.AddModelError("RoomNumber", "RoomNumber is required");

            // Act
            var result = await controller.CreateRoom(room) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task CreateRoom_ServiceError_ReturnsInternalServerError()
        {
            // Arrange
            var room = new Room
            {
                BuildingId = -1,
            };

            _roomServiceMock.Setup(service => service.CreateRoom(room)).ThrowsAsync(new Exception("Service error"));

            var controller = new RoomController(_roomServiceMock.Object);

            // Act
            var result = await controller.CreateRoom(room) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task GetAllRoom_ReturnsOkObjectResult()
        {
            // Arrange
            var rooms = new List<Room>
            {
                new Room
                {
                    Id = 1,
                    BuildingId = 1,
                    RoomNumber = "101",
                    Image = new byte[] { 0x12, 0x34, 0x56, 0x78 },
                    Status = "Active"
                }
            };

            _roomServiceMock.Setup(service => service.GetAllRoom())
                .ReturnsAsync(rooms);

            // Act
            var result = await _roomController.GetRoom() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public async Task GetAllRoom_NoRoomsFound_ReturnsNotFound()
        {
            // Arrange
            var emptyRoomList = new List<Room>();
            _roomServiceMock.Setup(service => service.GetAllRoom()).ReturnsAsync(emptyRoomList);
            var controller = new RoomController(_roomServiceMock.Object);

            // Act
            var result = await controller.GetRoom() as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GetAllRoom_ServiceError_ReturnsInternalServerError()
        {
            // Arrange
            _roomServiceMock.Setup(service => service.GetAllRoom()).ThrowsAsync(new Exception("Service error"));
            var controller = new RoomController(_roomServiceMock.Object);

            // Act
            var result = await controller.GetRoom() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task GetRoomById_ExistingId_ReturnsOkObjectResult()
        {
            // Arrange
            int roomId = 1;
            var room = new Room
            {
                Id = 1,
                BuildingId = 1,
                RoomNumber = "101",
                Image = new byte[] { 0x12, 0x34, 0x56, 0x78 },
                Status = "Active"
            };

            _roomServiceMock.Setup(service => service.GetRoomById(roomId))
                .ReturnsAsync(room);

            // Act
            var result = await _roomController.GetRoom(roomId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public async Task GetRoomById_NonExistingRoomId_ReturnsNotFound()
        {
            // Arrange
            int nonExistingId = 2;
            var rooms = new List<Room>
            {
                new Room
                {
                    Id = 1,
                    BuildingId = 1,
                    RoomNumber = "101",
                    Image = new byte[] { 0x12, 0x34, 0x56, 0x78 },
                    Status = "Active"
                }
            };

            _roomServiceMock.Setup(service => service.GetRoomById(nonExistingId)).ReturnsAsync((Room)null);
            var controller = new RoomController(_roomServiceMock.Object);

            // Act
            var actionResult = await controller.GetRoom(nonExistingId);

            // Assert
            var result = Assert.IsType<NotFoundResult>(actionResult);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task GetRoomById_ServiceError_ReturnsInternalServerError()
        {
            // Arrange
            int roomId = 999999;
            _roomServiceMock.Setup(service => service.GetRoomById(roomId)).ThrowsAsync(new Exception("Service error"));
            var controller = new RoomController(_roomServiceMock.Object);

            // Act
            var result = await controller.GetRoom(roomId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task UpdateRoom_ExistingRoom_ReturnsOkObjectResult()
        {
            // Arrange
            int roomId = 1;
            var room = new Room
            {
                Id = 1,
                BuildingId = 1,
                RoomNumber = "101",
                Image = new byte[] { 0x12, 0x34, 0x56, 0x78 },
                Status = "Green"
            };

            _roomServiceMock.Setup(service => service.GetRoomById(roomId))
                            .ReturnsAsync(room);

            _roomServiceMock.Setup(service => service.UpdateRooms(roomId, room))
                            .ReturnsAsync(1);

            // Act
            var result = await _roomController.UpdateRoom(roomId, room) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value);
            Assert.IsType<int>(result.Value);
        }

        [Fact]
        public async Task UpdateRoom_NonExistingRoom_ReturnsNotFound()
        {
            // Arrange
            int roomId = 1;
            var room = new Room
            {
                Id = 1,
                BuildingId = 1,
                RoomNumber = "101",
                Image = new byte[] { 0x12, 0x34, 0x56, 0x78 },
                Status = "Green"
            };

            _roomServiceMock.Setup(service => service.GetRoomById(roomId))
                            .ReturnsAsync((Room)null); // Simulating non-existing room

            // Act
            var result = await _roomController.UpdateRoom(roomId, room) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task UpdateRoom_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            int roomId = 1;
            var room = new Room
            {
                Id = 1,
                BuildingId = 1,
                RoomNumber = "101",
                Image = new byte[] { 0x12, 0x34, 0x56, 0x78 },
                Status = "Active"
            };

            _roomServiceMock.Setup(service => service.UpdateRooms(roomId, room))
                .ReturnsAsync(0);

            // Act
            var result = await _roomController.UpdateRoom(roomId, room) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task UpdateRoom_ServiceError_ReturnsInternalServerError()
        {
            // Arrange
            int roomId = -1;
            var roomToUpdate = new Room();

            _roomServiceMock.Setup(service => service.GetRoomById(roomId)).ReturnsAsync(new Room());
            _roomServiceMock.Setup(service => service.UpdateRooms(roomId, roomToUpdate)).ThrowsAsync(new Exception("Service error"));

            var controller = new RoomController(_roomServiceMock.Object);

            // Act
            var result = await controller.UpdateRoom(roomId, roomToUpdate) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task DeleteRoom_ExistingRoomNumber_ReturnsOkResultWithSuccessMessage()
        {
            // Arrange
            string roomNumber = "101";
            var existingRoom = new Room
            {
                Id = 1,
                BuildingId = 1,
                RoomNumber = roomNumber,
                Image = new byte[] { 0x12, 0x34, 0x56, 0x78 },
                Status = "Active"
            };

            _roomServiceMock.Setup(service => service.GetRoomByRoomNumber(roomNumber))
                .ReturnsAsync(existingRoom);

            _roomServiceMock.Setup(service => service.DeleteRoom(existingRoom.Id))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _roomController.DeleteRoom(roomNumber) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal("Room successfully deleted", result.Value);
        }

        [Fact]
        public async Task DeleteRoom_NonExistingRoomNumber_ReturnsNotFound()
        {
            // Arrange
            string roomNumber = "999";

            _roomServiceMock.Setup(service => service.GetRoomByRoomNumber(roomNumber))
                .ReturnsAsync((Room)null);

            // Act
            var result = await _roomController.DeleteRoom(roomNumber) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task DeleteRoom_ServiceError_ReturnsInternalServerError()
        {
            // Arrange
            string notExisting = "random room number";
            var roomToDelete = new Room { Id = 1 };

            _roomServiceMock.Setup(service => service.GetRoomByRoomNumber(notExisting)).ReturnsAsync(roomToDelete);
            _roomServiceMock.Setup(service => service.DeleteRoom(roomToDelete.Id)).ThrowsAsync(new Exception("Service error"));

            var controller = new RoomController(_roomServiceMock.Object);

            // Act
            var result = await controller.DeleteRoom(notExisting) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }
    }
}