using Newtonsoft.Json;

namespace Phoenix.Verification.Utilities
{
    public static class JsonUtilities
    {
        public static void SaveToFile(object modelApi, string dirname, string filename)
        {
            Directory.CreateDirectory(dirname);

            string json = JsonConvert.SerializeObject(modelApi, Formatting.Indented);
            File.WriteAllText($"{dirname}/{filename}.json", json);
        }

        public static T? ReadFromFile<T>(string dirname, string filename)
        {
            string json = File.ReadAllText($"{dirname}/{filename}.json");
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
