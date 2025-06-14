using System.ComponentModel.DataAnnotations;

public class Client
{
	[Key]
	public int Id { get; set; }
	public string Name { get; set; }

	[MaxLength(12)]
	public int? INN { get; set; }
	public string? Email { get; set; }
	public string? PhoneNumber { get; set; }
	public string? Adress { get; set; }

}