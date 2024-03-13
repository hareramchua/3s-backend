using _5s.Context;
using _5s.Model;
using Dapper;
using MongoDB.Driver;

namespace _5s.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;
        private readonly IMongoCollection<User> _userCollection;

        public UserRepository(DapperContext context)
        {
            _context = context;
            _userCollection = _context.GetDatabase().GetCollection<User>("User");
        }

        public async Task<string> CreateUser(User user)
        {
            await _userCollection.InsertOneAsync(user);
            return user.Id; // Assuming Id is assigned by MongoDB
        }

        public async Task DeleteUser(string id)
        {
            await _userCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userCollection.Find(_ => true).ToListAsync();
        }

        public async Task<User> GetUserById(string id)
        {
            return await _userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByName(string name)
        {
            return await _userCollection.Find(x => x.FirstName == name || x.LastName == name).FirstOrDefaultAsync();
        }

        public async Task<string> UpdateUser(string id, User updatedUser)
        {
            // Ensure the updatedUser's Id matches the id parameter
            updatedUser.Id = id;

            // Perform the update without modifying the _id field
            var filter = Builders<User>.Filter.Eq(x => x.Id, id);
            var result = await _userCollection.ReplaceOneAsync(filter, updatedUser);

            // Check if any document was modified
            return result.ModifiedCount > 0 ? "1" : "0";
        }

        public DapperContext Context => _context;
    }
}
