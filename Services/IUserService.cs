using _5s.Model;

namespace _5s.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Create a User
        /// </summary>
        /// <param name="user">User details</param>
        /// <returns>Return Id of the newly created space</returns>
        public Task<string> CreateUser(User user);
        /// <summary>
        /// Get all user by Id
        /// </summary>
        /// <returns>Return a list of all User with details</returns>
        public Task<IEnumerable<User>> GetAllUser();
        /// <summary>
        /// Get User by Id
        /// </summary>
        /// <param name="id">Id of an exising User</param>
        /// <returns>Return User details</returns>
        public Task<User> GetUserById(string id);
        /// <summary>
        /// Get User by Name
        /// </summary>
        /// <param name="name">Name of an existing user</param>
        /// <returns>Return User details</returns>
        public Task<User> GetUserByName(string name);
        /// <summary>
        /// Update an existing User by ID
        /// </summary>
        /// <param name="id">Id of an existing user</param>
        /// <param name="updatedUser">Details og an existing user</param>
        /// <returns>Returns Id of the newly update User</returns>
        public Task<string> UpdateUser(string id, User updatedUser);
        /// <summary>
        /// Delete a User
        /// </summary>
        /// <param name="id">Id of an existing User</param>
        public Task DeleteUser(string id);
    }
}
