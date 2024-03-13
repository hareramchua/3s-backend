using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using _5s.Repositories;
using _5s.Services;
using _5s.Model;

namespace _5sApiTest.Services
{
    public class CommentServiceTests
    {
        private readonly Mock<ICommentRepository> _commentRepository;
        private readonly ICommentService _commentService;

        public CommentServiceTests()
        {
            _commentRepository = new Mock<ICommentRepository>();
            _commentService = new CommentService(_commentRepository.Object);
        }

        [Fact]
        public async Task CreateComment_Returns_CommentId()
        {
            // Arrange
            var comment = new Comment(); // Create a sample comment object

            // Mocking repository behavior
            _commentRepository.Setup(repo => repo.CreateComment(It.IsAny<Comment>())).ReturnsAsync(1);
            // Assuming 1 is the comment ID returned upon successful creation

            // Act
            int commentId = await _commentService.CreateComment(comment);

            // Assert
            Assert.Equal(1, commentId); // Check if the returned ID matches the expected ID
            _commentRepository.Verify(repo => repo.CreateComment(It.IsAny<Comment>()), Times.Once);
            // Verify that the repository method was called once with the correct parameter
        }

        [Fact]
        public async Task DeleteComment_ValidId_CallsRepositoryDeleteComment()
        {
            // Arrange
            int commentId = 1;
            _commentRepository.Setup(repo => repo.DeleteComment(commentId)).Returns(Task.CompletedTask);

            // Act
            await _commentService.DeleteComment(commentId);

            // Assert
            _commentRepository.Verify(repo => repo.DeleteComment(commentId), Times.Once);
        }

        [Fact]
        public async Task GetAllComment_ReturnsListOfComments()
        {
            // Arrange
            var comments = new List<Comment>
            {
                new Comment(),
                new Comment(),
                new Comment()
            };

            // Mocking repository behavior
            _commentRepository.Setup(repo => repo.GetAllComment()).ReturnsAsync(comments);

            // Act
            var result = await _commentService.GetAllComment();

            // Assert
            Assert.Equal(comments, result); // Check if the returned list matches the expected list
            _commentRepository.Verify(repo => repo.GetAllComment(), Times.Once);
            // Verify that the repository method was called once
        }

        [Fact]
        public async Task GetCommentById_ReturnsComment()
        {
            // Arrange
            int commentId = 1; // A sample comment ID
            var comment = new Comment
            {
                Id = commentId,
                Sort = "Sort",
                SetInOrder = "SetInOrder",
                Shine = "Shine",
                Standarize = "Standarize",
                Sustain = "Sustain",
                Security = "Security",
            };

            // Mocking repository behavior
            _commentRepository.Setup(repo => repo.GetCommentById(commentId)).ReturnsAsync(comment);

            // Act
            var result = await _commentService.GetCommentById(commentId);

            // Assert
            Assert.Equal(comment, result); // Check if the returned comment matches the expected comment
            _commentRepository.Verify(repo => repo.GetCommentById(commentId), Times.Once);
            // Verify that the repository method was called once with the correct ID
        }

        [Fact]
        public async Task UpdateComment_ValidId_ReturnsUpdatedCommentId()
        {
            // Arrange
            int commentId = 1; // A sample comment ID
            var updateComment = new Comment
            {
                Sort = "Updated Sort",
                SetInOrder = "Updated SetInOrder",
                Shine = "Updated Shine",
                Standarize = "Updated Standarize",
                Sustain = "Updated Sustain",
                Security = "Updated Seiri",
            };

            // Mocking repository behavior
            _commentRepository.Setup(repo => repo.UpdateComment(commentId, It.IsAny<Comment>())).ReturnsAsync(1);
            // Assuming 1 is the comment ID returned upon successful update

            // Act
            int result = await _commentService.UpdateComment(commentId, updateComment);

            // Assert
            Assert.Equal(1, result); // Check if the returned result matches the expected result
            _commentRepository.Verify(repo => repo.UpdateComment(commentId, It.IsAny<Comment>()), Times.Once);
            // Verify that the repository method was called once with the correct ID and updated comment object
        }

        
    }
}