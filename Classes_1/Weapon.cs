using System;

public class Weapon
{
    public string Name { get; }
    private float _minDamage;
    private float _maxDamage;
    private Random _random;

    public float MinDamage
    {
        get { return _minDamage; }
        private set { _minDamage = value; }
    }

    public float MaxDamage
    {
        get { return _maxDamage; }
        private set { _maxDamage = value; }
    }

    public float Damage
    {
        get { return MinDamage + MaxDamage; }
    }

    public float GetDamage()
    {
        return _random.Next((int)MinDamage, (int)MaxDamage);
    }

    public Weapon(string name)
    {
        Name = name;
        _random = new Random();
    }

    public Weapon(string name, float minDamage, float maxDamage) : this(name)
    {
        SetDamageParams(minDamage, maxDamage);
    }

    public Weapon(string name, float damage) : this(name, damage, damage)
    {
    }

    public Weapon(float minDamage, float maxDamage)
    {
        MinDamage = minDamage;
        MaxDamage = maxDamage;
    }

    private void SetDamageParams(float minDamage, float maxDamage)
    {
        if (minDamage > maxDamage)
        {
            Console.WriteLine("Некорректные входные данные для оружия {0}. Минимальный урон больше максимального. Значения будут поменяны местами.", Name);
            float temp = minDamage;
            minDamage = maxDamage;
            maxDamage = temp;
        }

        if (minDamage < 1f)
        {
            Console.WriteLine("Форсированная установка минимального значения урона для оружия {0}.", Name);
            minDamage = 1f;
        }

        MinDamage = minDamage;
        MaxDamage = maxDamage;
    }
}
