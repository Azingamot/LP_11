using System.ComponentModel.DataAnnotations;

public class Worker
{
    [Key]
    public int Id { get; set; }
    public string? FullName { get; set; }
    [DataType(DataType.Date)]
    public DateTime? BirthDate { get; set; }    
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public Factory? Factory { get; set; }
    public Post? Post { get; set; }
}
