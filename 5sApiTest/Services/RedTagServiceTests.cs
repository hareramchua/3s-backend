using _5s.Model;
using _5s.Repositories;
using _5s.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace _5sApiTest.Services
{
    public class RedTagServiceTests
    {
        private readonly Mock<IRedTagRepository> _redTagRepositoryMock;
        private readonly IRedTagService _redTagService;

        public RedTagServiceTests()
        {
            _redTagRepositoryMock = new Mock<IRedTagRepository>();
            _redTagService = new RedTagService(_redTagRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateRedTag_ValidRedTag_ReturnsRedTagId()
        {
            // Arrange
            var redTag = new RedTag
            {
                ItemName = "Sample Item",
                Quantity = 5,
                RoomId = 1
            };

            var redTagId = 1;
            _redTagRepositoryMock.Setup(repo => repo.CreateRedTag(It.IsAny<RedTag>())).ReturnsAsync(redTagId);

            // Act
            var createdRedTagId = await _redTagService.CreateRedTag(redTag);

            // Assert
            Assert.Equal(redTagId, createdRedTagId);
        }

        [Fact]
        public async Task DeleteRedTag_ValidId_DeletesRedTag()
        {
            // Arrange
            var redTagId = 1;

            // Act
            await _redTagService.DeleteRedTag(redTagId);

            // Assert
            _redTagRepositoryMock.Verify(repo => repo.DeleteRedTag(redTagId), Times.Once);
        }

        [Fact]
        public async Task GetAllRedTag_ReturnsAllRedTags()
        {
            // Arrange
            var redTagList = new List<RedTag>
            {
                new RedTag { Id = 1, ItemName = "Sample Item 1", Quantity = 5, RoomId = 1 },
                new RedTag { Id = 2, ItemName = "Sample Item 2", Quantity = 10, RoomId = 2 }
            };
            _redTagRepositoryMock.Setup(repo => repo.GetAllRedTags()).ReturnsAsync(redTagList);

            // Act
            var result = await _redTagService.GetAllRedTag();

            // Assert
            Assert.Equal(redTagList.Count, ((List<RedTag>)result).Count);
        }

        [Fact]
        public async Task GetRedTagById_ValidId_ReturnsRedTag()
        {
            // Arrange
            var redTagId = 1;
            var redTag = new RedTag { Id = redTagId, ItemName = "Sample Item", Quantity = 5, RoomId = 1 };
            _redTagRepositoryMock.Setup(repo => repo.GetRedTagById(redTagId)).ReturnsAsync(redTag);

            // Act
            var result = await _redTagService.GetRedTagById(redTagId);

            // Assert
            Assert.Equal(redTagId, result.Id);
        }

        [Fact]
        public async Task GetRedTagByName_ValidName_ReturnsRedTag()
        {
            // Arrange
            var redTagName = "Sample Item";
            var redTag = new RedTag { Id = 1, ItemName = redTagName, Quantity = 5, RoomId = 1 };
            _redTagRepositoryMock.Setup(repo => repo.GetRedTagByName(redTagName)).ReturnsAsync(redTag);

            // Act
            var result = await _redTagService.GetRedTagByName(redTagName);

            // Assert
            Assert.Equal(redTagName, result.ItemName);
        }

        [Fact]
        public async Task UpdateRedTag_ValidIdAndRedTag_ReturnsUpdatedRedTagId()
        {
            // Arrange
            var redTagId = 1;
            var updatedRedTag = new RedTag { Id = redTagId, ItemName = "Updated Item", Quantity = 8, RoomId = 2 };
            _redTagRepositoryMock.Setup(repo => repo.UpdateRedTag(redTagId, It.IsAny<RedTag>())).ReturnsAsync(redTagId);

            // Act
            var result = await _redTagService.UpdateRedTag(redTagId, updatedRedTag);

            // Assert
            Assert.Equal(redTagId, result);
        }
    }
}
