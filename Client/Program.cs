namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(" https://localhost:7150/WeatherForecast");
            for (int i = 0; i < 3; i++)
            {
                client.GetAsync(client.BaseAddress).Wait();
            }
        }
    }
}