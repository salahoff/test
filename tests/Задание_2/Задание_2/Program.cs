using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace Задание_2
{
    public class Info
    {
        public float Temp { get; set; }
        public float Humidity { get; set; } 
        public int Sunset { get; set; }
        public int Sunrise { get; set; }
    }

    public class WeatherResponse
    {
       public Info Main { get; set; }
       public Info Sys { get; set; }
       public string Name { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите название города:");
            string city = Console.ReadLine();

            string url = "http://api.openweathermap.org/data/2.5/weather?q=" + city + "&units=metric&appid=c9d9e13aa3174bb31933dbb96d7bb525";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string response;

            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);

            void ConsoleOutput()
            {

                Console.WriteLine("Температура: {0}°C", weatherResponse.Main.Temp);
                Console.WriteLine("Влажность: {0}%", weatherResponse.Main.Humidity);

                TimeSpan set = TimeSpan.FromMilliseconds(weatherResponse.Sys.Sunset);
                TimeSpan rise = TimeSpan.FromMilliseconds(weatherResponse.Sys.Sunrise);

                Console.WriteLine("Закат: {0}:{1}", set.Hours, set.Minutes);
                Console.WriteLine("Восход: {0}:{1}", rise.Hours - 12, rise.Minutes);
            }

            ConsoleOutput();

            DateTime ThToday = DateTime.Now;
            string ThData = ThToday.ToString("dd.MM.yyyy");
 
            FileStream fs = new FileStream(ThData + ".txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            Console.SetOut(sw);
            ConsoleOutput();
            TextWriter tmp = Console.Out;
            Console.SetOut(tmp);
            sw.Close();

            Console.ReadKey();
        }

    }

}