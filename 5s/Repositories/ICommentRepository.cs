using _5s.Model;

namespace _5s.Repositories
{
    public interface ICommentRepository
    {
        /// <summary>
        /// Create Comment
        /// </summary>
        /// <param name="comment">Comment Model</param>
        /// <returns>Return Id of newly created Comment</returns>
        public Task<string> CreateComment(Comment comment);
        /// <summary>
        /// Gets All Comment
        /// </summary>
        /// <returns>Returns a list of all Comments with details</returns>
        public Task<IEnumerable<Comment>> GetAllComment();
        /// <summary>
        /// Get a Comment by Id
        /// </summary>
        /// <param name="id">Comment Id</param>
        /// <returns>Returns a Comment with Details</returns>
        public Task<Comment> GetCommentById(string id);
        /// <summary>
        /// Update an Existing Comment
        /// </summary>
        /// <param name="id">Comment Id</param>
        /// <param name="updateComment">new Comment Details</param>
        /// <returns>Returns Id of the UpdatedComment</returns>
        public Task<string> UpdateComment(string id, Comment updateComment);
        /// <summary>
        /// Delete Comment
        /// </summary>
        /// <param name="id">Comment Id</param>
        public Task DeleteComment(string id);

    }
}
