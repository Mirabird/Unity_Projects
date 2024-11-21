namespace Final_Game
{
    public class Blackjack : CasinoGameBase
    {
        private readonly int _numberOfCards;
        private int _stavka;
        private Queue<Card> _deck;
        private List<Card> _playerHand;
        private List<Card> _dealerHand;

        public int Bank { get; set; }

        public ISaveLoadService<string> saveLoadService { get; set; }

        public Blackjack(int numberOfCards, int stavka)
        {
            CheckInputParametersValidity(new object[] { numberOfCards });

            _numberOfCards = numberOfCards;
            _stavka = stavka;
            FactoryMethod();
        }

        protected override void FactoryMethod()
        {
            _deck = CreateDeck();

            _playerHand = new List<Card>();
            _dealerHand = new List<Card>();

            //Начальная раздача
            DealCardToPlayer();
            DealCardToPlayer();
            DealCardToDealer();
            DealCardToDealer();

            PlayGame();
        }

        private void DealCardToPlayer()
        {
            Card card = _deck.Dequeue();
            _playerHand.Add(card);
        }

        private void DealCardToDealer()
        {
            Card card = _deck.Dequeue();
            _dealerHand.Add(card);
        }

        private Queue<Card> CreateDeck()
        {
            List<Card> cards = new List<Card>();

            for (int i = 0; i < _numberOfCards; i++)
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                    {
                        Card card = new Card(suit, rank);
                        cards.Add(card);
                    }
                }
            }
            Shuffle(cards);

            Queue<Card> deck = new Queue<Card>(cards);

            return deck;
        }

        private void Shuffle<T>(List<T> list)
        {
            Random random = new Random();

            int n = list.Count;

            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public void BuildService(ISaveLoadService<string> saveLoadService)
        {
            this.saveLoadService = saveLoadService;
        }

        protected override void PrintResults()
        {
            Console.WriteLine();
            Console.WriteLine("Результаты игры:");

            Console.WriteLine();
            Console.WriteLine("Ваша рука:");
            foreach (Card card in _playerHand)
            {
                Console.WriteLine(card);
            }
            int playerValue = GetHandValue(_playerHand);
            Console.WriteLine($"Общая стоимость карт: {playerValue}");

            Console.WriteLine();
            Console.WriteLine("Рука дилера:");
            foreach (Card card in _dealerHand)
            {
                Console.WriteLine(card);
            }
            int dealerValue = GetHandValue(_dealerHand);
            Console.WriteLine($"Общая стоимость карт: {dealerValue}");


            ISaveLoadService<string> saveLoadService = new FileSystemSaveLoadService<string>();
            BuildService(saveLoadService);

            string savedBank = saveLoadService.LoadData("bank");
            int.TryParse(savedBank, out int savedBankValue);
            Bank = savedBankValue;

            //и                          //или
            if (playerValue <= 21 && (playerValue > dealerValue || dealerValue > 21))
            {
                Console.WriteLine();
                Bank += _stavka;
                Console.WriteLine("Вы выиграли!");
                Console.WriteLine($"Банк: {Bank}");

                saveLoadService.SaveData(Bank.ToString(), "bank");
                Console.WriteLine($"Сохранён банк: {Bank}");
                OnWinInvoke();
            }
            else if (dealerValue <= 21 && (dealerValue > playerValue || playerValue > 21))
            {
                Console.WriteLine();
                Bank -= _stavka;
                Console.WriteLine("Вы проиграли!");
                Console.WriteLine($"Банк: {Bank}");

                saveLoadService.SaveData(Bank.ToString(), "bank");
                Console.WriteLine($"Сохранён банк: {Bank}");
                OnLooseInvoke();
            }
            else if (playerValue >= 21 && dealerValue >= 21)
            {
                Console.WriteLine();
                Bank = Bank;
                Console.WriteLine("Ничья!");
                Console.WriteLine($"Банк: {Bank}");

                saveLoadService.SaveData(Bank.ToString(), "bank");
                Console.WriteLine($"Сохранён банк: {Bank}");
                OnDrawInvoke();
            }
        }

        private int GetHandValue(List<Card> _hand)
        {
            int playerPoints = 0;
            int dealerPoints = 0;

            foreach (Card card in _hand)
            {
                if (card.Rank == Rank.Ace)
                {
                    playerPoints += 11;
                    dealerPoints++;
                }
                else if (card.Rank == Rank.Jack || card.Rank == Rank.Queen || card.Rank == Rank.King)
                {
                    playerPoints += 10;
                }
                else
                {
                    playerPoints += (int)card.Rank;
                }
            }

            while (playerPoints > 21 && dealerPoints > 0)
            {
                playerPoints -= 10;
                dealerPoints--;
            }

            return playerPoints;
        }

        private bool IsBlackJack(List<Card> _hand)
        {
            return GetHandValue(_hand) == 21 && _hand.Count == 2;
        }

        private bool IsBust(List<Card> _hand)
        {
            return GetHandValue(_hand) > 21;
        }

        public override void PlayGame()
        {
            if (IsBlackJack(_playerHand) || IsBlackJack(_dealerHand))
            {
                PrintResults();
            }
            else
            {
                while (true)
                {
                    int playerHandValue = GetHandValue(_playerHand);
                    int dealerHandValue = GetHandValue(_dealerHand);

                    if (playerHandValue < 21 && dealerHandValue < 21)
                    {
                        DealCardToPlayer();
                        DealCardToDealer();
                    }
                    else
                    {
                        break;
                    }
                }

                PrintResults();
            }
        }
    }
}
