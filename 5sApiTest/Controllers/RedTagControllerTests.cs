using _5s.Controllers;
using _5s.Model;
using _5s.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace _5sApiTest.Controllers
{
    public class RedTagControllerTests
    {
        private readonly Mock<IRedTagService> _redTagServiceMock;
        private readonly RedTagController _redTagController;

        public RedTagControllerTests()
        {
            _redTagServiceMock = new Mock<IRedTagService>();
            _redTagController = new RedTagController(_redTagServiceMock.Object);
        }

        [Fact]
        public async Task CreateRedTag_ValidRedTag_ReturnsCreatedAtRouteResult()
        {
            // Arrange
            var redTag = new RedTag { ItemName = "Sample Item", Quantity = 5, RoomId = 1 };
            var redTagId = 1;
            _redTagServiceMock.Setup(service => service.CreateRedTag(redTag)).ReturnsAsync(redTagId);

            // Act
            var result = await _redTagController.CreateRedTag(redTag) as CreatedAtRouteResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("GetRedTagById", result.RouteName);
            Assert.Equal(redTag.Id, result.RouteValues["id"]);
            Assert.Equal(redTagId, result.Value);
        }

        [Fact]
        public async Task CreateRedTag_InvalidData_ReturnsBadRequest()
        {
            // Arrange
            var redtag = new RedTag
            {
                ItemName = null,
                Quantity = 0,
                RoomId = -1
            };

            var controller = new RedTagController(_redTagServiceMock.Object);
            controller.ModelState.AddModelError("RedTagName", "ItemName is required");
            controller.ModelState.AddModelError("Quantity", "should be greater than 0");
            controller.ModelState.AddModelError("RoomId", "RoomId should not be lesser than 0");

            // Act
            var result = await controller.CreateRedTag(redtag) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public async Task CreateRedTag_ServiceError_ReturnsInternalServerError()
        {
            // Arrange
            var redtag = new RedTag
            {
                // Set valid RedTag properties
            };

            _redTagServiceMock.Setup(service => service.CreateRedTag(redtag)).ThrowsAsync(new Exception("Server error"));

            var controller = new RedTagController(_redTagServiceMock.Object);

            // Act
            var result = await controller.CreateRedTag(redtag) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task GetRedTag_ReturnsOkResultWithRedTagList()
        {
            // Arrange
            var redTagList = new List<RedTag>
            {
                new RedTag { Id = 1, ItemName = "Sample Item 1", Quantity = 5, RoomId = 1 },
                new RedTag { Id = 2, ItemName = "Sample Item 2", Quantity = 10, RoomId = 2 }
            };
            _redTagServiceMock.Setup(service => service.GetAllRedTag()).ReturnsAsync(redTagList);

            // Act
            var result = await _redTagController.GetRedTag() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            var redTagResult = Assert.IsType<List<RedTag>>(result.Value);
            Assert.Equal(redTagList.Count, redTagResult.Count);
        }

        [Fact]
        public async Task GetAllRedTag_NoRedTagsFound_ReturnsNotFound()
        {
            // Arrange
            List<RedTag> emptyRedTagList = new List<RedTag>();
            _redTagServiceMock.Setup(service => service.GetAllRedTag()).ReturnsAsync(emptyRedTagList);
            var controller = new RedTagController(_redTagServiceMock.Object);

            // Act
            var result = await controller.GetRedTag() as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GetAllRedTag_ServiceError_ReturnsInternalServerError()
        {
            // Arrange
            _redTagServiceMock.Setup(service => service.GetAllRedTag()).ThrowsAsync(new Exception("Server error"));
            var controller = new RedTagController(_redTagServiceMock.Object);

            // Act
            var result = await controller.GetRedTag() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task GetRedTagById_ValidId_ReturnsOkResultWithRedTag()
        {
            // Arrange
            var redTagId = 1;
            var redTag = new RedTag { Id = redTagId, ItemName = "Sample Item", Quantity = 5, RoomId = 1 };
            _redTagServiceMock.Setup(service => service.GetRedTagById(redTagId)).ReturnsAsync(redTag);

            // Act
            var result = await _redTagController.GetRedTag(redTagId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            var redTagResult = Assert.IsType<RedTag>(result.Value);
            Assert.Equal(redTag.Id, redTagResult.Id);
        }

        [Fact]
        public async Task GetRedTagById_NoRedTagFound_ReturnsNoContent()
        {
            // Arrange
            int nonExistingId = 999; // Assuming this ID does not exist
            _redTagServiceMock.Setup(service => service.GetRedTagById(nonExistingId)).ReturnsAsync((RedTag)null);
            var controller = new RedTagController(_redTagServiceMock.Object);

            // Act
            var result = await controller.GetRedTag(nonExistingId) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }

        [Fact]
        public async Task GetRedTagById_ServiceError_ReturnsInternalServerError()
        {
            // Arrange
            int sampleId = -1;
            _redTagServiceMock.Setup(service => service.GetRedTagById(sampleId)).ThrowsAsync(new Exception("Service error"));
            var controller = new RedTagController(_redTagServiceMock.Object);

            // Act
            var result = await controller.GetRedTag(sampleId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task UpdateRedTag_ExistingId_ReturnsOkResultWithUpdatedRedTag()
        {
            // Arrange
            var redTagId = 1;
            var existingRedTag = new RedTag
            {
                Id = 1,
                ItemName = "Existing Item",
                Quantity = 5,
                RoomId = 2
            };
            var updatedRedTag = new RedTag
            {
                Id = redTagId,
                ItemName = "Updated Item",
                Quantity = 8,
                RoomId = 2
            };

            _redTagServiceMock.Setup(service => service.GetRedTagById(redTagId)).ReturnsAsync(existingRedTag);
            _redTagServiceMock.Setup(service => service.UpdateRedTag(redTagId, updatedRedTag)).ReturnsAsync(1);

            var controller = new RedTagController(_redTagServiceMock.Object);

            // Act
            var actionResult = await controller.UpdateRedTag(redTagId, updatedRedTag) as OkObjectResult;

            // Assert
            Assert.NotNull(actionResult);

            var updatedRedTagResult = actionResult.Value as int?;
            Assert.NotNull(updatedRedTagResult);
            Assert.Equal(1, updatedRedTagResult);
        }

        [Fact]
        public async Task UpdateRedTag_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var redTagId = 100;
            var updatedRedTag = new RedTag { Id = redTagId, ItemName = "Updated Item", Quantity = 8, RoomId = 2 };

            _redTagServiceMock.Setup(service => service.GetRedTagById(redTagId)).ReturnsAsync((RedTag)null);

            var controller = new RedTagController(_redTagServiceMock.Object);

            // Act
            var result = await controller.UpdateRedTag(redTagId, updatedRedTag) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task UpdateRedTag_ServiceError_ReturnsInternalServerError()
        {
            // Arrange
            int sampleId = -1;
            RedTag redTagToUpdate = new RedTag();

            _redTagServiceMock.Setup(service => service.GetRedTagById(sampleId)).ReturnsAsync(new RedTag());
            _redTagServiceMock.Setup(service => service.UpdateRedTag(sampleId, redTagToUpdate)).ThrowsAsync(new Exception("Service error"));

            var controller = new RedTagController(_redTagServiceMock.Object);

            // Act
            var result = await controller.UpdateRedTag(sampleId, redTagToUpdate) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task DeleteRedTag_RedTagDeleted_ReturnsOk()
        {
            // Arrange
            string redTagNameToDelete = "RedTagToDelete";

            _redTagServiceMock.Setup(service => service.GetRedTagByName(redTagNameToDelete)).ReturnsAsync(new RedTag());
            _redTagServiceMock.Setup(service => service.DeleteRedTag(It.IsAny<int>())).Returns(Task.CompletedTask);

            var controller = new RedTagController(_redTagServiceMock.Object);

            // Act
            var result = await controller.DeleteRedTag(redTagNameToDelete) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal("RedTag successfully deleted", result.Value);
        }

        [Fact]
        public async Task DeleteRedTag_RedTagNotFound_ReturnsNotFound()
        {
            // Arrange
            string nonExistingRedTagName = "NonExistingRedTag";

            _redTagServiceMock.Setup(service => service.GetRedTagByName(nonExistingRedTagName)).ReturnsAsync((RedTag)null);

            var controller = new RedTagController(_redTagServiceMock.Object);

            // Act
            var result = await controller.DeleteRedTag(nonExistingRedTagName) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task DeleteRedTag_ServiceError_ReturnsInternalServerError()
        {
            // Arrange
            string redTagNameToDelete = "RedTagToDelete";

            _redTagServiceMock.Setup(service => service.GetRedTagByName(redTagNameToDelete)).ReturnsAsync(new RedTag());
            _redTagServiceMock.Setup(service => service.DeleteRedTag(It.IsAny<int>())).ThrowsAsync(new Exception("Service error"));

            var controller = new RedTagController(_redTagServiceMock.Object);

            // Act
            var result = await controller.DeleteRedTag(redTagNameToDelete) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }
    }
}
