
using _5s.Model;

namespace _5s.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="userModel">User Model</param>
        /// <returns>Returns Id of newly created User</returns>
        public Task<string> CreateUser(User userModel);
        /// <summary>
        /// Get all User
        /// </summary>
        /// <returns>Returns a list of all User with details</returns>
        public Task<IEnumerable<User>> GetAllUsers();
        /// <summary>
        /// Get User by Id
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>Returns User details</returns>
        public Task<User> GetUserById(string id);
        /// <summary>
        /// Get User by Name
        /// </summary>
        /// <param name="name">User Name</param>
        /// <returns>Returns User details</returns>
        public Task<User> GetUserByName(string name);
        /// <summary>
        /// Update existing User
        /// </summary>
        /// <param name="id">User Id</param>
        /// <param name="updatedUser">new User details</param>
        /// <returns>Returns Id of updated User</returns>
        public Task<string> UpdateUser(string id, User updatedUser);
        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="id">User Id</param>
        public Task DeleteUser(string id);
    }
}
