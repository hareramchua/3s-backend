using _5s.Context;
using _5s.Model;
using MongoDB.Driver;

namespace _5s.Repositories
{
    public class SpaceImageRepository : ISpaceImageRepository
    {
        private readonly DapperContext _context;
        private readonly IMongoCollection<SpaceImage> _spaceImageCollection;

        public SpaceImageRepository(DapperContext context)
        {
            _context = context;
            _spaceImageCollection = _context.GetDatabase().GetCollection<SpaceImage>("SpaceImage");
        }

        public async Task<string> CreateSpaceImage(SpaceImage spaceImage)
        {
            await _spaceImageCollection.InsertOneAsync(spaceImage);
            return spaceImage.Id; // Assuming Id is assigned by MongoDB
        }

        public async Task DeleteSpaceImage(string id)
        {
            await _spaceImageCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<SpaceImage>> GetAllSpaceImageBySpaceId(string spaceId)
        {
            return await _spaceImageCollection.Find(x => x.SpaceId == spaceId).ToListAsync();
        }
    }
}
