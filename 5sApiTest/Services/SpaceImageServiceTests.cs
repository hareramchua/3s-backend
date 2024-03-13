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
    public class SpaceImageServiceTests
    {
        private readonly Mock<ISpaceImageRepository> _spaceImageRepositoryMock;
        private readonly ISpaceImageService _spaceImageService;

        public SpaceImageServiceTests()
        {
            _spaceImageRepositoryMock = new Mock<ISpaceImageRepository>();
            _spaceImageService = new SpaceImageService(_spaceImageRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateSpaceImage_ValidSpaceImage_ReturnsImageId()
        {
            // Arrange
            var spaceImage = new SpaceImage
            {
                SpaceId = 1,
                Image = new byte[] { 0x1, 0x2, 0x3 },
                UploadedDate = DateTime.Now
            };

            var imageId = 1; // This could be the expected image ID after creation
            _spaceImageRepositoryMock.Setup(repo => repo.CreateSpaceImage(It.IsAny<SpaceImage>())).ReturnsAsync(imageId);

            // Act
            var createdImageId = await _spaceImageService.CreateSpaceImage(spaceImage);

            // Assert
            Assert.Equal(imageId, createdImageId);
        }

        [Fact]
        public async Task DeleteSpaceImage_ValidId_DeletesImage()
        {
            // Arrange
            var imageId = 1;

            // Act
            await _spaceImageService.DeleteSpaceImage(imageId);

            // Assert
            _spaceImageRepositoryMock.Verify(repo => repo.DeleteSpaceImage(imageId), Times.Once);
        }

        [Fact]
        public async Task GetAllSpaceImagesBySpaceId_ValidSpaceId_ReturnsSpaceImages()
        {
            // Arrange
            var spaceId = 1;
            var spaceImages = new List<SpaceImage>
            {
                new SpaceImage { Id = 1, SpaceId = spaceId, Image = new byte[] { 0x1, 0x2 }, UploadedDate = DateTime.Now },
                new SpaceImage { Id = 2, SpaceId = spaceId, Image = new byte[] { 0x3, 0x4 }, UploadedDate = DateTime.Now }
            };

            _spaceImageRepositoryMock.Setup(repo => repo.GetAllSpaceImageBySpaceId(spaceId)).ReturnsAsync(spaceImages);

            // Act
            var result = await _spaceImageService.GetAllSpaceImagesBySpaceId(spaceId);

            // Assert
            Assert.Equal(spaceImages.Count, ((List<SpaceImage>)result).Count); // Check if the count of returned images matches the expected count
        }
    }
}
