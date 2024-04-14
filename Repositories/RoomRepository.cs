using _5s.Context;
using _5s.Model;
using MongoDB.Driver;

namespace _5s.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly DapperContext _context;
        private readonly IMongoCollection<Room> _roomCollection;

        public RoomRepository(DapperContext context)
        {
            _context = context;
            _roomCollection = _context.GetDatabase().GetCollection<Room>("Rooms");
        }

        public async Task<string> CreateRoom(Room room)
        {
            await _roomCollection.InsertOneAsync(room);
            return room.Id; // Assuming Id is assigned by MongoDB
        }

        public async Task DeleteRoom(string id)
        {
            await _roomCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            return await _roomCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Room> GetRoomById(string id)
        {
            return await _roomCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Room> GetRoomByRoomNumber(string roomNumber)
        {
            return await _roomCollection.Find(x => x.RoomNumber == roomNumber).FirstOrDefaultAsync();
        }

        public async Task<string> UpdateRoom(string id, Room updatedRoom)
        {
             updatedRoom.Id = id;
            
            // Perform the update without modifying the _id field
            var filter = Builders<Room>.Filter.Eq(x => x.Id, id);
            var result = await _buildingCollection.ReplaceOneAsync(filter, updatedRoom);

            // Check if any document was modified
            return result.ModifiedCount > 0 ? "1" : "0";
        }

        public DapperContext Context => _context;
    }
}
