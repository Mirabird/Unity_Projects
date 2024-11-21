using Final_Game;

class Program
{
    static void Main(string[] args)
    {
        // Создание экземпляра класса Casino
        Casino casino = new Casino();

        // Создание и инициализация экземпляра ISaveLoadService<string>
        ISaveLoadService<string> saveLoadService = new FileSystemSaveLoadService<string>();

        // Передача зависимостей в объект Casino
        casino.BuildService(saveLoadService);

        // Запуск игры
        casino.StartGame();

        Console.ReadLine();
    }
}