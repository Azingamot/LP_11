using LP_11.Utils;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

public class Database
{
    public static ClassContext Context = new ClassContext();

    public static void OnLoad()
    {
        if (!Context.Users.Any(u => u.Login == "admin"))
        {
            Context.WorkerUser.Add(new WorkerUser() { Worker = new Worker() { FullName = "admin" },
            User = new User() { Login = "admin", Password = Encryptor.Encrypt<byte[]>("12345"), Role = new Role { Name = "admin", AccessLevel = 10 } }
			});
            Context.SaveChanges();
        }
    }

    public static void CreateSession(User user)
    {
        if (CheckSession(user.Id)) return;

        UserSession session = new UserSession();
        session.User = user;
        session.LoginTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
        Context.UserSessions.Add(session);
        Context.SaveChanges();
    }

    public static bool CheckSession(int id)
	{
        if (Context.UserSessions.Any(u => u.User.Id == id))
        {
            UserSession session = Context.UserSessions.Where(u => u.User.Id == id).First();

            if (DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc) - DateTime.SpecifyKind(session.LoginTime, DateTimeKind.Utc) > TimeSpan.FromMinutes(30))
            {
                Context.UserSessions.Remove(session);
                Context.SaveChanges();
                return false;
            }
            return true;
        }
        return false;
    }
}

