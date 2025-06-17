using System.ComponentModel.DataAnnotations;

public class ProductionProperty
{
	[Key]
	public int Id { get; set; }
	public string Name { get; set; }
}
