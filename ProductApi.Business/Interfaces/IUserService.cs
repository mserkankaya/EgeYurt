using ProductApi.Entity.Concrete;

namespace ProductApi.Business.Interfaces
{
    public interface IUserService
	{
        /// <summary>
        /// kullanıcı adı ve şifre alarak bir kullanıcıyı doğrular. 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        User Authenticate(string username, string password);
        
    }
}
