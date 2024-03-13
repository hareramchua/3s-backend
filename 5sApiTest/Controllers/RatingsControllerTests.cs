using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using _5s.Controllers;
using _5s.Model;
using _5s.Services;
using Moq;
using Xunit;

namespace _5sApiTest.Controllers
{
    public class RatingsControllerTests
    {
        private readonly Mock<IRatingService> _ratingsServiceMock;
        private readonly RatingsController _ratingsController;

        public RatingsControllerTests()
        {
            _ratingsServiceMock = new Mock<IRatingService>();
            _ratingsController = new RatingsController(_ratingsServiceMock.Object);
        }

        [Fact]
        public async Task CreateRatings_ValidRatings_ReturnsCreatedAtRouteResult()
        {
            // Arrange
            var ratings = new Ratings {
                Id = 1,
                Sort = 5,
                SetInOrder = 5,
                Shine = 5,
                Standarize = 5,
                Sustain = 5,
                Security = 5,
                isActive = true,
                DateModified = DateTime.Now,
            };
            _ratingsServiceMock.Setup(service => service.CreateRatings(ratings)).ReturnsAsync(1);

            // Act
            var result = await _ratingsController.CreateRatings(ratings) as CreatedAtRouteResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
            Assert.Equal("GetRatingsById", result.RouteName);
            Assert.Equal(1, result.RouteValues["id"]);
        }

        [Fact]
        public async Task CreateRatings_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var invalidRating = new Ratings();
            _ratingsController.ModelState.AddModelError("PropertyName", "Error message");

            // Act
            var result = await _ratingsController.CreateRatings(invalidRating) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task CreateRatings_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var ratings = new Ratings();
            _ratingsServiceMock.Setup(service => service.CreateRatings(ratings)).ThrowsAsync(new Exception());

            // Act
            var result = await _ratingsController.CreateRatings(ratings) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task GetAllRatings_ReturnsOkResult()
        {
            // Arrange
            var ratingsList = new[]
            {
                new Ratings
                {
                    Id = 1,
                    Sort = 4.5f,
                    SetInOrder = 3.8f,
                    Shine = 4.0f,
                    Standarize = 4.5f,
                    Sustain = 4.2f,
                    Security = 4.1f,
                    isActive = true,
                    DateModified = DateTime.Now,
                    SpaceId = 5
                },
                
                new Ratings
                {
                    Id = 2,
                    Sort = 5.0f,
                    SetInOrder = 8.1f,
                    Shine = 9.0f,
                    Standarize = 7.5f,
                    Sustain = 6.2f,
                    Security = 5.1f,
                    isActive = true,
                    DateModified = DateTime.Now,
                    SpaceId = 4
                }
            };
            _ratingsServiceMock.Setup(service => service.GetAllRatings()).ReturnsAsync(ratingsList);

            // Act
            var result = await _ratingsController.GetRatings() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(ratingsList, result.Value);
        }

