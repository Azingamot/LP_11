using Microsoft.EntityFrameworkCore;
public class ClassContext: DbContext
{
    public ClassContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost; Port=5432; Database=LP_11; Username=postgres; Password=azingamot");
    }

    public DbSet<Worker> Workers { get; set; }
}

