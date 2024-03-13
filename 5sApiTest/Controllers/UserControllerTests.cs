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
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly UserController _userController;

        public UserControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _userController = new UserController(_userServiceMock.Object);
        }

        [Fact]
        public async Task CreateUser_ValidUser_ReturnsOkResultWithNewUser()
        {
            // Arrange
            var _userServiceMock = new Mock<IUserService>();

            var newUser = new User
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Role = "admin"
            };

            _userServiceMock.Setup(service => service.CreateUser(newUser))
                .ReturnsAsync((User user) =>
                {
                    return 1;
                });

            var _userController = new UserController(_userServiceMock.Object);

            // Act
            var result = await _userController.CreateUser(newUser) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value);
            Assert.IsType<int>(result.Value);
        }

        [Fact]
        public async Task CreateUser_ServiceError_ReturnsInternalServerError()
        {
            // Arrange
            var newUser = new User {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Role = "admin"
            };
            _userServiceMock.Setup(service => service.CreateUser(newUser))
                            .ThrowsAsync(new Exception("Simulated service error"));

            var controller = new UserController(_userServiceMock.Object);

            // Act
            var result = await controller.CreateUser(newUser) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task GetAllUser_ExistingUsers_ReturnsOkResultWithUsers()
        {
            // Arrange
            var expectedUsers = new List<User> {
                new User { Id = 1, FirstName = "John", LastName = "Doe", Role = "user" },
                new User { Id = 2, FirstName = "Juan", LastName = "Dela Cruz", Role = "admin" }
            };
            _userServiceMock.Setup(service => service.GetAllUser())
                .ReturnsAsync(expectedUsers);

            // Act
            var result = await _userController.GetUser() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value);
            Assert.IsType<List<User>>(result.Value);
        }

        [Fact]
        public async Task GetAllUser_ServiceError_ReturnsInternalServerError()
        {
            // Arrange
            _userServiceMock.Setup(service => service.GetAllUser())
                            .ThrowsAsync(new Exception("Simulated service error"));

            var controller = new UserController(_userServiceMock.Object);

            // Act
            var result = await controller.GetUser() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task GetUserById_ExistingUserId_ReturnsOkResultWithUser()
        {
            // Arrange
            int userId = 1;
            var expectedUser = new User
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Role = "user"
            };
            _userServiceMock.Setup(service => service.GetUserById(userId))
                .ReturnsAsync(expectedUser);

            // Act
            var result = await _userController.GetUser(userId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value);
            Assert.IsType<User>(result.Value);
        }

        [Fact]
        public async Task GetUserById_ServiceError_ReturnsInternalServerError()
        {
            // Arrange
            int userId = 1;

            _userServiceMock.Setup(service => service.GetUserById(userId))
                            .ThrowsAsync(new Exception("Simulated service error"));

            var controller = new UserController(_userServiceMock.Object);

            // Act
            var result = await controller.GetUser(userId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task UpdateUser_ExistingUserIdAndValidUser_ReturnsOkResultWithUpdatedUser()
        {
            // Arrange
            var updatedUser = new User
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Dela Cruz",
                Role = "admin"
            };

            var existingUser = new User
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Role = "user"
            };

            _userServiceMock.Setup(service => service.GetUserById(1))
                .ReturnsAsync(existingUser);

            _userServiceMock.Setup(service => service.UpdateUser(1, updatedUser))
                .ReturnsAsync(1);

            // Act
            var result = await _userController.UpdateUser(1, updatedUser) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value);
            Assert.IsType<int>(result.Value);
        }

        [Fact]
        public async Task UpdateUser_UserNotFound_ReturnsNotFound()
        {
            // Arrange
            int userId = 1;
            var updatedUser = new User
            {
                Id = userId,
                FirstName = "Updated",
                LastName = "User",
                Role = "admin"
            };

            _userServiceMock.Setup(service => service.GetUserById(userId)).ReturnsAsync((User)null);

            var controller = new UserController(_userServiceMock.Object);

            // Act
            var result = await controller.UpdateUser(userId, updatedUser) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task UpdateUser_ServiceError_ReturnsInternalServerError()
        {
            // Arrange
            int userId = 1;
            var updatedUser = new User
            {
                Id = userId,
                FirstName = "Updated",
                LastName = "User",
                Role = "admin"
            };

            _userServiceMock.Setup(service => service.GetUserById(userId))
                           .ReturnsAsync(new User());

            _userServiceMock.Setup(service => service.UpdateUser(userId, updatedUser)).ThrowsAsync(new Exception("Simulated service error"));

            var controller = new UserController(_userServiceMock.Object);

            // Act
            var result = await controller.UpdateUser(userId, updatedUser) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task DeleteUser_ExistingUserId_ReturnsOkResultWithSuccessMessage()
        {
            // Arrange
            int userId = 1; // Define an existing user ID
            var existingUser = new User
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Role = "user"
            };
            _userServiceMock.Setup(service => service.GetUserById(userId))
                .ReturnsAsync(existingUser);

            // Act
            var result = await _userController.DeleteUser(userId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal("User successfully deleted", result.Value);
        }

        [Fact]
        public async Task DeleteUser_UserNotFound_ReturnsNotFound()
        {
            // Arrange
            int userId = 1;

            _userServiceMock.Setup(service => service.GetUserById(userId)).ReturnsAsync((User)null);

            var controller = new UserController(_userServiceMock.Object);

            // Act
            var result = await controller.DeleteUser(userId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task DeleteUser_ServiceError_ReturnsInternalServerError()
        {
            // Arrange
            int userId = 1;

            _userServiceMock.Setup(service => service.GetUserById(userId))
                            .ReturnsAsync(new User { Id = userId });

            _userServiceMock.Setup(service => service.DeleteUser(userId))
                            .ThrowsAsync(new Exception("Simulated service error"));

            var controller = new UserController(_userServiceMock.Object);

            // Act
            var result = await controller.DeleteUser(userId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }
    }
}
