using System;
using System.Collections.Generic;

class Combat
{
    private List<Rate> rates;

    public Combat()
    {
        rates = new List<Rate>();
    }

    public void StartCombat(Unit unit1, Unit unit2)
    {
        Random random = new Random();
        Console.WriteLine("Бой начался!");

        while (true)
        {
            int damage = random.Next((int)unit1.DamageInterval.Min, (int)(unit1.DamageInterval.Max + 1));

            int unit1Damage = damage;
            unit2.Health -= unit1Damage;

            damage = random.Next((int)unit2.DamageInterval.Min, (int)unit2.DamageInterval.Max + 1);
            int unit2Damage = damage;
            unit1.Health -= unit2Damage;

            Rate rate1 = new Rate(unit1, unit1Damage, unit1.Health);
            rates.Add(rate1);

            Rate rate2 = new Rate(unit2, unit2Damage, unit2.Health);
            rates.Add(rate2);

            if (unit1.Health <= 0 || unit2.Health <= 0)
                break;
        }
    }

    public void ShowResults()
    {
        Console.WriteLine("Бой завершен!");

        foreach (Rate rate in rates)
        {
            Console.WriteLine("Боец " + rate.Unit.Name + " нанес урон " + rate.Damage + " и оставил " + rate.Health + " здоровья.");
        }
    }
}

struct Rate
    {
        public Unit Unit { get; }
        public int Damage { get; }
        public double Health { get; }

        public Rate(Unit unit, int damage, double health)
        {
            Unit = unit;
            Damage = damage;
            Health = Math.Round(health, 2);
        }
    }
