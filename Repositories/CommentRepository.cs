using _5s.Context;
using _5s.Model;
using MongoDB.Driver;

namespace _5s.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DapperContext _context;
        private readonly IMongoCollection<Comment> _commentCollection;

        public CommentRepository(DapperContext context)
        {
            _context = context;
            _commentCollection = _context.GetDatabase().GetCollection<Comment>("Comments");
        }

        public async Task<string> CreateComment(Comment comment)
        {
            await _commentCollection.InsertOneAsync(comment);
            return comment.Id; // Assuming Id is assigned by MongoDB
        }

        public async Task DeleteComment(string id)
        {
            await _commentCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Comment>> GetAllComment()
        {
            return await _commentCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Comment> GetCommentById(string id)
        {
            return await _commentCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<string> UpdateComment(string id, Comment updatedComment)
        {
            var result = await _commentCollection.ReplaceOneAsync(x => x.Id == id, updatedComment);
            return result.ModifiedCount > 0 ? "1" : "0";
        }

        public DapperContext Context => _context;
    }
}
