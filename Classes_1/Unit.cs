using System;

public class Unit
{
    public Unit(string name, float health, Helm helm, Shell shell, Boots boots, Weapon weapon, Interval damageInterval)
    {
        Name = name;
        Health = health;
        Helm = helm;
        Shell = shell;
        Boots = boots;
        Weapon = weapon;
        DamageInterval = damageInterval;
    }

    public string Name { get; }
    private float _health;

    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if (_health < 0)
            {
                _health = 0;
            }
        }
    }

    public Interval DamageInterval { get; set; }

    public int HealthInt
    {
        get
        {
            return (int)Math.Floor(Health);
        }
    }

    public float Damage
    {
        get
        {
            return Weapon != null ? Weapon.Damage + BaseDamage : BaseDamage;
        }
    }

    public float Armor
    {
        get
        {
            float HelmArmor = Helm != null ? Helm.ArmorValue : 0;
            float ShellArmor = Shell != null ? Shell.ArmorValue : 0;
            float BootsArmor = Boots != null ? Boots.ArmorValue : 0;

            float TotalArmor = HelmArmor * ShellArmor * BootsArmor;

            if (TotalArmor < 0)
            {
                TotalArmor = 0;
            }
            else if (TotalArmor > 1)
            {
                TotalArmor = 1;
            }

            return TotalArmor;
        }
    }

    public float RealHealth
    {
        get
        {
            return Health * (1f + Armor);
        }
    }

    public bool SetDamage(float value)
    {
        Health -= value * Armor;

        return Health <= 0f;
    }

    private const float BaseDamage = 5;
    public Weapon Weapon { get; set; }
    public Helm Helm { get; set; }
    public Shell Shell { get; set; }
    public Boots Boots { get; set; }

    public Unit() : this("Unknown Unit", 100)
    {
    }

    public Unit(string name) : this(name, 100)
    {
    }

    public Unit(string name, float health)
    {
        Name = name;
        Health = health;
    }

    public void EquipWeapon(Weapon weapon)
    {
        Weapon = weapon;
    }
    public void EquipHelm(Helm helm)
    {
        Helm = helm;
    }

    public void EquipShell(Shell shell)
    {
        Shell = shell;
    }

    public void EquipBoots(Boots boots)
    {
        Boots = boots;
    }

    public struct Interval
    {
        public double MinValue { get; }
        public double MaxValue { get; }

        public Interval(double minValue, double maxValue)
        {
            if (minValue > maxValue)
            {
                // Если minValue больше maxValue, меняем их местами
                MinValue = maxValue;
                MaxValue = minValue;
                Console.WriteLine("Некорректные входные данные: minValue больше maxValue. Значения были переставлены.");
            }
            else
            {
                MinValue = minValue;
                MaxValue = maxValue;
            }
        }

        public Interval(double value) : this(value, value)
        {
        }

        public double GetRandomNumber()
        {
            //знаю, что так не должно быть, но не получается поставить юнитевский рандом
            Random random = new Random();
            return random.Next((int)MaxValue, (int)MinValue);
        }

        public double Min => MinValue;

        public double Max => MaxValue;

        public double Average => (MinValue + MaxValue) / 2;

        public override string ToString()
        {
            return $"Interval({MinValue}, {MaxValue})";
        }
    }
}