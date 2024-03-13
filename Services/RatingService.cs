using _5s.Model;
using _5s.Repositories;

namespace _5s.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingsRepository _repository;
        public RatingService(IRatingsRepository repository)
        {
            _repository = repository;
        }
        public async Task<string> CreateRatings(Ratings ratings)
        {
            var ratingModel = new Ratings
            {
                Id = "",
                Sort = ratings.Sort,
                SetInOrder = ratings.SetInOrder,
                Shine = ratings.Shine,
                Standarize = ratings.Standarize,
                Sustain = ratings.Sustain,
                Security = ratings.Security,
                isActive = ratings.isActive,
                DateModified = DateTime.Now,
                SpaceId = ratings.SpaceId
            };

            return await _repository.CreateRatings(ratingModel);
        }

        public async Task DeleteRatings(string id)
        {
            await _repository.DeleteRatings(id);
        }

        public Task<IEnumerable<Ratings>> GetAllRatings()
        {
            return _repository.GetAllRatings();
        }

        public async Task<Ratings> GetRatingsById(string id)
        {
            return await _repository.GetRatingsById(id);
        }
        // public async Task<Ratings> GetRatingsByName(string id)
        // {
        //     return await _repository.GetRatingsByName(id);
        // }

        public async Task<string> UpdateRatings(string id, Ratings updatedRatings)
        {
            var ratingModel = new Ratings
            {
                Sort = updatedRatings.Sort,
                SetInOrder = updatedRatings.SetInOrder,
                Shine = updatedRatings.Shine,
                Standarize = updatedRatings.Standarize,
                Sustain = updatedRatings.Sustain,
                Security = updatedRatings.Security,
                isActive = updatedRatings.isActive,
                DateModified = DateTime.Now
            };

            return await _repository.UpdateRatings(id, ratingModel);
        }
    }
}
