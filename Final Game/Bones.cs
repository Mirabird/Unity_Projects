using System;

namespace Final_Game
{
    public class Bones : CasinoGameBase
    {
        private List<Dice> _playerDice;
        private List<Dice> _computerDice;
        private int _min;
        private int _max;
        private int _number;
        private int _stavka;
        Random random = new Random();

        public int Bank { get; set; }

        public ISaveLoadService<string> saveLoadService { get; set; }

        public int PlayerScore = 0;
        public int ComputerScore = 0;

        public Bones(int Number, int min, int max, int stavka)
        {
            _min = min;
            _max = max;
            _number = Number;
            _stavka = stavka;
            CheckInputParametersValidity(new object[] { Number, min, max , stavka});
            FactoryMethod();
        }

        public void BuildService(ISaveLoadService<string> saveLoadService)
        {
            this.saveLoadService = saveLoadService;
        }

        protected override void FactoryMethod()
        {
            _playerDice = new List<Dice>();
            _computerDice = new List<Dice>();

            for (int i = 0; i < _number; i++)
            {
                Dice dice = new Dice(_min, _max);
                _playerDice.Add(dice);
                _computerDice.Add(dice);
            }
            PlayGame();
        }

        protected override void PrintResults()
        {
            ISaveLoadService<string> saveLoadService = new FileSystemSaveLoadService<string>();
            BuildService(saveLoadService);

            string savedBank = saveLoadService.LoadData("bank");
            int.TryParse(savedBank, out int savedBankValue);
            Bank = savedBankValue;

            Console.WriteLine();
            Console.WriteLine("Результаты игры:");

            Console.WriteLine($"У вас {PlayerScore} очков!");
            Console.WriteLine($"У ПК {ComputerScore} очков!");

            if (PlayerScore > ComputerScore)
            {
                Console.WriteLine();
                Bank += _stavka;
                Console.WriteLine("Вы выиграли!");
                Console.WriteLine($"Банк: {Bank}");

                saveLoadService.SaveData(Bank.ToString(), "bank");
                Console.WriteLine($"Сохранён банк: {Bank}");
                OnWinInvoke();
            }
            else if (PlayerScore < ComputerScore)
            {
                Console.WriteLine();
                Bank -= _stavka;
                Console.WriteLine("Вы проиграли!");
                Console.WriteLine($"Банк: {Bank}");

                saveLoadService.SaveData(Bank.ToString(), "bank");
                Console.WriteLine($"Сохранён банк: {Bank}");
                OnLooseInvoke();
            }
            else
            {
                Console.WriteLine();
                Bank = Bank;
                Console.WriteLine("Ничья!");

                saveLoadService.SaveData(Bank.ToString(), "bank");
                Console.WriteLine($"Сохранён банк: {Bank}");
                OnDrawInvoke();
            }
        }

        public override void PlayGame()
        {
            foreach (Dice dice in _playerDice)
            {
                PlayerScore = random.Next(_min, _max);
            }

            foreach (Dice dice in _computerDice)
            {
                ComputerScore = random.Next(_min, _max);
            }
            PrintResults();
        }
    }
}
