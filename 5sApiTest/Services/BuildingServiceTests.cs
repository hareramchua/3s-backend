using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using _5s.Repositories;
using _5s.Services;
using _5s.Model;
using Microsoft.AspNetCore.Routing;

namespace _5sApiTest.Services
{
    public class BuildingServiceTests
    {
        private readonly Mock<IBuildingRepository> _buildingRepository;
        private readonly IBuildingService _buildingService;

        public BuildingServiceTests()
        {
            _buildingRepository = new Mock<IBuildingRepository>();
            _buildingService = new BuildingService(_buildingRepository.Object);
        }

        [Fact]
        public async Task CreateBuilding_ValidInput_ReturnsId()
        {
            // Arrange
            byte[] newImage = new byte[] { 0x1, 0x2, 0x3 }; // A sample image byte array
            var building = new Building
            {
                BuildingName = "Gregorio L. Escario",
                BuildingCode = "GLE",
                Image = newImage
            };
            _buildingRepository.Setup(repo => repo.CreateBuilding(It.IsAny<Building>())).ReturnsAsync(1); // Mock repository method

            // Act
            var result = await _buildingService.CreateBuilding(building);

            // Assert
            Assert.Equal(1, result); // Assuming the returned ID is 1
        }

        [Fact]
        public async Task DeleteBuilding_ValidId_DeletesBuilding()
        {
            // Arrange
            var buildingIdToDelete = 1;
            _buildingRepository.Setup(repo => repo.DeleteBuilding(It.IsAny<int>())).Verifiable();

            // Act
            await _buildingService.DeleteBuilding(buildingIdToDelete);

            // Assert
            _buildingRepository.Verify(repo => repo.DeleteBuilding(buildingIdToDelete), Times.Once);
        }

        [Fact]
        public async Task GetAllBuilding_ReturnsBuildings()
        {
            // Arrange
            var buildings = new List<Building>
            {
                new Building { Id = 1, BuildingName = "Nicholas Gregorio Escario", BuildingCode = "NGE" },
                new Building { Id = 2, BuildingName = "Gregorio L. Escario", BuildingCode = "GLE" }
            };

            _buildingRepository.Setup(repo => repo.GetAllBuildings()).ReturnsAsync(buildings);

            // Act
            var result = await _buildingService.GetAllBuilding();

            // Assert
            Assert.NotNull(result); // Check if result is not null
            Assert.IsAssignableFrom<IEnumerable<Building>>(result); // Check if result is IEnumerable<Building>
            Assert.Collection(result,
                b => Assert.Equal("Nicholas Gregorio Escario", b.BuildingName),
                b => Assert.Equal("Gregorio L. Escario", b.BuildingName)
            );
        }

        [Fact]
        public async Task GetBuildingById_ExistingId_ReturnsBuilding()
        {
            // Arrange
            int buildingId = 1;
            byte[] existingImage = new byte[] { 0x1, 0x2, 0x3 }; // A sample image byte array
            var expectedBuilding = new Building
            {
                Id = buildingId,
                BuildingName = "Gregorio L. Escario",
                BuildingCode = "GLE",
                Image = existingImage
            };

            _buildingRepository.Setup(repo => repo.GetBuildingById(buildingId)).ReturnsAsync(expectedBuilding);

            // Act
            var result = await _buildingService.GetBuildingById(buildingId);

            // Assert
            Assert.NotNull(result); // Check if result is not null
            Assert.IsType<Building>(result); // Check if the result is of type Building
            Assert.Equal(expectedBuilding.Id, result.Id); // Check if the ID matches
            Assert.Equal(expectedBuilding.BuildingName, result.BuildingName); // Check if the name matches
            Assert.Equal(expectedBuilding.BuildingCode, result.BuildingCode); // Check if the code matches
        }

        [Fact]
        public async Task UpdateBuilding_ValidId_ReturnsUpdatedBuildingId()
        {
            // Arrange
            int buildingId = 1; // A sample building ID
            byte[] updatedImage = new byte[] { 0x1, 0x2, 0x3 }; // A sample image byte array
            Building updatedBuilding = new Building
            {
                Id = buildingId,
                BuildingName = "Updated Name",
                BuildingCode = "Updated Code",
                Image = updatedImage // Assign the updated image byte array
            };

            // Mocking repository behavior
            _buildingRepository.Setup(repo => repo.UpdateBuilding(buildingId, It.IsAny<Building>())).ReturnsAsync(1);
            // Assuming 1 is the building ID returned upon successful update

            // Act
            int result = await _buildingService.UpdateBuilding(buildingId, updatedBuilding);

            // Assert
            Assert.Equal(1, result); // Check if the returned result matches the expected result
            _buildingRepository.Verify(repo => repo.UpdateBuilding(buildingId, It.IsAny<Building>()), Times.Once);
            // Verify that the repository method was called once with the correct ID and updated building object
        }
    }
}
