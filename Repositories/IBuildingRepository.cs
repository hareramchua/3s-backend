using _5s.Model;

namespace _5s.Repositories
{
    public interface IBuildingRepository
    {
        /// <summary>
        /// Create a building
        /// </summary>
        /// <param name="building">Building Model</param>
        /// <returns>Returns Id of newly created building</returns>
        public Task<string> CreateBuilding(Building building);
        /// <summary>
        /// Gets all buidling
        /// </summary>
        /// <returns>Returns a list of all building with details</returns>
        public Task<IEnumerable<Building>> GetAllBuildings();
        /// <summary>
        /// Get building by Id
        /// </summary>
        /// <param name="id">Building Id</param>
        /// <returns>Returns building details</returns>
        public Task<Building> GetBuildingById(string id);
        /// <summary>
        /// Get building by Name
        /// </summary>
        /// <param name="name">Building name</param>
        /// <returns>Returns building details</returns>
        public Task<Building> GetBuildingByName(string name);
        /// <summary>
        /// Updates an existing Building
        /// </summary>
        /// <param name="id">Building Id</param>
        /// <param name="updatedBuilding">new Building Details</param>
        /// <returns>Returns the Id of the updated building</returns>
        public Task<string> UpdateBuilding(string id, Building updatedBuilding);
        /// <summary>
        /// Delete a building
        /// </summary>
        /// <param name="id">Building Id</param>
        public Task DeleteBuilding(string id);
    }
}
