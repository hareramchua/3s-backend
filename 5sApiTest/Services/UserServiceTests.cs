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
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateUser_ValidUser_ReturnsUserId()
        {
            // Arrange
            var user = new User
            {
                FirstName = "John",
                LastName = "Doe",
                Username = "johndoe",
                Password = "password",
                Role = "user"
            };

            var userId = 1;
            _userRepositoryMock.Setup(repo => repo.CreateUser(It.IsAny<User>())).ReturnsAsync(userId);

            // Act
            var createdUserId = await _userService.CreateUser(user);

            // Assert
            Assert.Equal(userId, createdUserId);
        }

        [Fact]
        public async Task GetAllUser_ReturnsAllUsers()
        {
            // Arrange
            var userList = new List<User>
            {
                new User { Id = 1, FirstName = "Ralph", LastName = "Laviste", Role = "admin"},
                new User { Id = 2, FirstName = "John", LastName = "Doe", Role = "user"}
            };

            _userRepositoryMock.Setup(repo => repo.GetAllUsers()).ReturnsAsync(userList);

            // Act
            var result = await _userService.GetAllUser();

            // Assert
            Assert.Equal(userList.Count, ((List<User>)result).Count);
        }

        [Fact]
        public async Task GetUserById_ValidId_ReturnsUser()
        {
            // Arrange
            var userId = 1;
            var user = new User { Id = userId, FirstName = "Alice", LastName = "Smith" };

            _userRepositoryMock.Setup(repo => repo.GetUserById(userId)).ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserById(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.Id);
        }

        [Fact]
        public async Task GetUserByName_ValidName_ReturnsUser()
        {
            // Arrange
            var userName = "Alice";
            var user = new User { Id = 1, FirstName = "Alice", LastName = "Smith", Role = "admin" };

            _userRepositoryMock.Setup(repo => repo.GetUserByName(userName)).ReturnsAsync(user);

            // Act
            var result = await _userService.GetUserByName(userName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userName, result.FirstName);
        }

        [Fact]
        public async Task UpdateUser_ValidId_ReturnsUpdatedUserId()
        {
            // Arrange
            var userId = 1;
            var updatedUser = new User { Id = userId, FirstName = "UpdatedFirstName", LastName = "UpdatedLastName", Role = "admin" };

            _userRepositoryMock.Setup(repo => repo.UpdateUser(userId, It.IsAny<User>())).ReturnsAsync(userId);

            // Act
            var result = await _userService.UpdateUser(userId, updatedUser);

            // Assert
            Assert.Equal(userId, result);
        }

        [Fact]
        public async Task DeleteUser_ValidId_DeletesUser()
        {
            // Arrange
            var userId = 1;

            // Act
            await _userService.DeleteUser(userId);

            // Assert
            _userRepositoryMock.Verify(repo => repo.DeleteUser(userId), Times.Once);
        }
    }
}
