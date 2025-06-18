using System.ComponentModel.DataAnnotations;

public class Orders
{
	[Key]
	public int Id { get; set; }
	public Worker Worker { get; set; }
	public Client Client { get; set; }
	public Production Production { get; set; }
	public Warehouse? Warehouse { get; set; }
	public int Count { get; set; }
	public bool Fulfilled { get; set; }
}