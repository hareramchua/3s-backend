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
    public class SpaceServiceTests
    {
        private readonly Mock<ISpaceRepository> _spaceRepositoryMock;
        private readonly ISpaceService _spaceService;

        public SpaceServiceTests()
        {
            _spaceRepositoryMock = new Mock<ISpaceRepository>();
            _spaceService = new SpaceService(_spaceRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateSpace_ValidSpace_ReturnsSpaceId()
        {
            // Arrange
            var space = new Space { Name = "Board", Pictures = new List<byte[]>(), RoomId = 1 };
            _spaceRepositoryMock.Setup(repo => repo.CreateSpace(It.IsAny<Space>())).ReturnsAsync(1); // Assuming 1 indicates success

            // Act
            var result = await _spaceService.CreateSpace(space);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task DeleteSpace_ExistingSpaceId_DeletesSpace()
        {
            // Arrange
            int spaceId = 1;
            _spaceRepositoryMock.Setup(repo => repo.DeleteSpace(spaceId)).Verifiable();

            // Act
            await _spaceService.DeleteSpace(spaceId);

            // Assert
            _spaceRepositoryMock.Verify(repo => repo.DeleteSpace(spaceId), Times.Once);
        }

        [Fact]
        public async Task GetAllSpace_ReturnsListOfSpaces()
        {
            // Arrange
            var spaces = new List<Space> { new Space { Id = 1, Name = "Chairs", Pictures = new List<byte[]>(), RoomId = 1 } };
            _spaceRepositoryMock.Setup(repo => repo.GetAllSpace()).ReturnsAsync(spaces);

            // Act
            var result = await _spaceService.GetAllSpace();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result); // Assuming only one space is returned
        }

        [Fact]
        public async Task GetSpaceById_ExistingId_ReturnsSpace()
        {
            // Arrange
            int spaceId = 1;
            var space = new Space { Id = spaceId, Name = "Under the Desk", Pictures = new List<byte[]>(), RoomId = 1 };
            _spaceRepositoryMock.Setup(repo => repo.GetSpaceById(spaceId)).ReturnsAsync(space);

            // Act
            var result = await _spaceService.GetSpaceById(spaceId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(spaceId, result.Id);
        }

        [Fact]
        public async Task GetSpaceByName_ExistingName_ReturnsSpace()
        {
            // Arrange
            string spaceName = "Meeting Room";
            var space = new Space { Id = 1, Name = spaceName, Pictures = new List<byte[]>(), RoomId = 1 };
            _spaceRepositoryMock.Setup(repo => repo.GetSpaceByName(spaceName)).ReturnsAsync(space);

            // Act
            var result = await _spaceService.GetSpaceByName(spaceName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(spaceName, result.Name);
        }

        [Fact]
        public async Task UpdateSpace_ExistingIdAndSpace_ReturnsUpdatedSpaceId()
        {
            // Arrange
            int spaceId = 1;
            var updatedSpace = new Space { Id = spaceId, Name = "New Meeting Room", Pictures = new List<byte[]>(), RoomId = 1 };
            _spaceRepositoryMock.Setup(repo => repo.UpdateSpace(spaceId, It.IsAny<Space>())).ReturnsAsync(1); // Assuming 1 indicates success

            // Act
            var result = await _spaceService.UpdateSpace(spaceId, updatedSpace);

            // Assert
            Assert.Equal(1, result);
        }
    }
}