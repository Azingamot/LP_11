using System.ComponentModel.DataAnnotations;

public class Post
{
	[Key]
	public int Id { get; set; }
	public string Name { get; set; }
	public int Salary { get; set; }
}
