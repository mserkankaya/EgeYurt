namespace ProductApi.Entity.Concrete
{
	/// <summary>
	/// User varlık nesnesi özellikleri.
	/// </summary>
    public class User
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Role { get; set; } 
	}
}
