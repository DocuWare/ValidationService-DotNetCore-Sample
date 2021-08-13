using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValidationServiceDotNetCoreSample.Helpers;
using ValidationServiceDotNetCoreSample.Entities;
using ValidationServiceDotNetCoreSample.Interfaces;

namespace ValidationServiceDotNetCoreSample.Services
{
    /// <summary>
    /// Fake user service
    /// </summary>
    public class FakeUserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private readonly List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
        };

        ///<inheritdoc />
        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.Password == password));

            // authentication successful so return user details without password
            return user?.WithoutPassword();
        }

        ///<inheritdoc />
        public async Task<IEnumerable<User>> GetAll()
        {
            return await Task.Run(() => _users.WithoutPasswords());
        }
    }
}