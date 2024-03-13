using _5s.Model;
using _5s.Repositories;

namespace _5s.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly IBuildingRepository _buildingRepository;
        public BuildingService(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }
        public async Task<string> CreateBuilding(Building building)
        {
            var buildingModel = new Building
            {
                BuildingName = building.BuildingName,
                BuildingCode = building.BuildingCode,
                Image = building.Image
            };

            return await _buildingRepository.CreateBuilding(buildingModel);
        }

        public async Task DeleteBuilding(string id)
        {
            await _buildingRepository.DeleteBuilding(id);
        }

        public Task<IEnumerable<Building>> GetAllBuilding()
        {
            return _buildingRepository.GetAllBuildings();
        }

        public async Task<Building> GetBuildingById(string id)
        {
            return await _buildingRepository.GetBuildingById(id);
        }
        public async Task<Building> GetBuildingByName(string name)
        {
            return await _buildingRepository.GetBuildingByName(name);
        }

        public async Task<string> UpdateBuilding(string id, Building updatedBuilding)
        {
            var updatedModel = new Building
            {
                BuildingCode = updatedBuilding.BuildingCode,
                BuildingName = updatedBuilding.BuildingName,
                Image = updatedBuilding.Image
            };

            var updated = await _buildingRepository.UpdateBuilding(id, updatedModel);

            return updated;
        }
    }
}
