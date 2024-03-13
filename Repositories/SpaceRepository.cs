using _5s.Context;
using _5s.Model;
using MongoDB.Driver;

namespace _5s.Repositories
{
    public class SpaceRepository : ISpaceRepository
    {
        private readonly DapperContext _context;
        private readonly IMongoCollection<Space> _spaceCollection;

        public SpaceRepository(DapperContext context)
        {
            _context = context;
            _spaceCollection = _context.GetDatabase().GetCollection<Space>("Spaces");
        }

        public DapperContext Context => _context;

        public async Task<string> CreateSpace(Space space)
        {
            await _spaceCollection.InsertOneAsync(space);
            return space.Id; // Assuming Id is assigned by MongoDB
        }

        public async Task DeleteSpace(string id)
        {
            await _spaceCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Space>> GetAllSpace()
        {
            return await _spaceCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Space> GetSpaceById(string id)
        {
            return await _spaceCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Space> GetSpaceByName(string name)
        {
            return await _spaceCollection.Find(x => x.Name == name).FirstOrDefaultAsync();
        }

        public async Task<string> UpdateSpace(string id, Space updatedSpace)
        {
            var result = await _spaceCollection.ReplaceOneAsync(x => x.Id == id, updatedSpace);
            return result.ModifiedCount > 0 ? "1" : "0";
        }
    }
}
