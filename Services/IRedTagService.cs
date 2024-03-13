using _5s.Model;

namespace _5s.Services
{
    public interface IRedTagService
    {
        /// <summary>
        /// Create redtag model
        /// </summary>
        /// <param name="redtag">Redtag details</param>
        /// <returns>Return newly created redtag</returns>
        public Task<string> CreateRedTag(RedTag redtag);
        /// <summary>
        /// Get all redtag
        /// </summary>
        /// <returns>Returns a list of all redtag with details</returns>
        public Task<IEnumerable<RedTag>> GetAllRedTag();
        /// <summary>
        /// Get redtag by Id
        /// </summary>
        /// <param name="id">Id of an existing redtag</param>
        /// <returns>Return redtag details</returns>
        public Task<RedTag> GetRedTagById(string id);
        /// <summary>
        /// Get redtag by name
        /// </summary>
        /// <param name="name">name of an exising redtag</param>
        /// <returns>Return redtag details</returns>
        public Task<RedTag> GetRedTagByName(string name);
        /// <summary>
        /// Update an exising redtag by id
        /// </summary>
        /// <param name="id">id of an existing redtag</param>
        /// <param name="updatedRedTag">new redtag details</param>
        /// <returns>Return id of the newly updated redtag</returns>
        public Task<string> UpdateRedTag(string id, RedTag updatedRedTag);
        /// <summary>
        /// Delete a redtag by Id
        /// </summary>
        /// <param name="id">Id of an existing redtag</param>
        public Task DeleteRedTag(string id);
    }
}
