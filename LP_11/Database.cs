public class Database
{
    public static ClassContext Context = new ClassContext();

    public static void OnLoad()
    {
        if (!Context.Workers.Any(u => u.FullName == "test"))
        {
            Context.Workers.Add(new Worker() {FullName = "test" });
            Context.SaveChanges();
        }
    }
}

