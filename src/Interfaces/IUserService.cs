using System.Collections.Generic;
using System.Threading.Tasks;
using ValidationServiceDotNetCoreSample.Entities;

namespace ValidationServiceDotNetCoreSample.Interfaces
{
    /// <summary>
    /// Service for handling the users
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Authenticates the user.
        /// </summary>
        /// <param name="username">The user to find</param>
        /// <param name="password">The user password</param>
        /// <returns>The user without password or NULL in case the user was not found</returns>
        Task<User> Authenticate(string username, string password);
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>All users</returns>
        Task<IEnumerable<User>> GetAll();
    }
}