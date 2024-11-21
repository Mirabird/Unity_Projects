using System.IO;

namespace Final_Game
{
    public interface ISaveLoadService<T>
    {
        void SaveData(T data, string identifier);
        T LoadData(string identifier);
    }


    public class FileSystemSaveLoadService<T> : ISaveLoadService<string>
    {
        private readonly string _savePath;

        public FileSystemSaveLoadService()
        {
            string path = "D:/";
            _savePath = path;
            // Проверяем, существует ли путь, и если нет - создаем
            if (!Directory.Exists(_savePath))
            {
                Directory.CreateDirectory(_savePath);
            }
        }

        public void SaveData(string data, string identifier)
        {
            string filePath = Path.Combine(_savePath, identifier + ".txt");
            // Записываем данные в файл
            File.WriteAllText(filePath, data);
        }

        public string LoadData(string identifier)
        {
            string filePath = Path.Combine(_savePath, identifier + ".txt");
            // Проверяем, существует ли файл
            if (File.Exists(filePath))
            {
                // Читаем все строки из файла
                string[] lines = File.ReadAllLines(filePath);
                // Возвращаем последнюю строку
                if (lines.Length > 0)
                {
                    return lines[lines.Length - 1];
                }
            }

            // Если файла нет или он пустой, возвращаем пустую строку
            return string.Empty;
        }
    }
}
