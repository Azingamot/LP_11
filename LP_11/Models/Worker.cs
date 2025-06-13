using System.ComponentModel.DataAnnotations;

public class Worker
{
    [Key]
    public int Id { get; set; }
    public string? FullName { get; set; }
    public DateTime? BirthDate { get; set; }    
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}
