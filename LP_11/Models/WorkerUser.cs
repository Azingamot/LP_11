using System.ComponentModel.DataAnnotations;

public class WorkerUser
{
	[Key]
	public int Id { get; set; }	
	public Worker Worker { get; set; }
	public User User { get; set; }
}