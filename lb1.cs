using System;

public class Product
{
    private string name;
    private decimal price;
    private int quantity;
    private DateTime lastUpdated;

    public Product(string name, decimal price, int quantity)
    {
        Name = name;
        Price = price;
        this.quantity = quantity;
        lastUpdated = DateTime.Now;
    }

    public string Name
    {
        get => name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Назва товару не може бути порожньою.");
            name = value;
            lastUpdated = DateTime.Now;
        }
    }

    public decimal Price
    {
        get => price;
        set
        {
            if (value < 0)
                throw new ArgumentException("Ціна не може бути меншою за 0.");
            price = value;
            lastUpdated = DateTime.Now;
        }
    }

    public int Quantity => quantity;

    public decimal TotalValue => price * quantity;

    public DateTime LastUpdated => lastUpdated;

    public void Restock(int amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Кількість має бути додатньою.");
            return;
        }
        quantity += amount;
        lastUpdated = DateTime.Now;
    }

    public void Sell(int amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Кількість має бути додатньою.");
            return;
        }
        if (amount > quantity)
        {
            Console.WriteLine("Недостатньо товару на складі!");
            return;
        }
        quantity -= amount;
        lastUpdated = DateTime.Now;
    }

    public string GetInfo()
    {
        return $"Товар: {name}, Ціна: {price} грн, Кількість: {quantity}, Загальна вартість: {TotalValue} грн";
    }
}

class Program
{
    static void Main()
    {
        Product apple = new Product("Яблуко", 5, 100);
        Console.WriteLine(apple.GetInfo());

        apple.Sell(20);
        Console.WriteLine(apple.GetInfo());

        apple.Restock(50);
        Console.WriteLine(apple.GetInfo());

        apple.Price = 7;
        Console.WriteLine(apple.GetInfo());

        apple.Name = "Зелене яблуко";
        Console.WriteLine(apple.GetInfo());

        try { apple.Price = -10; } catch (Exception e) { Console.WriteLine(e.Message); }
        try { apple.Name = ""; } catch (Exception e) { Console.WriteLine(e.Message); }

        apple.Sell(200);
    }
}
