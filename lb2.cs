using System;
using System.Collections.Generic;

public class User
{
    public string UserName { get; set; }
    public string Email { get; set; }
    private string _password;

    public void SetPassword(string newPassword)
    {
        _password = newPassword;
    }

    public bool Authenticate(string inputPassword)
    {
        return _password == inputPassword;
    }

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Ім'я: {UserName} | Email: {Email}");
    }
}

public class Admin : User
{
    public void BlockUser(User user)
    {
        Console.WriteLine($"Користувача {user.UserName} заблоковано.");
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine("Роль: Адміністратор");
    }
}

public class Moderator : User
{
    public void ModerateContent()
    {
        Console.WriteLine("Контент модеровано.");
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine("Роль: Модератор");
    }
}

public class RegularUser : User
{
    public void PostComment()
    {
        Console.WriteLine("Коментар опубліковано.");
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine("Роль: Звичайний користувач");
    }
}

class Program
{
    static void Main()
    {
        // Створення користувачів
        Admin admin = new Admin { UserName = "AdminUser", Email = "admin@example.com" };
        admin.SetPassword("admin123");

        Moderator mod = new Moderator { UserName = "ModUser", Email = "mod@example.com" };
        mod.SetPassword("wrongpass");  // буде неправильний для демонстрації

        RegularUser user = new RegularUser { UserName = "RegUser", Email = "user@example.com" };
        user.SetPassword("user123");

        // Список користувачів через upcasting
        List<User> users = new List<User> { admin, mod, user };

        Console.WriteLine("=== Інформація про користувачів ===");
        foreach (var u in users)
        {
            u.DisplayInfo();
            Console.WriteLine();
        }

        Console.WriteLine("=== Тестування методів ===");

        foreach (var u in users)
        {
            if (u is Admin a)
                a.BlockUser(user);  // Адмін блокує звичайного юзера
            else if (u is Moderator m)
                m.ModerateContent();
            else if (u is RegularUser ru)
                ru.PostComment();
        }

        Console.WriteLine("\n=== Перевірка аутентифікації ===");
        Console.WriteLine($"{admin.UserName}: {(admin.Authenticate("admin123") ? "Успішна аутентифікація" : "Невірний пароль")}");
        Console.WriteLine($"{mod.UserName}: {(mod.Authenticate("mod123") ? "Успішна аутентифікація" : "Невірний пароль")}");
        Console.WriteLine($"{user.UserName}: {(user.Authenticate("user123") ? "Успішна аутентифікація" : "Невірний пароль")}");
    }
}
