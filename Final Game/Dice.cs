namespace Final_Game
{
    public struct Dice
    {
        private int _min;
        private int _max;

        public int Number { get; } 

        Random random = new Random();

        public Dice(int min, int max)
        {
            _min = min;
            _max = max;

            Number = random.Next(_min, _max);

            if (Number < 1 || Number > max)
            {
                throw new WrongDiceNumberException(Number, max);
            }
        }
    }
}
