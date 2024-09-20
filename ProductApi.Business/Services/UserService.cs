using ProductApi.Business.Interfaces;
using ProductApi.Entity.Concrete;

namespace ProductApi.Business.Services
{
    public class UserService : IUserService
	{

        /// <summary>
		/// Üyelik olmadığı için kullanıcılar içeren bir liste.
		/// </summary>
		
        private readonly List<User> _users = new List<User>
	{
			
		new User { Username = "admin", Password = "password", Role = "Admin" },
		new User { Username = "user", Password = "password", Role = "User" }
	};

        /// <summary>
		/// Kullanıcı adı ve şifre ile kullanıcıyı doğrular.
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <returns></returns>
        public User Authenticate(string username, string password)
		{
			return _users.FirstOrDefault(u => u.Username == username && u.Password == password);
		}
	}
}
