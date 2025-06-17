using System.ComponentModel.DataAnnotations;

public class ProductionProductionProperty
{
	[Key]
	public int Id { get; set; }
	public Production Production { get; set; }
	public ProductionProperty ProductionPropety { get; set; }
	public string Value { get; set; }
}

