using System.Text.Json;
using System.IO;

namespace VKI.MapApp.Data
{
    public static class JsonDataManager
    {
        /// <summary>
        /// Загружает данные из JSON файла.
        /// </summary>
        /// <typeparam name="T">Тип данных для десериализации.</typeparam>
        /// <param name="filePath">Путь к JSON файлу.</param>
        /// <returns>Объект типа T или null, если файл не существует.</returns>
        public static T? Load<T>(string filePath) where T : class, new()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<T>(json)?? null;
            }
            return null;
        }

        /// <summary>
        /// Сохраняет данные в JSON файл.
        /// </summary>
        /// <typeparam name="T">Тип данных для сериализации.</typeparam>
        /// <param name="data">Данные для сохранения.</param>
        /// <param name="filePath">Путь к JSON файлу.</param>
        public static void Save<T>(T data, string filePath)
        {
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}