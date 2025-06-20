﻿using LP_11.Utils;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

public class Database
{
    public static ClassContext Context = new ClassContext();

    public static void OnLoad()
    {
        ClassContext LoadingContext = new ClassContext();
        if (!LoadingContext.Users.Any(u => u.Login == "admin"))
        {
            LoadingContext.WorkerUser.Add(new WorkerUser() { Worker = new Worker() { FullName = "admin" },
            User = new User() { Login = "admin", Password = Encryptor.Encrypt<byte[]>("12345"), Role = new Role { Name = "admin", AccessLevel = 10 } }
			});
            LoadingContext.SaveChanges();
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

    public static void DeleteSession(int id)
    {
        UserSession? session = Context.UserSessions.Where(u => u.Id == id).FirstOrDefault();
        if (session != null)
        {
            Context.UserSessions.Remove(session);
            Context.SaveChanges();
        }
    }

    public static bool CheckSession(int id)
	{
        ClassContext checkContext = new ClassContext();
        if (checkContext.UserSessions.Any(u => u.User.Id == id))
        {
            UserSession session = checkContext.UserSessions.Where(u => u.User.Id == id).First();

            if (DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc) - DateTime.SpecifyKind(session.LoginTime, DateTimeKind.Utc) > TimeSpan.FromMinutes(30))
            {
                checkContext.UserSessions.Remove(session);
                checkContext.SaveChanges();
                return false;
            }
            return true;
        }
        return false;
    }

    public static bool LoadFile(Production production, byte[] array)
    {
        try
        {
            ProductionImage? productionImage = Context.ProductionImages.Where(u => u.Production == production).FirstOrDefault();

            if (productionImage != null)
            {
                Context.Remove(productionImage);
            }
            productionImage = new ProductionImage() { Production = production, Image = array };
            Context.ProductionImages.Attach(productionImage);
            Context.SaveChanges();    
            return true;
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.ToString());
            return false;
        } 
    }
}

