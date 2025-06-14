using System.ComponentModel.DataAnnotations;

public class ProductionProductionProperty
{
	[Key]
	public int Id { get; set; }
	public Production Production { get; set; }
	public ProductionCategory ProductionCategory { get; set; }
}