        [Fact]
        public async Task GetAllRatings_NoRatingsFound_ReturnsNotFound()
        {
            // Arrange
            List<Ratings> emptyRatingList = new List<Ratings>(); // Empty ratings list
            _ratingsServiceMock.Setup(service => service.GetAllRatings()).ReturnsAsync(emptyRatingList);
            var controller = new RatingsController(_ratingsServiceMock.Object);

            // Act
            var result = await controller.GetRatings() as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GetAllRatings_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            _ratingsServiceMock.Setup(service => service.GetAllRatings()).ThrowsAsync(new Exception());

            // Act
            var result = await _ratingsController.GetRatings() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task GetRatingsById_ExistingRatings_ReturnsOkResult()
        {
            // Arrange
            var ratingsId = 1;
            var existingRatings = new Ratings {
                Id = 1,
                Sort = 5,
                SetInOrder = 5,
                Shine = 5,
                Standarize = 5,
                Sustain = 5,
                Security = 5,
                isActive = true,
                DateModified = DateTime.Now,
            };
            _ratingsServiceMock.Setup(service => service.GetRatingsById(ratingsId)).ReturnsAsync(existingRatings);

            // Act
            var result = await _ratingsController.GetRatings(ratingsId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(existingRatings, result.Value);
        }

        [Fact]
        public async Task GetRatingsById_NonExistingRatings_ReturnsNotFound()
        {
            // Arrange
            var ratingsId = 999;
            _ratingsServiceMock.Setup(service => service.GetRatingsById(ratingsId)).ReturnsAsync((Ratings)null);
            var controller = new RatingsController(_ratingsServiceMock.Object);
                
            // Act
            var result = await controller.GetRatings(ratingsId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GetRatingsById_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var ratingId = 1; // Replace with an ID that triggers an exception
            _ratingsServiceMock.Setup(service => service.GetRatingsById(ratingId)).ThrowsAsync(new Exception());
            var controller = new RatingsController(_ratingsServiceMock.Object);

            // Act
            var result = await controller.GetRatings(ratingId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task UpdateRatings_ExistingRatings_ReturnsOkResult()
        {
            // Arrange
            var ratingsId = 1; // Replace with an existing ratings ID
            var existingRatings = new Ratings
            {
                Id = 1,
                Sort = 3.5f,
                SetInOrder = 4.2f,
                Shine = 2.8f,
                Standarize = 4.0f,
                Sustain = 4.5f,
                Security = 3.7f,
                isActive = true,
                DateModified = DateTime.Now,
                SpaceId = 10
            };

            var updatedRatings = new Ratings
            {
                Id = 1,
                Sort = 4.0f,
                SetInOrder = 3.8f,
                Shine = 3.5f,
                Standarize = 4.2f,
                Sustain = 4.5f,
                Security = 4.0f,
                isActive = true,
                DateModified = DateTime.Now,
                SpaceId = 15
            };

            _ratingsServiceMock.Setup(service => service.GetRatingsById(ratingsId)).ReturnsAsync(existingRatings);
            _ratingsServiceMock.Setup(service => service.UpdateRatings(ratingsId, updatedRatings)).ReturnsAsync(1);

            // Act
            var result = await _ratingsController.UpdateRatings(ratingsId, updatedRatings) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(1, result.Value);
        }

        [Fact]
        public async Task UpdateRatings_NonExistingRatings_ReturnsNotFound()
        {
            // Arrange
            var ratingsId = 999;
            var updatedRatings = new Ratings
            {
                Id = 1,
                Sort = 4.0f,
                SetInOrder = 3.8f,
                Shine = 3.5f,
                Standarize = 4.2f,
                Sustain = 4.5f,
                Security = 4.0f,
                isActive = true,
                DateModified = DateTime.Now,
                SpaceId = 15
            };

            _ratingsServiceMock.Setup(service => service.GetRatingsById(ratingsId)).ReturnsAsync((Ratings)null);

            // Act
            var result = await _ratingsController.UpdateRatings(ratingsId, updatedRatings) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task UpdateRatings_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var ratingsId = 1;
            var updatedRatings = new Ratings();

            _ratingsServiceMock.Setup(service => service.GetRatingsById(ratingsId)).ReturnsAsync(new Ratings());
            _ratingsServiceMock.Setup(service => service.UpdateRatings(ratingsId, updatedRatings)).ThrowsAsync(new Exception());
            var controller = new RatingsController(_ratingsServiceMock.Object);

            // Act
            var result = await controller.UpdateRatings(ratingsId, updatedRatings) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task DeleteRatings_ExistingRatings_ReturnsOkResult()
        {
            // Arrange
            var ratingsId = 1;
            var existingRatings = new Ratings {
                Id = 1,
                Sort = 4.0f,
                SetInOrder = 3.8f,
                Shine = 3.5f,
                Standarize = 4.2f,
                Sustain = 4.5f,
                Security = 4.0f,
                isActive = true,
                DateModified = DateTime.Now,
                SpaceId = 15
            };

            _ratingsServiceMock.Setup(service => service.GetRatingsById(ratingsId)).ReturnsAsync(existingRatings);
            _ratingsServiceMock.Setup(service => service.DeleteRatings(ratingsId)).Returns(Task.CompletedTask);

            // Act
            var result = await _ratingsController.DeleteRatings(ratingsId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal("Ratings successfully deleted", result.Value);
        }

        [Fact]
        public async Task DeleteRatings_NonExistingRatings_ReturnsNotFound()
        {
            // Arrange
            var ratingsId = 999; // Replace with a non-existing ratings ID

            _ratingsServiceMock.Setup(service => service.GetRatingsById(ratingsId)).ReturnsAsync((Ratings)null);

            // Act
            var result = await _ratingsController.DeleteRatings(ratingsId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task DeleteRatings_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var ratingsId = 1; // Replace with an existing ratings ID

            _ratingsServiceMock.Setup(service => service.GetRatingsById(ratingsId)).ThrowsAsync(new Exception());

            // Act
            var result = await _ratingsController.DeleteRatings(ratingsId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }
    }
}