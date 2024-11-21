using GamePrototype.Dungeon;
using GamePrototype.Items.EconomicItems;
using static GamePrototype.Utils.UnitFactoryDemo;

namespace GamePrototype.Utils
{
    public static class DungeonBuilder
    {
        public static DungeonRoom BuildDungeon()
        {
            var enter = new DungeonRoom("Enter");
            var monsterRoom = new DungeonRoom("Monster", UnitFactoryDemo.CreateGoblinEnemy());
            var emptyRoom = new DungeonRoom("Empty");
            var lootRoom = new DungeonRoom("Loot1", new Gold());
            var lootStoneRoom = new DungeonRoom("Loot1", new Grindstone("Stone"));
            var finalRoom = new DungeonRoom("Final", new Grindstone("Stone1"));

            enter.TrySetDirection(Direction.Right, monsterRoom);
            enter.TrySetDirection(Direction.Left, emptyRoom);

            monsterRoom.TrySetDirection(Direction.Forward, lootRoom);
            monsterRoom.TrySetDirection(Direction.Left, emptyRoom);

            emptyRoom.TrySetDirection(Direction.Forward, lootStoneRoom);

            lootRoom.TrySetDirection(Direction.Forward, finalRoom);
            lootStoneRoom.TrySetDirection(Direction.Forward, finalRoom);

            return enter;
        }
    }

    // Реализация строителя для легкого уровня сложности
    public static class EasyDungeonBuilder
    {
        public static DungeonRoom BuildDungeon()
        {
            // Создание подземелий для легкого уровня сложности
            var enter = new DungeonRoom("Вход");
            var monsterRoom = new DungeonRoom("Комната с монстром", new EasyUnitFactory().CreateGoblinEnemy());
            var emptyRoom = new DungeonRoom("Пустая комната");
            var lootRoom = new DungeonRoom("Комната с добычей", new Gold());
            var lootStoneRoom = new DungeonRoom("Комната с добычей", new Grindstone("Камень"));
            var finalRoom = new DungeonRoom("Финальная комната", new Grindstone("Камень1"));

            enter.TrySetDirection(Direction.Right, monsterRoom);
            enter.TrySetDirection(Direction.Left, emptyRoom);

            monsterRoom.TrySetDirection(Direction.Forward, lootRoom);
            monsterRoom.TrySetDirection(Direction.Left, emptyRoom);

            emptyRoom.TrySetDirection(Direction.Forward, lootStoneRoom);
            lootStoneRoom.TrySetDirection(Direction.Left, finalRoom);

            return enter;
        }
    }

    // Реализация строителя для сложного уровня сложности
    public static class HardDungeonBuilder
    {
        public static DungeonRoom BuildDungeon()
        {
            // Создание подземелий для сложного уровня сложности
            var enter = new DungeonRoom("Вход");
            var bossRoom = new DungeonRoom("Комната с боссом", new HardUnitFactory().CreateGoblinEnemy());
            var trapRoom = new DungeonRoom("Комната с ловушками");
            var lootRoom = new DungeonRoom("Комната с добычей", new Gold());
            var lootGemRoom = new DungeonRoom("Комната с добычей", new Gold("Рубин"));
            var finalRoom = new DungeonRoom("Финальная комната", new Gold("Рубин1"));

            enter.TrySetDirection(Direction.Right, bossRoom);
            enter.TrySetDirection(Direction.Left, trapRoom);

            bossRoom.TrySetDirection(Direction.Forward, lootRoom);
            bossRoom.TrySetDirection(Direction.Left, trapRoom);

            trapRoom.TrySetDirection(Direction.Forward, lootGemRoom);
            lootGemRoom.TrySetDirection(Direction.Left, finalRoom);

            return enter;
        }
    }
}

