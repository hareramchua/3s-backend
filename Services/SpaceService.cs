using _5s.Model;
using _5s.Repositories;

namespace _5s.Services
{
    public class SpaceService : ISpaceService
    {
        private readonly ISpaceRepository _spaceRepository;
        public SpaceService(ISpaceRepository spaceRepository)
        {
            _spaceRepository = spaceRepository;
        }

        public async Task<string> CreateSpace(Space space)
        {
            var spaceModel = new Space
            {
                Name = space.Name,
                RoomId = space.RoomId,
                Standard = space.Standard,
            };

            return await _spaceRepository.CreateSpace(spaceModel);
        }

        public async Task DeleteSpace(string id)
        {
            await _spaceRepository.DeleteSpace(id);
        }

        public Task<IEnumerable<Space>> GetAllSpace()
        {
            return _spaceRepository.GetAllSpace();
        }

        public async Task<Space> GetSpaceById(string id)
        {
            return await _spaceRepository.GetSpaceById(id);
        }
        public async Task<Space> GetSpaceByName(string name) 
        {
            return await _spaceRepository.GetSpaceByName(name); 
        }

        public async Task<string> UpdateViewedDate(string id, Space space){
            space.ViewedDate = DateTime.Now;

            var updated = await _spaceRepository.UpdateSpace(id, space);
            return updated;
        }

        public async Task<string> UpdateAssessedDate(string id, Space space){
            space.AssessedDate = DateTime.Now;

            var updated = await _spaceRepository.UpdateSpace(id, space);
            return updated;
        }

        public async Task<string> UpdateSpace(string id, Space updatedSpace)
        {
            var updateSpace = new Space
            {
                Id = id,
                Name = updatedSpace.Name,
                Standard = updatedSpace.Standard,
            };

            var updated = await _spaceRepository.UpdateSpace(id, updatedSpace);

            return updated;
        }
    }
}
