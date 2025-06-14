using System.ComponentModel.DataAnnotations;

public class UserSession
{
	[Key]
	public int Id { get; set; }
	public User User { get; set; }
	public DateTime LoginTime { get; set; }
}
