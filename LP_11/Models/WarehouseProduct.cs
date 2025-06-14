using System.ComponentModel.DataAnnotations;

public class WarehouseProduct
{
	[Key]
	public int Id { get; set; }
	public Production Production { get; set; }
	public int Count { get; set; }
}
