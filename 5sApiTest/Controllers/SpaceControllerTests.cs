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
    public class SpaceControllerTests
    {
        private readonly Mock<ISpaceService> _spaceServiceMock;
        private readonly SpaceController _spaceController;

        public SpaceControllerTests()
        {
            _spaceServiceMock = new Mock<ISpaceService>();
            _spaceController = new SpaceController(_spaceServiceMock.Object);
        }

        [Fact]
        public async Task CreateSpace_ValidSpace_ReturnsCreatedAtRouteResult()
        {
            // Arrange
            var space = new Space { Id = 1, Name = "Meeting Room", Pictures = new List<byte[]>(), RoomId = 1 };
            _spaceServiceMock.Setup(service => service.CreateSpace(It.IsAny<Space>())).ReturnsAsync(1);
            var controller = new SpaceController(_spaceServiceMock.Object);

            // Act
            var result = await controller.CreateSpace(space) as CreatedAtRouteResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("GetSpaceById", result.RouteName);
            Assert.Equal(1, result.RouteValues["id"]);
        }

        [Fact]
        public async Task CreateSpace_InvalidData_ReturnsBadRequest()
        {
            // Arrange
            var space = new Space
            {
                Id = 0,
                Name = "Table",
                Pictures = new List<byte[]>(),
                RoomId = 1
            };

            var controller = new SpaceController(_spaceServiceMock.Object);
            controller.ModelState.AddModelError("Id", "Id should not be <= 0");

            // Act
            var result = await controller.CreateSpace(space) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task CreateSpace_ServiceError_ReturnsInternalServerError()
        {
            // Arrange
            var space = new Space
            {
                Id = 1,
                Name = "SampleSpace",
                Pictures = new List<byte[]>(),
                RoomId = 2
            };

            _spaceServiceMock.Setup(service => service.CreateSpace(It.IsAny<Space>())).ThrowsAsync(new Exception("Simulated service error"));

            var controller = new SpaceController(_spaceServiceMock.Object);

            // Act
            var result = await controller.CreateSpace(space) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task GetAllSpace_ReturnsOkObjectResult()
        {
            // Arrange
            var spaces = new List<Space> { new Space { Id = 1, Name = "Meeting Room", Pictures = new List<byte[]>(), RoomId = 1 } };
            _spaceServiceMock.Setup(service => service.GetAllSpace()).ReturnsAsync(spaces);

            // Act
            var result = await _spaceController.GetSpace() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public async Task GetAllSpace_NoSpacesFound_ReturnsNotFound()
        {
            // Arrange
            List<Space> emptySpaceList = new List<Space>();
            _spaceServiceMock.Setup(service => service.GetAllSpace()).ReturnsAsync(emptySpaceList);
            var controller = new SpaceController(_spaceServiceMock.Object);

            // Act
            var result = await controller.GetSpace() as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GetAllSpace_ServiceError_ReturnsInternalServerError()
        {
            // Arrange
            _spaceServiceMock.Setup(service => service.GetAllSpace()).ThrowsAsync(new Exception("Simulated service error"));
            var controller = new SpaceController(_spaceServiceMock.Object);

            // Act
            var result = await controller.GetSpace() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task GetSpaceById_ExistingId_ReturnsOkObjectResult()
        {
            // Arrange
            int spaceId = 1;
            var space = new Space { Id = spaceId, Name = "Meeting Room", Pictures = new List<byte[]>(), RoomId = 1 };
            _spaceServiceMock.Setup(service => service.GetSpaceById(spaceId)).ReturnsAsync(space);

            // Act
            var result = await _spaceController.GetSpace(spaceId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public async Task GetSpaceById_NoSpaceFound_ReturnsNotFound()
        {
            // Arrange
            int sampleId = -1;
            _spaceServiceMock.Setup(service => service.GetSpaceById(sampleId)).ReturnsAsync((Space)null);
            var controller = new SpaceController(_spaceServiceMock.Object);

            // Act
            var result = await controller.GetSpace(sampleId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GetSpaceById_ServiceError_ReturnsInternalServerError()
        {
            // Arrange
            int sampleId = 1; // Assuming this ID exists or can be found
            _spaceServiceMock.Setup(service => service.GetSpaceById(sampleId)).ThrowsAsync(new Exception("Simulated service error"));
            var controller = new SpaceController(_spaceServiceMock.Object);

            // Act
            var result = await controller.GetSpace(sampleId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task UpdateSpace_ValidSpace_ReturnsOk()
        {
            // Arrange
            int sampleId = 1;
            var spaceToUpdate = new Space
            {
                Id = sampleId,
                Name = "UpdatedSpace",
                Pictures = new List<byte[]>(),
                RoomId = 2
            };

            _spaceServiceMock.Setup(service => service.GetSpaceById(sampleId)).ReturnsAsync(spaceToUpdate);
            _spaceServiceMock.Setup(service => service.UpdateSpace(sampleId, spaceToUpdate)).ReturnsAsync(1);
            var controller = new SpaceController(_spaceServiceMock.Object);

            // Act
            var result = await controller.UpdateSpace(sampleId, spaceToUpdate) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public async Task UpdateSpace_SpaceNotFound_ReturnsNotFound()
        {
            int nonExistentId = 999;
            var spaceToUpdate = new Space
            {
                Id = nonExistentId,
                Name = "UpdatedSpace",
                Pictures = new List<byte[]>(),
                RoomId = 2
            };

            _spaceServiceMock.Setup(service => service.GetSpaceById(nonExistentId)).ReturnsAsync((Space)null);
            var controller = new SpaceController(_spaceServiceMock.Object);

            // Act
            var result = await controller.UpdateSpace(nonExistentId, spaceToUpdate) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task UpdateSpace_ServiceError_ReturnsInternalServerError()
        {
            // Arrange
            int sampleId = 1;
            var spaceToUpdate = new Space
            {
                Id = sampleId,
                Name = "UpdatedSpace",
                Pictures = new List<byte[]>(),
                RoomId = 2
            };

            _spaceServiceMock.Setup(service => service.GetSpaceById(sampleId)).ReturnsAsync(spaceToUpdate);
            _spaceServiceMock.Setup(service => service.UpdateSpace(sampleId, spaceToUpdate))
                             .ThrowsAsync(new Exception("Simulated service error"));
            var controller = new SpaceController(_spaceServiceMock.Object);

            // Act
            var result = await controller.UpdateSpace(sampleId, spaceToUpdate) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task DeleteSpace_ExistingId_ReturnsOkObjectResult()
        {
            // Arrange
            int spaceId = 1;
            _spaceServiceMock.Setup(service => service.GetSpaceById(spaceId)).ReturnsAsync(new Space { Id = spaceId, Name = "Meeting Room", Pictures = new List<byte[]>(), RoomId = 1 });

            // Act
            var result = await _spaceController.DeleteSpace(spaceId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public async Task DeleteSpace_SpaceNotFound_ReturnsNotFound()
        {
            // Arrange
            int nonExistentId = -1;

            _spaceServiceMock.Setup(service => service.GetSpaceById(nonExistentId)).ReturnsAsync((Space)null);
            var controller = new SpaceController(_spaceServiceMock.Object);

            // Act
            var result = await controller.DeleteSpace(nonExistentId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task DeleteSpace_ServiceError_ReturnsInternalServerError()
        {
            // Arrange
            int sampleId = 1;

            var spaceToDelete = new Space
            {
                Id = sampleId,
                Name = "SpaceToDelete",
                Pictures = new List<byte[]>(),
                RoomId = 2
            };

            _spaceServiceMock.Setup(service => service.GetSpaceById(sampleId)).ReturnsAsync(spaceToDelete);
            _spaceServiceMock.Setup(service => service.DeleteSpace(sampleId))
                             .ThrowsAsync(new Exception("Simulated service error"));
            var controller = new SpaceController(_spaceServiceMock.Object);

            // Act
            var result = await controller.DeleteSpace(sampleId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }
    }
}