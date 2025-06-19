using Microsoft.EntityFrameworkCore;
public class ClassContext: DbContext
{
    public ClassContext()
    {
		Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost; Port=5432; Database=LP_11; Username=postgres; Password=azingamot; Include Error Detail=true");
    }

    public DbSet<Worker> Workers { get; set; }
	public DbSet<Role> Roles { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<WorkerUser> WorkerUser { get; set; }
    public DbSet<UserSession> UserSessions { get; set; }
    public DbSet<Production> Productions { get; set; }
    public DbSet<ProductionCategory> ProductionCategories { get; set; }
    public DbSet<ProductionProperty> ProductionProperties { get; set; }
    public DbSet<Orders> Orders { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Factory> Factories { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<WarehouseProduct> WarehouseProducts { get; set; }
    public DbSet<ProductionProductionProperty> ProductionProductionPropertes { get; set; }
    public DbSet<Post> Posts { get; set; }

    public DbSet<ProductionImage> ProductionImages { get; set; }
}

