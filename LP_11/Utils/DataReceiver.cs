using Microsoft.EntityFrameworkCore;

public class DataReceiver
{
	public static List<ProductionProductionProperty> ReceiveProductionProperties(Production production)
	{
		ClassContext context = new ClassContext();

		List<ProductionProductionProperty> properties = context.ProductionProductionPropertes
			.Include(u => u.ProductionPropety).Include(u => u.Production)
			.Where(u => u.Production == production).ToList();

		return properties;
	}

	public static List<Production> ReceiveProduction()
	{
		ClassContext context = new ClassContext();

		List<Production> productions = context.Productions.Include(u => u.Category).ToList();

		return productions;
	}

	public static List<Client> ReceiveClients()
	{
		ClassContext context = new ClassContext();

		List<Client> clients = context.Clients.ToList();

		return clients;
	}

	public static List<Worker> ReceiveWorkers()
	{
		ClassContext context = new ClassContext();

		List<Worker> workers = context.Workers
			.Include(u => u.Factory)
			.Include(u => u.Post)
			.ToList();

		return workers;
	}

	public static List<Factory> ReceiveFactories()
	{
		ClassContext context = new ClassContext();

		List<Factory> factories = context.Factories.ToList();

		return factories;
	}

	public static List<Post> ReceivePosts()
	{
		ClassContext context = new ClassContext();
		
		List<Post> posts = context.Posts.ToList();

		return posts; 
	}

	public static int ReceiveAccessLevel(int id)
	{
		ClassContext context = new ClassContext();
		User? user = context.Users.Where(u => u.Id == id).Include(u => u.Role).FirstOrDefault();
		if (user != null && user.Role != null)
		{
			int res = user.Role.AccessLevel;
			return res;
		}
		return 0;
	}
}
