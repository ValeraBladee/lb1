using System;

// Інтерфейс для об'єктів, які можуть отримувати пошкодження
public interface IDamageable
{
    void TakeDamage(int amount);
}
// Абстрактний клас Projectile (Снаряд)
public abstract class Projectile
{
    protected int damage;

    public Projectile(int damage)
    {
        this.damage = damage;
    }

    public abstract void HitTarget(IDamageable target);
}
// Куля - конкретна реалізація Projectile
public class Bullet : Projectile
{
    public Bullet(int damage) : base(damage) { }

    public override void HitTarget(IDamageable target)
    {
        Console.WriteLine("Bullet fired!");
        target.TakeDamage(damage);
    }
}
// Ракета з підвищеним пошкодженням
public class Rocket : Projectile
{
    private static Random rand = new Random();

    public Rocket(int baseDamage) : base(baseDamage) { }

    public override void HitTarget(IDamageable target)
    {
        int actualDamage = damage + rand.Next(-5, 6); // варіація +/-5
        Console.WriteLine($"Rocket explodes with damage: {actualDamage}");
        target.TakeDamage(actualDamage);
    }
}
// Звичайний ворог
public class Enemy : IDamageable
{
    public int Health { get; private set; } = 100;

    public void TakeDamage(int amount)
    {
        Health -= amount;
        Console.WriteLine($"Enemy took {amount} damage. Health now: {Health}");

        if (Health <= 0)
            Console.WriteLine("Enemy destroyed!");
    }
}
// Броньований ворог з меншою шкодою від снарядів
public class ArmoredEnemy : IDamageable
{
    public int Health { get; private set; } = 150;

    public void TakeDamage(int amount)
    {
        int reducedDamage = amount / 2;
        Health -= reducedDamage;
        Console.WriteLine($"ArmoredEnemy took {reducedDamage} damage (reduced). Health now: {Health}");

        if (Health <= 0)
            Console.WriteLine("ArmoredEnemy destroyed!");
    }
}
// Стіна, яку можна зламати
public class BreakableWall : IDamageable
{
    public int Durability { get; private set; } = 50;

    public void TakeDamage(int amount)
    {
        Durability -= amount;
        Console.WriteLine($"Wall took {amount} damage. Durability now: {Durability}");

        if (Durability <= 0)
            Console.WriteLine("Wall broken!");
    }
}
class Program
{
    static void Main()
    {
        // Цілі
        IDamageable enemy = new Enemy();
        IDamageable wall = new BreakableWall();
        IDamageable armoredEnemy = new ArmoredEnemy();

        // Снаряди
        Projectile bullet = new Bullet(20);
        Projectile rocket = new Rocket(40);

        Console.WriteLine("\n=== Стрільба кулею ===");
        bullet.HitTarget(enemy);
        bullet.HitTarget(wall);

        Console.WriteLine("\n=== Стрільба ракетою ===");
        rocket.HitTarget(armoredEnemy);
        rocket.HitTarget(wall);
    }
}
