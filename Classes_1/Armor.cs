using System;

public class Armor
{
    private const float MinArmorValue = 0f;
    private const float MaxArmorValue = 1f;

    private readonly string name;
    private float armorValue;

    public string Name
    {
        get { return name; }
    }

    public float ArmorValue
    {
        get { return armorValue; }
        set
        {
            armorValue = MathExtensions.Clamp(value, MinArmorValue, MaxArmorValue);
            if (armorValue != value)
            {
                Console.WriteLine("Задано некорректное значение брони. Будет установлено значение в диапазоне от 0 до 1.");
            }
        }
    }

    public Armor(string name)
    {
        this.name = name;
        InitializeArmorValue();
    }

    private void InitializeArmorValue()
    {
        switch (name.ToLower())
        {
            case "шлем":
                armorValue = new Helm().ArmorValue;
                break;

            case "кираса":
                armorValue = new Shell().ArmorValue;
                break;

            case "сапоги":
                armorValue = new Boots().ArmorValue;
                break;

            default:
                Console.WriteLine("Некорректное имя брони. Будет установлено значение по умолчанию.");
                armorValue = 0.5f;
                break;
        }
    }
}

public class Helm
{
    public Helm(float helmArmor)
    {
        ArmorValue = helmArmor;
    }
    public float ArmorValue { get; private set; }

    public Helm()
    {
        ArmorValue = 0.5f;
    }
}

public class Shell
{
    public Shell(float shellArmor)
    {
        ArmorValue = shellArmor;
    }

    public float ArmorValue { get; private set; }

    public Shell()
    {
        ArmorValue = 0.8f;
    }
}

public class Boots
{
    public Boots(float bootsArmor)
    {
        ArmorValue = bootsArmor;
    }

    public float ArmorValue { get; private set; }

    public Boots()
    {
        ArmorValue = 0.3f;
    }
}
public static class MathExtensions
{
    public static T Clamp<T>(T value, T min, T max) where T : IComparable<T>
    {
        if (value.CompareTo(min) < 0)
            return min;
        else if (value.CompareTo(max) > 0)
            return max;
        else
            return value;
    }
}
