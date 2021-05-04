namespace SQLExamples.Models
{
	using System;
    public class Users
    {
		public Guid UserId { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string NameFull { get; set; }
		public string Email { get; set; }
		public bool Available { get; set; }
	}
}