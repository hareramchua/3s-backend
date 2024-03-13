using _5s.Context;
using _5s.Model;
using MongoDB.Driver;

namespace _5s.Repositories
{
    public class RatingsRepository : IRatingsRepository
    {
        private readonly DapperContext _context;
        private readonly IMongoCollection<Ratings> _ratingsCollection;

        public RatingsRepository(DapperContext context)
        {
            _context = context;
            _ratingsCollection = _context.GetDatabase().GetCollection<Ratings>("Ratings");
        }

        public async Task<string> CreateRatings(Ratings ratings)
        {
            await _ratingsCollection.InsertOneAsync(ratings);
            return ratings.Id; // Assuming Id is assigned by MongoDB
        }

        public async Task DeleteRatings(string id)
        {
            await _ratingsCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Ratings>> GetAllRatings()
        {
            return await _ratingsCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Ratings> GetRatingsById(string id)
        {
            return await _ratingsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        // public async Task<Ratings> GetRatingsByName(string name)
        // {
        //     return await _ratingsCollection.Find(x => x.Name == name).FirstOrDefaultAsync();
        // }

        public async Task<string> UpdateRatings(string id, Ratings updatedRatings)
        {
            var result = await _ratingsCollection.ReplaceOneAsync(x => x.Id == id, updatedRatings);
            return result.ModifiedCount > 0 ? "1" : "0";
        }

        public DapperContext Context => _context;
    }
}
