using _5s.Model;

namespace _5s.Repositories
{
    public interface IRoomRepository
    {
        /// <summary>
        /// Create Room
        /// </summary>
        /// <param name="room">Room Model</param>
        /// <returns>Return Id of newly created Room</returns>
        public Task<string> CreateRoom(Room room);
        /// <summary>
        /// Get All Room
        /// </summary>
        /// <returns>Returns a list of all Room with details</returns>
        public Task<IEnumerable<Room>> GetAllRooms();
        /// <summary>
        /// Get Room by Id
        /// </summary>
        /// <param name="id">Room Id</param>
        /// <returns>Returns Room details</returns>
        public Task<Room> GetRoomById(string id);
        /// <summary>
        /// Get Room by RoomNumber
        /// </summary>
        /// <param name="roomNumber">Room RooomNumber</param>
        /// <returns>Returns Room details</returns>
        public Task<Room> GetRoomByRoomNumber(string roomNumber);
        /// <summary>
        /// Update existing Room
        /// </summary>
        /// <param name="id">Room Id</param>
        /// <param name="updatedRoom">new Room details</param>
        /// <returns>Returns Id of updated Room</returns>
        public Task<string> UpdateRoom(string id, Room updatedRoom);
        /// <summary>
        /// Delete Room
        /// </summary>
        /// <param name="id">Room Id</param>
        public Task DeleteRoom(string id);
    }
}
