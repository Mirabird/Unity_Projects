using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Final_Game
{
    public class WrongDiceNumberException : Exception
    {
        public WrongDiceNumberException(int number, int maxValue)
            : base($"Некорректное значение номера: {number}. Допустимый диапазон: от 1 до {maxValue}.")
        {
        }
    }
}
