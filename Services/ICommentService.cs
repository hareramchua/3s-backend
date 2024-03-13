using _5s.Model;

namespace _5s.Services
{
    public interface ICommentService
    {
        /// <summary>
        /// Create comment
        /// </summary>
        /// <param name="comment">Comment detail</param>
        /// <returns>id of comment</returns>
        public Task<string> CreateComment(Comment comment);
        /// <summary>
        /// Get all comment
        /// </summary>
        /// <returns>Returns a list of all comment with details</returns>
        public Task<IEnumerable<Comment>> GetAllComment();
        /// <summary>
        /// Get comment by Id
        /// </summary>
        /// <param name="id">Id of Comment</param>
        /// <returns>Return a certain Comment detail</returns>
        public Task<Comment> GetCommentById(string id);
        /// <summary>
        /// Update an existing comment
        /// </summary>
        /// <param name="id">Id of the exising comment</param>
        /// <param name="updateComment">Updated comment details</param>
        /// <returns>Return od of the new Comment</returns>
        public Task<string> UpdateComment(string id, Comment updateComment);
        /// <summary>
        /// Delete a comment by Id
        /// </summary>
        /// <param name="id">Id of the Comment</param>
        public Task DeleteComment(string id);
    }
}
