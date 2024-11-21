using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units;

namespace GamePrototype.Utils
{
    public class UnitFactoryDemo
    {
        public static Unit CreatePlayer(string name)
        {
            var player = new Player(name, 30, 30, 6);
            Console.WriteLine("Select weapon: Gun or Sword");
            string weapon = Console.ReadLine();
            if (weapon == "Sword")
            {
                player.AddItemToInventory(new Weapon(10, 15, "Sword"));
                Console.WriteLine("You have chosen a Sword");
            }
            else if (weapon == "Gun")
            {
                player.AddItemToInventory(new Weapon(15, 20, "Gun"));
                Console.WriteLine("You have chosen a Gun");
            }
            else
            {
                Console.WriteLine("You entered the name of the weapon incorrectly. It will be installed by default");
            }

            Console.WriteLine("Select armour: Armour or SuperArmour");
            string armour = Console.ReadLine();
            if (armour == "Armour")
            {
                player.AddItemToInventory(new Armour(10, 15, "Armour"));
                Console.WriteLine("You have chosen an armour");
            }
            else if (armour == "SuperArmour")
            {
                player.AddItemToInventory(new Armour(20, 30, "SuperArmour"));
                Console.WriteLine("You have chosen a SuperArmour");
            }
            else
            {
                Console.WriteLine("You entered the name of the armour incorrectly. It will be installed by default");
            }

            player.AddItemToInventory(new HealthPotion("Potion"));
            return player;
        }

        public static Unit CreateGoblinEnemy() => new Goblin(GameConstants.Goblin, 18, 18, 2);



        public interface IUnitFactory
        {
            Unit CreateGoblinEnemy();
        }

        // Реализация фабрики для легкого уровня сложности
        public class EasyUnitFactory
        {
            public Unit CreateGoblinEnemy()
            {
                // Создание юнита для легкого уровня сложности
                var goblin = new Goblin(GameConstants.Goblin, 5, 5, 2);
                return goblin;
            }
        }

        // Реализация фабрики для сложного уровня сложности
        public class HardUnitFactory
        {
            public Unit CreateGoblinEnemy()
            {
                // Создание юнита для сложного уровня сложности
                var goblin = new Goblin(GameConstants.Goblin, 30, 30, 15);
                return goblin;
            }
        }
    }
}
