using Newtonsoft.Json;
using System.Text;
using System.Web;
using WeatherWeather;


Console.OutputEncoding = Encoding.UTF8;
var apiKey = "de652bea5fc39e262e384811c44b47a7";
var usec = new HttpClient();

while (true)
{
    Console.Write("Введите город, для которого хотите посмотреть прогноз погоды ");
    var city = Console.ReadLine();
    var response = await usec.GetAsync(@$"https://api.openweathermap.org/data/2.5/forecast?q={HttpUtility.UrlEncode(city)}&appid={apiKey}&units=metric&lang=ru");
    if (response.IsSuccessStatusCode)
    {
        var result = await response.Content.ReadAsStringAsync();
        var model = JsonConvert.DeserializeObject<WeatherIRLinfo>(result);
        Console.Clear();
        Console.WriteLine(
            $"Погода в городе: {model.city.name}, {model.city.country} на {DateTime.Now} - {model.list[0].weather[0].description}\n" +
            $"Температура воздуха: {Math.Round(model.list[0].main.temp, 1)}°С\n" +
            $"Ощущается как: {Math.Round(model.list[0].main.feels_like, 1)}°С\n" +
            $"Влажность - {model.list[0].main.humidity}%\n" +
            $"Давление - {Math.Round(model.list[0].main.grnd_level / 1.33322, 2)} мм ртутного столба\n\n" +
            $"Прогноз погоды на 4 дня:\n");


        DateTime somedays;
        int vivod = 0;
        List smdays;
        for (int i = 0; i < 4; i++)
        {
            smdays = model.list[vivod];
            somedays = DateTime.Parse(smdays.dt_txt);

            Console.WriteLine($"{somedays.ToShortDateString()}");
            Console.WriteLine($"{somedays.ToString("dddd")[0].ToString() + somedays.ToString("dddd").Substring(1)}");
            Console.WriteLine($"{Math.Round(smdays.main.temp_min, 1)}, {Math.Round(smdays.main.temp_max, 1)} ");
            Console.WriteLine($"{(smdays.weather[0].description)[0].ToString() + (smdays.weather[0].description).Substring(1)}");
            Console.WriteLine(" ");
            vivod += 8;
        }
    }

    else
    {
        Console.WriteLine("Неправильно указан город, попробуйте еще раз!");
    }
        Console.ReadLine();
        Console.Clear();
    }




