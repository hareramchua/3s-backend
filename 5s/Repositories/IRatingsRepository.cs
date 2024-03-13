using _5s.Model;

namespace _5s.Repositories
{
    public interface IRatingsRepository
    {
        /// <summary>
        /// Create Rating
        /// </summary>
        /// <param name="ratings">Rating Model</param>
        /// <returns>Returns Id of newly created Ratings</returns>
        public Task<string> CreateRatings(Ratings ratings);
        /// <summary>
        /// Gets All Ratings
        /// </summary>
        /// <returns>Returns list of all Rating with details</returns>
        public Task<IEnumerable<Ratings>> GetAllRatings();
        /// <summary>
        /// Gets Rating by Id
        /// </summary>
        /// <param name="id">Rating Id</param>
        /// <returns>Returns Rating details</returns>
        public Task<Ratings> GetRatingsById(string id);
        // /// <summary>
        // /// Gets Rating By Name
        // /// </summary>
        // /// <param name="name">Rating Name</param>
        // /// <returns>Returns Rating details</returns>
        // public Task<Ratings> GetRatingsByName(string name);
        /// <summary>
        /// Update and existing Ratings
        /// </summary>
        /// <param name="id">Rating Id</param>
        /// <param name="updatedRatings">new Rating Details</param>
        /// <returns>Returns updated Rating Id</returns>
        public Task<string> UpdateRatings(string id,Ratings updatedRatings);
        /// <summary>
        /// Delete Rating
        /// </summary>
        /// <param name="id">Rating Id</param>
        public Task DeleteRatings(string id);
    }
}
