using _5s.Context;
using _5s.Model;
using Dapper;
using MongoDB.Driver;

namespace _5s.Repositories
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly DapperContext _context;
        private readonly IMongoCollection<Building> _buildingCollection;

        public BuildingRepository(DapperContext context)
        {
            _context = context;
            _buildingCollection = _context.GetDatabase().GetCollection<Building>("Building");
        }

        public async Task<string> CreateBuilding(Building building)
        {
            await _buildingCollection.InsertOneAsync(building);
            return building.Id; // Assuming Id is assigned by MongoDB
        }

        public async Task DeleteBuilding(string id)
        {
            await _buildingCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Building>> GetAllBuildings()
        {
            return await _buildingCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Building> GetBuildingById(string id)
        {
            return await _buildingCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Building> GetBuildingByName(string name)
        {
            return await _buildingCollection.Find(x => x.BuildingName == name).FirstOrDefaultAsync();
        }

        public async Task<Building> GetBuildingByCode(string code)
        {
            return await _buildingCollection.Find(x => x.BuildingCode == code).FirstOrDefaultAsync();
        }

        public async Task<string> UpdateBuilding(string id, Building updatedBuilding)
        {
            updatedBuilding.Id = id;
            
            // Perform the update without modifying the _id field
            var filter = Builders<Building>.Filter.Eq(x => x.Id, id);
            var result = await _buildingCollection.ReplaceOneAsync(filter, updatedBuilding);

            // Check if any document was modified
            return result.ModifiedCount > 0 ? "1" : "0";
        }

        public DapperContext Context => _context;
    }
}
