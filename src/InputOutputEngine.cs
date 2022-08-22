using System.Text.Json;

namespace EngineOverheating
{
    public static class InputOutputEngine
    {
        public static double readTemperature()
        {
            double temperature = 0;
            do
            {
                Console.WriteLine("Введите температуру среды: ");
            } while (!Double.TryParse(Console.ReadLine(), out temperature));
            return temperature;
        }
        public static void writeTime(double time)
        {
            Console.WriteLine("Время до перегрева: " + time + " секунд");
        }
        public static List<Engine> readEnginParamsJSON(string path)
        {
            List<Engine> source = new List<Engine>();
            try
            {
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    source = JsonSerializer.Deserialize<List<Engine>>(json);
                    if (source == null)
                    {
                        throw new Exception();
                    }
                }
            }
            catch
            {
                Console.WriteLine("Некорректные данные JSON");
            }
            return source;
        }
    }
}
