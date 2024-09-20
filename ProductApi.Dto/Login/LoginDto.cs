namespace ProductApi.Dto.Login
{
    public class LoginDto
	{
        /// <summary>
        /// Kullanıcının giriş yapmak için kullandığı kullanıcı adını temsil eder.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// kullanıcının giriş yapmak için kullandığı şifreyi temsil eder.
        /// </summary>
        public string Password { get; set; }
	}
}
