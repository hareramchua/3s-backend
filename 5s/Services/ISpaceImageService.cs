using _5s.Model;

namespace _5s.Services
{
    public interface ISpaceImageService
    {
        /// <summary>
        /// Create SpaceImage
        /// </summary>
        /// <param name="spaceImage">Image</param>
        /// <returns>Returns Id of Space Image</returns>
        Task<string> CreateSpaceImage(SpaceImage spaceImage);
        /// <summary>
        /// Delete SpaceImage by Id
        /// </summary>
        /// <param name="id">Id of SpaceImage</param>
        Task DeleteSpaceImage(string id);
        /// <summary>
        /// Gets all SpaceImage by SpaceId
        /// </summary>
        /// <param name="spaceId">Space Id</param>
        /// <returns>Returns All SpaceImage by Id</returns>
        Task<IEnumerable<SpaceImage>> GetAllSpaceImagesBySpaceId(string spaceId);
    }
}
