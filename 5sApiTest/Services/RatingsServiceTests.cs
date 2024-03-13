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
    public class RatingServiceTests
    {
        private readonly Mock<IRatingsRepository> _ratingsRepositoryMock;
        private readonly IRatingService _ratingService;

        public RatingServiceTests()
        {
            _ratingsRepositoryMock = new Mock<IRatingsRepository>();
            _ratingService = new RatingService(_ratingsRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateRatings_ValidRatings_ReturnsRatingId()
        {
            // Arrange
            var ratings = new Ratings
            {
                Sort = 5.0f,
                SetInOrder = 4.0f,
                Shine = 3.0f,
                Standarize = 4.5f,
                Sustain = 4.0f,
                Security = 3.5f,
                isActive = true,
                DateModified = DateTime.Now,
                SpaceId = 1
            };

            var ratingId = 1;
            _ratingsRepositoryMock.Setup(repo => repo.CreateRatings(It.IsAny<Ratings>())).ReturnsAsync(ratingId);

            // Act
            var createdRatingId = await _ratingService.CreateRatings(ratings);

            // Assert
            Assert.Equal(ratingId, createdRatingId);
        }

        [Fact]
        public async Task DeleteRatings_ValidId_DeletesRating()
        {
            // Arrange
            var ratingId = 1;

            // Act
            await _ratingService.DeleteRatings(ratingId);

            // Assert
            _ratingsRepositoryMock.Verify(repo => repo.DeleteRatings(ratingId), Times.Once);
        }

        [Fact]
        public async Task GetAllRatings_ReturnsAllRatings()
        {
            // Arrange
            var ratingsList = new List<Ratings>
            {
                new Ratings { Id = 1, Sort = 5.0f, SetInOrder = 4.0f, Shine = 3.0f, Standarize = 4.5f, Sustain = 4.0f, Security = 3.5f, isActive = true, DateModified = DateTime.Now, SpaceId = 1 },
                new Ratings { Id = 2, Sort = 4.5f, SetInOrder = 3.5f, Shine = 4.0f, Standarize = 4.0f, Sustain = 3.5f, Security = 4.0f, isActive = true, DateModified = DateTime.Now, SpaceId = 2 }
            };
            _ratingsRepositoryMock.Setup(repo => repo.GetAllRatings()).ReturnsAsync(ratingsList);

            // Act
            var result = await _ratingService.GetAllRatings();

            // Assert
            Assert.Equal(ratingsList.Count, ((List<Ratings>)result).Count);
        }

        [Fact]
        public async Task GetRatingsById_ValidId_ReturnsRatings()
        {
            // Arrange
            var ratingId = 1;
            var rating = new Ratings { Id = ratingId, Sort = 5.0f, SetInOrder = 4.0f, Shine = 3.0f, Standarize = 4.5f, Sustain = 4.0f, Security = 3.5f, isActive = true, DateModified = DateTime.Now, SpaceId = 1 };
            _ratingsRepositoryMock.Setup(repo => repo.GetRatingsById(ratingId)).ReturnsAsync(rating);

            // Act
            var result = await _ratingService.GetRatingsById(ratingId);

            // Assert
            Assert.Equal(ratingId, result.Id);
        }

            [Fact]
        public async Task UpdateRatings_ValidIdAndRatings_ReturnsUpdatedRatingId()
        {
            // Arrange
            var ratingId = 1;
            var updatedRatings = new Ratings { Id = ratingId, Sort = 4.5f, SetInOrder = 4.0f, Shine = 3.5f, Standarize = 4.0f, Sustain = 4.0f, Security = 3.5f, isActive = true, DateModified = DateTime.Now, SpaceId = 1 };
            _ratingsRepositoryMock.Setup(repo => repo.UpdateRatings(ratingId, It.IsAny<Ratings>())).ReturnsAsync(ratingId);

            // Act
            var result = await _ratingService.UpdateRatings(ratingId, updatedRatings);

            // Assert
            Assert.Equal(ratingId, result);
        }
    }
}
