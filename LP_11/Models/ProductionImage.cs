using System.ComponentModel.DataAnnotations;

public class ProductionImage
{
    [Key]
    public int Id { get; set; }
    public Production Production { get; set; }
    public byte[] Image { get; set; }
}