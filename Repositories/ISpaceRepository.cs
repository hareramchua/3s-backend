using _5s.Model;

namespace _5s.Repositories
{
    public interface ISpaceRepository
    {
        /// <summary>
        /// Create Space
        /// </summary>
        /// <param name="space">Space model</param>
        /// <returns>Returns Id of newly created Space</returns>
        public Task<string> CreateSpace(Space space);
        /// <summary>
        /// Get all Space
        /// </summary>
        /// <returns>Returns a list of all Space with details</returns>
        public Task<IEnumerable<Space>> GetAllSpace();
        /// <summary>
        /// Get Space by Id
        /// </summary>
        /// <param name="id">Space Id</param>
        /// <returns>Returns Space details</returns>
        public Task<Space> GetSpaceById(string id);
        /// <summary>
        /// Get Space by Name
        /// </summary>
        /// <param name="name">Space Name</param>
        /// <returns>Returns Space details</returns>
        public Task<Space> GetSpaceByName(string name);
        /// <summary>
        /// Update existing Space
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedSpace"></param>
        /// <returns>Returns Id of updated Space</returns>
        public Task<string> UpdateSpace(string id, Space updatedSpace);
        /// <summary>
        /// Delete Space
        /// </summary>
        /// <param name="id">Space Id</param>
        public Task DeleteSpace(string id);
    }
}
