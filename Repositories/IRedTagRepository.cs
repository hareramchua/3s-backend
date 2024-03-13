using _5s.Model;

namespace _5s.Repositories
{
    public interface IRedTagRepository
    {
        /// <summary>
        /// Create RedTag
        /// </summary>
        /// <param name="redtag">RedTag Model</param>
        /// <returns>Returns Id of newly created RedTag</returns>
        public Task<string> CreateRedTag(RedTag redtag);
        /// <summary>
        /// Get All Redtag
        /// </summary>
        /// <returns>Returns a list of RedTag with details</returns>
        public Task<IEnumerable<RedTag>> GetAllRedTags();
        /// <summary>
        /// Get RedTag by Id
        /// </summary>
        /// <param name="id">RedTag Id</param>
        /// <returns>Returns RedTag details</returns>
        public Task<RedTag> GetRedTagById(string id);
        /// <summary>
        /// Get RedTag by name
        /// </summary>
        /// <param name="Name">RedTag name</param>
        /// <returns>Returns RedTag details</returns>
        public Task<RedTag> GetRedTagByName(string Name);
        /// <summary>
        /// Update existing Redtag
        /// </summary>
        /// <param name="id">RedTag Id</param>
        /// <param name="updatedRedTag">new RedTag details</param>
        /// <returns>Return Id of updated RedTag</returns>
        public Task<string> UpdateRedTag(string id, RedTag updatedRedTag);
        /// <summary>
        /// Delete RedTag
        /// </summary>
        /// <param name="id">RedTag Id</param>
        public Task DeleteRedTag(string id);
    }
}
