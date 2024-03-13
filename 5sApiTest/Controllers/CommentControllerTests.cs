using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _5s.Controllers;
using _5s.Services;
using _5s.Model;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace _5sApiTest.Controllers
{
    public class CommentControllerTests
    {
        private readonly Mock<ICommentService> _commentServiceMock;
        private readonly CommentController _commentController;

        public CommentControllerTests()
        {
            _commentServiceMock = new Mock<ICommentService>();
            _commentController = new CommentController(_commentServiceMock.Object);
        }

        [Fact]
        public async Task CreateComment_ValidComment_ReturnsOkResult()
        {
            // Arrange
            var comment = new Comment
            {
                Id = 1,
                Sort = "1",
                SetInOrder = "1",
                Shine = "1",
                Standarize = "1",
                Sustain = "1",
                Security = "1",
                isActive = true,
                DateModified = DateTime.Now,
                RatingId = 1
            };
            _commentServiceMock.Setup(service => service.CreateComment(comment)).ReturnsAsync(1);

            // Act
            var result = await _commentController.CreateComment(comment) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public async Task CreateComment_InvalidComment_ReturnsBadRequest()
        {
            // Arrange
            var invalidComment = new Comment();
            _commentController.ModelState.AddModelError("Sort", "Sort field is required");
            _commentController.ModelState.AddModelError("SetInOrder", "SetInOrder field is required");
            _commentController.ModelState.AddModelError("Shine", "Shine field is required");
            _commentController.ModelState.AddModelError("Standarize", "Standarize field is required");
            _commentController.ModelState.AddModelError("Sustain", "Sustain field is required");
            _commentController.ModelState.AddModelError("Security", "Security field is required");
            _commentController.ModelState.AddModelError("isActive", "isActive field is required");
            _commentController.ModelState.AddModelError("DateModified", "DateModified field is required");
            _commentController.ModelState.AddModelError("RatingId", "RatingId field is required");

            // Act
            var result = await _commentController.CreateComment(invalidComment) as BadRequestObjectResult;

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.IsType<SerializableError>(result.Value);
        }

        [Fact]
        public async Task CreateComment_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var comment = new Comment { /* Define comment properties */ };
            _commentServiceMock.Setup(service => service.CreateComment(comment)).ThrowsAsync(new Exception());
            var controller = new CommentController(_commentServiceMock.Object);

            // Act
            var result = await controller.CreateComment(comment) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task GetAllComment_ReturnsOkResult()
        {
            // Arrange
            var comments = new List<Comment>
            {
                new Comment {
                    Id = 1,
                    Sort = "1",
                    SetInOrder = "1",
                    Shine = "1",
                    Standarize = "1",
                    Sustain = "1",
                    Security = "1",
                    isActive = true,
                    DateModified = DateTime.Now,
                    RatingId = 1
                },
                new Comment {
                    Id = 2,
                    Sort = "test",
                    SetInOrder = "test",
                    Shine = "test",
                    Standarize = "test",
                    Sustain = "test",
                    Security = "test",
                    isActive = true,
                    DateModified = DateTime.Now,
                    RatingId = 1
                }
            };
            _commentServiceMock.Setup(service => service.GetAllComment()).ReturnsAsync(comments);
            var controller = new CommentController(_commentServiceMock.Object);

            // Act
            var result = await controller.GetComment() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsType<List<Comment>>(result.Value);
        }

        [Fact]
        public async Task GetAllComment_NoCommentsFound_ReturnsNotFound()
        {
            // Arrange
            var emptyCommentList = new List<Comment>();
            _commentServiceMock.Setup(service => service.GetAllComment()).ReturnsAsync(emptyCommentList);
            var controller = new CommentController(_commentServiceMock.Object);

            // Act
            var result = await controller.GetComment() as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GetAllComment_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            _commentServiceMock.Setup(service => service.GetAllComment()).ThrowsAsync(new Exception("Something went wrong"));
            var controller = new CommentController(_commentServiceMock.Object);

            // Act
            var result = await controller.GetComment() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task GetCommentById_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var commentId = 1;
            var existingComment = new Comment
            {
                Id = commentId,
                Sort = "test",
                SetInOrder = "test",
                Shine = "test",
                Standarize = "test",
                Sustain = "test",
                Security = "test",
                isActive = true,
                DateModified = DateTime.Now,
                RatingId = 1
            };

            _commentServiceMock.Setup(service => service.GetCommentById(commentId)).ReturnsAsync(existingComment);
            var controller = new CommentController(_commentServiceMock.Object);

            // Act
            var result = await controller.GetComment(commentId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(existingComment, result.Value);
        }

        [Fact]
        public async Task GetCommentById_InvalidCommentId_ReturnsNotFound()
        {
            // Arrange
            int invalidCommentId = 999; // Replace with a non-existent comment ID
            _commentServiceMock.Setup(service => service.GetCommentById(invalidCommentId)).ReturnsAsync((Comment)null);
            var controller = new CommentController(_commentServiceMock.Object);

            // Act
            var result = await controller.GetComment(invalidCommentId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GetCommentById_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var commentId = 1;
            _commentServiceMock.Setup(service => service.GetCommentById(commentId)).ThrowsAsync(new Exception("500 Server Error"));
            var controller = new CommentController(_commentServiceMock.Object);

            // Act
            var result = await controller.GetComment(commentId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task UpdateComment_ExistingComment_ReturnsOkResult()
        {
            // Arrange
            var commentId = 1;
            var existingComment = new Comment
            {
                Id = commentId,
                Sort = "test",
                SetInOrder = "test",
                Shine = "test",
                Standarize = "test",
                Sustain = "test",
                Security = "test",
                isActive = true,
                DateModified = DateTime.Now,
                RatingId = 1
            };
            var updatedComment = new Comment
            {
                Id = commentId,
                Sort = "updated test",
                SetInOrder = "updated test",
                Shine = "updated test",
                Standarize = "updated test",
                Sustain = "updated test",
                Security = "updated test",
                isActive = false,
                DateModified = DateTime.Now,
                RatingId = 2
            };

            _commentServiceMock.Setup(service => service.GetCommentById(commentId)).ReturnsAsync(existingComment);
            _commentServiceMock.Setup(service => service.UpdateComment(commentId, updatedComment)).ReturnsAsync(1);
            var controller = new CommentController(_commentServiceMock.Object);

            // Act
            var result = await controller.UpdateComment(commentId, updatedComment) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(1, result.Value);
        }

        [Fact]
        public async Task UpdateComment_NonExistingComment_ReturnsNotFound()
        {
            // Arrange
            var commentId = 999;
            var updatedComment = new Comment
            {
                Id = commentId,
                Sort = "updated test",
                SetInOrder = "updated test",
                Shine = "updated test",
                Standarize = "updated test",
                Sustain = "updated test",
                Security = "updated test",
                isActive = false,
                DateModified = DateTime.Now,
                RatingId = 2
            };

            _commentServiceMock.Setup(service => service.GetCommentById(commentId)).ReturnsAsync((Comment)null);
            var controller = new CommentController(_commentServiceMock.Object);

            // Act
            var result = await controller.UpdateComment(commentId, updatedComment) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task UpdateComment_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var commentId = 1; // Replace with an existing comment ID
            var updatedComment = new Comment
            {
                Id = commentId,
                Sort = "updated test",
                SetInOrder = "updated test",
                Shine = "updated test",
                Standarize = "updated test",
                Sustain = "updated test",
                Security = "updated test",
                isActive = false,
                DateModified = DateTime.Now,
                RatingId = 2
            };

            _commentServiceMock.Setup(service => service.GetCommentById(commentId)).ThrowsAsync(new Exception());
            var controller = new CommentController(_commentServiceMock.Object);

            // Act
            var result = await controller.UpdateComment(commentId, updatedComment) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task DeleteComment_ExistingComment_ReturnsOkResult()
        {
            // Arrange
            var commentId = 1; // Replace with an existing comment ID
            var existingComment = new Comment
            {
                Id = commentId,
                Sort = "test",
                SetInOrder = "test",
                Shine = "test",
                Standarize = "test",
                Sustain = "test",
                Security = "test",
                isActive = true,
                DateModified = DateTime.Now,
                RatingId = 1
            };

            _commentServiceMock.Setup(service => service.GetCommentById(commentId)).ReturnsAsync(existingComment);
            _commentServiceMock.Setup(service => service.DeleteComment(commentId)).Returns(Task.CompletedTask);
            var controller = new CommentController(_commentServiceMock.Object);

            // Act
            var result = await controller.DeleteComment(commentId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal("Comment successfully deleted", result.Value);
        }

        [Fact]
        public async Task DeleteComment_NonExistingComment_ReturnsNotFound()
        {
            // Arrange
            var commentId = 999; // Replace with a non-existing comment ID

            _commentServiceMock.Setup(service => service.GetCommentById(commentId)).ReturnsAsync((Comment)null);
            var controller = new CommentController(_commentServiceMock.Object);

            // Act
            var result = await controller.DeleteComment(commentId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }
        
        [Fact]
        public async Task DeleteComment_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var commentId = 1;

            _commentServiceMock.Setup(service => service.GetCommentById(commentId)).ThrowsAsync(new Exception());
            var controller = new CommentController(_commentServiceMock.Object);

            // Act
            var result = await controller.DeleteComment(commentId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }
    }
}
