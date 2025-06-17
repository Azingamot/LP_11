using System.ComponentModel.DataAnnotations;

public class Client
{
	[Key]
	public int Id { get; set; }
	public string Name { get; set; }
	public string? INN { get; set; }
	public string? Email { get; set; }
	public string? PhoneNumber { get; set; }
	public string? Adress { get; set; }

}