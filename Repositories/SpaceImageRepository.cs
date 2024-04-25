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
        public async Task<SpaceImage> GetSpaceImageById(string id)
        {
            return await _spaceImageCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
         public async Task<string> UpdateSpaceImage(string id, SpaceImage updatedSpaceImage)
        {
          updatedSpaceImage.Id = id;
            
            // Perform the update without modifying the _id field
            var filter = Builders<SpaceImage>.Filter.Eq(x => x.Id, id);
            var result = await _spaceImageCollection.ReplaceOneAsync(filter, updatedSpaceImage);

            // Check if any document was modified
            return result.ModifiedCount > 0 ? "1" : "0";
        }
    }
}
