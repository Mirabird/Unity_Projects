namespace Final_Game
{
    public interface IGame
    {
        void StartGame();
    }

    public interface IBuilderSupporter<T, U> 
        where T : ISaveLoadService<string>
        where U : CasinoGameBase
    {
        void BuildService(T service);
        void BuildCustom(U[] customClasses);
    }

    public class Casino : IGame, IBuilderSupporter<ISaveLoadService<string>, CasinoGameBase>
    {
        public ISaveLoadService<string> saveLoadService {  get; set; }
        private CasinoGameBase _game;
        public string PlayerName { get; set; }
        public int Bank {  get; set; }

        public int Stavka { get; set; }

        public void StartGame()
        {
            PlayerName = saveLoadService.LoadData("playerName");

            Console.WriteLine();
            Console.WriteLine($"Привет, {PlayerName}! Добро пожаловать в казино!");

            string savedBank = saveLoadService.LoadData("bank");
            if (!string.IsNullOrEmpty(savedBank))
            {
                int.TryParse(savedBank, out int savedBankValue);
                Bank = savedBankValue;
                Console.WriteLine($"Загружен банк: {Bank}");

                int MaxBank = 2500;

                if (Bank > MaxBank)
                {
                    int Ostatok = Bank - MaxBank;
                    Bank = Bank / 2;
                    Console.WriteLine($"Ты разорил казино на {Ostatok}, на его месте построят новое :(");
                    Console.WriteLine("You wasted half of your bank money in casino’s bar");
                    Console.WriteLine($"Новое значение твоего банка: {Bank}");

                    saveLoadService.SaveData(Bank.ToString(), "bank");
                }
            }
            else
            {
                // Если данных для банка нет, создаем новые и сохраняем их
                Console.WriteLine("Введите имя пользователя: ");
                PlayerName = Console.ReadLine();
                Bank = 1000;
                saveLoadService.SaveData(Bank.ToString(), "bank");
                Console.WriteLine($"Банк создан со значением: {Bank}");

                saveLoadService.SaveData(PlayerName.ToString(), "playerName");
            }
            var builder = CasinoBuilder.BuildGame();
        }

        public void BuildService(ISaveLoadService<string> saveLoadService)
        {
            this.saveLoadService = saveLoadService;
        }

        public void BuildCustom(CasinoGameBase[] customClasses)
        {
            if (customClasses.Length > 0)
            {
                _game = customClasses[0];
                _game.OnWin += () => Console.WriteLine("Поздравляем!");
                _game.OnLoose += () => Console.WriteLine("Повезёт в другой раз!");
                _game.OnDraw += () => Console.WriteLine("Повезёт в другой раз!");
            }
        }
    }

    public class CasinoBuilder
    {
        public static IBuilderSupporter<ISaveLoadService<string>, CasinoGameBase> BuildGame()
        {
            var casino = new Casino();

            Console.WriteLine();
            Console.WriteLine("Выберите игру: ");
            Console.WriteLine("1. Blackjack");
            Console.WriteLine("2. Bones");
            Console.WriteLine("exit - Выйти из казино");

            string choice = Console.ReadLine();

            if (choice == "exit")
            {
                Console.WriteLine("До свидания!");
                Environment.Exit(0);
                Console.ReadLine();
            }

            ISaveLoadService<string> saveLoadService1 = new FileSystemSaveLoadService<string>();
            casino.BuildService(saveLoadService1);

            int Bank = Int32.Parse(saveLoadService1.LoadData("bank"));

            if (Bank == 0)
            {
                Console.WriteLine("No money? Kicked!");
                Environment.Exit(0);
                Console.ReadLine();
            }

            Console.WriteLine("Сделай ставку: ");
            casino.Stavka = int.Parse(Console.ReadLine());

            if (casino.Stavka > Bank)
            {
                Console.WriteLine("Сорри чел, но у тебя нет столько денег");
                CasinoBuilder.BuildGame();
            }
            else
            {
                if (choice == "1")
                {
                    // Создаем экземпляр ISaveLoadService<string> и передаем его в метод BuildService
                    ISaveLoadService<string> saveLoadService = new FileSystemSaveLoadService<string>();
                    casino.BuildService(saveLoadService);

                    // Создаем экземпляр Blackjack с дополнительными аргументами (например, numberOfDecks)
                    int numberOfDecks = 3;
                    Console.WriteLine($"В игре {numberOfDecks} колоды");
                    int Stavka = casino.Stavka;
                    CasinoGameBase blackjack = new Blackjack(numberOfDecks, Stavka);

                    // Создаем массив игр и передаем его в метод BuildCustom
                    CasinoGameBase[] games = new CasinoGameBase[] { blackjack };
                    casino.BuildCustom(games);
                    casino.StartGame();
                }
                else if (choice == "2")
                {
                    // Создаем экземпляр ISaveLoadService<string> и передаем его в метод BuildService
                    ISaveLoadService<string> saveLoadService = new FileSystemSaveLoadService<string>();
                    casino.BuildService(saveLoadService);

                    // Создаем экземпляр Bones с дополнительными аргументами (например, Number, min и max)
                    int Number = 4;
                    int min = 4;
                    int max = 24;
                    int Stavka = casino.Stavka;
                    Console.WriteLine($"В игре {Number} кубика. Минимальное значение: {min}. Максимальное значение: {max}");
                    CasinoGameBase bones = new Bones(Number, min, max, Stavka);

                    // Создаем массив игр и передаем его в метод BuildCustom
                    CasinoGameBase[] games = new CasinoGameBase[] { bones };
                    casino.BuildCustom(games);
                    casino.StartGame();
                }
                else
                {
                    Console.WriteLine("Неккоректный выбор игры!");
                    casino.StartGame();
                }
            }
            // Приводим объект casino к типу IBuilderSupporter и возвращаем его
            return (IBuilderSupporter<ISaveLoadService<string>, CasinoGameBase>)casino;
        }
    }
}