using _5s.Model;

namespace _5s.Services
{
    public interface ISpaceService
    {
        /// <summary>
        /// Create a space
        /// </summary>
        /// <param name="space">Space details</param>
        /// <returns>return Id of newly created Space</returns>
        public Task<string> CreateSpace(Space space);
        /// <summary>
        /// Get all space
        /// </summary>
        /// <returns>Return a list of all Space with details</returns>
        public Task<IEnumerable<Space>> GetAllSpace();
        /// <summary>
        /// Get Space by Id
        /// </summary>
        /// <param name="id">id of an existing Space</param>
        /// <returns>Return space details</returns>
        public Task<Space> GetSpaceById(string id);
        /// <summary>
        /// Get Space by Name
        /// </summary>
        /// <param name="name">Name of an exising Space</param>
        /// <returns>Return space details</returns>
        public Task<Space> GetSpaceByName(string name);
        /// <summary>
        /// Update space by Id
        /// </summary>
        /// <param name="id">Id of an exising space</param>
        /// <param name="updatedSpace">new Space details</param>
        /// <returns>Return Id of newly updated Space</returns>
        public Task<string> UpdateSpace(string id, Space updatedSpace);
        /// <summary>
        /// Delete space
        /// </summary>
        /// <param name="id">Id of an existing space</param>
        public Task DeleteSpace(string id);
    }
}
