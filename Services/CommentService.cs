using _5s.Model;
using _5s.Repositories;

namespace _5s.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public async Task<string> CreateComment(Comment comment)
        {
            var commentModel = new Comment
            {
                Id = "",
                Sort = comment.Sort,
                SetInOrder = comment.SetInOrder,
                Shine = comment.Shine,
                Standarize = comment.Standarize,
                Sustain = comment.Sustain,
                Security = comment.Security,
                isActive = comment.isActive,
                DateModified = DateTime.Now,
                RatingId = comment.RatingId
            };
            return await _commentRepository.CreateComment(commentModel);
        }

        public async Task DeleteComment(string id)
        {
            await _commentRepository.DeleteComment(id);
        }

        public async Task<IEnumerable<Comment>> GetAllComment()
        {
            return await _commentRepository.GetAllComment();
        }

        public async Task<Comment> GetCommentById(string id)
        {
            return await _commentRepository.GetCommentById(id);
        }

        public async Task<string> UpdateComment(string id,Comment updateComment)
        {
            var updatedComment = new Comment
            {
                Id = id,
                Sort = updateComment.Sort,
                SetInOrder = updateComment.SetInOrder,
                Shine = updateComment.Shine,
                Standarize = updateComment.Standarize,
                Sustain = updateComment.Sustain,
                Security = updateComment.Security,
                isActive = updateComment.isActive,
                DateModified = DateTime.Now,
                RatingId = updateComment.RatingId
            };
            return await _commentRepository.UpdateComment(id, updatedComment);
        }
    }
}
