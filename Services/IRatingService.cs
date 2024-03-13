using _5s.Model;

namespace _5s.Services
{
    public interface IRatingService
    {
        /// <summary>
        /// Create ratings
        /// </summary>
        /// <param name="ratings">Ratings detail</param>
        /// <returns>Returns new rating Id</returns>
        public Task<string> CreateRatings(Ratings ratings);
        /// <summary>
        /// Get all ratings
        /// </summary>
        /// <returns>Returns a list of all ratings detail</returns>
        public Task<IEnumerable<Ratings>> GetAllRatings();
        /// <summary>
        /// Get ratings by Id
        /// </summary>
        /// <param name="id">id of exisiting rating</param>
        /// <returns>Return ratings detail</returns>
        public Task<Ratings> GetRatingsById(string id);
        // /// <summary>
        // /// Get Ratings by Name
        // /// </summary>
        // /// <param name="id">name of existing rating</param>
        // /// <returns>Return ratings detail</returns>
        // public Task<Ratings> GetRatingsByName(string id);
        /// <summary>
        /// Update an exisiting rating
        /// </summary>
        /// <param name="id">id of existing rating</param>
        /// <param name="updatedRatings">details of new rating</param>
        /// <returns>Returns Id of the newly updated ratings</returns>
        public Task<string> UpdateRatings(string id, Ratings updatedRatings);
        /// <summary>
        /// Delete rating
        /// </summary>
        /// <param name="id">id of existing rating</param>
        public Task DeleteRatings(string id);
    }
}
