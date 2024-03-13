using _5s.Context;
using _5s.Model;
using MongoDB.Driver;

namespace _5s.Repositories
{
    public class RedTagRepository : IRedTagRepository
    {
        private readonly DapperContext _context;
        private readonly IMongoCollection<RedTag> _redTagCollection;

        public RedTagRepository(DapperContext context)
        {
            _context = context;
            _redTagCollection = _context.GetDatabase().GetCollection<RedTag>("RedTags");
        }

        public async Task<string> CreateRedTag(RedTag redTag)
        {
            await _redTagCollection.InsertOneAsync(redTag);
            return redTag.Id; // Assuming Id is assigned by MongoDB
        }

        public async Task DeleteRedTag(string id)
        {
            await _redTagCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<RedTag>> GetAllRedTags()
        {
            return await _redTagCollection.Find(_ => true).ToListAsync();
        }

        public async Task<RedTag> GetRedTagById(string id)
        {
            return await _redTagCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<RedTag> GetRedTagByName(string name)
        {
            return await _redTagCollection.Find(x => x.ItemName == name).FirstOrDefaultAsync();
        }

        public async Task<string> UpdateRedTag(string id, RedTag updatedRedTag)
        {
            var result = await _redTagCollection.ReplaceOneAsync(x => x.Id == id, updatedRedTag);
            return result.ModifiedCount > 0 ? "1" : "0";

        }

        public DapperContext Context => _context;
    }
}
