using System;
using static Unit;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Подготовка к бою:");

        Console.WriteLine("Введите имя бойца:");
        string name = Console.ReadLine();

        Console.WriteLine("Введите начальное здоровье бойца (10-100):");
        int health = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите значение брони шлема от 0 до 1:");
        float helmArmor = float.Parse(Console.ReadLine());

        Console.WriteLine("Введите значение брони кирасы от 0 до 1:");
        float shellArmor = float.Parse(Console.ReadLine());

        Console.WriteLine("Введите значение брони сапог от 0 до 1:");
        float bootsArmor = float.Parse(Console.ReadLine());

        Console.WriteLine("Укажите минимальный урон оружия (0-20):");
        float minDamage = float.Parse(Console.ReadLine());

        Console.WriteLine("Укажите максимальный урон оружия (20-40):");
        float maxDamage = float.Parse(Console.ReadLine());

        Unit player = new Unit(name, health, new Helm(helmArmor), new Shell(shellArmor), new Boots(bootsArmor), new Weapon(minDamage, maxDamage), new Interval(15, 25));

        Console.WriteLine("Общий показатель брони равен: " + player.Armor);
        Console.WriteLine("Фактическое значение здоровья равно: " + player.RealHealth);

        Unit unit1 = new Unit("Боец 1", 80, new Helm(helmArmor), new Shell(shellArmor), new Boots(bootsArmor), new Weapon(minDamage, maxDamage), new Interval(10, 30));
        Unit unit2 = new Unit("Боец 2", 90, new Helm(helmArmor), new Shell(shellArmor), new Boots(bootsArmor), new Weapon(minDamage, maxDamage), new Interval(5, 25));

        Console.WriteLine($"Боец 1: {unit1.Name}, Здоровье: {unit1.Health}, Интервал урона: {unit1.DamageInterval.Min}-{unit1.DamageInterval.Max}");
        Console.WriteLine($"Боец 2: {unit2.Name}, Здоровье: {unit2.Health}, Интервал урона: {unit2.DamageInterval.Min}-{unit2.DamageInterval.Max}");

        Combat combat = new Combat();
        combat.StartCombat(unit1, unit2);

        combat.ShowResults();

        Console.ReadLine();
    }
}