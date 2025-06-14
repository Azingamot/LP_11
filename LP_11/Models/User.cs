using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int Id { get; set; }
    public String Login { get; set; }
    public byte[] Password { get; set; }
    public Role Role { get; set; }
}
