using System.Text.Json;
using System.Text;
using System.Globalization;

namespace TankClientDotNet.Client;



class Api
{
    private HttpClient client;
    public Api(string baseUrl)
    {
        client = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };
    }

    public async Task<int> CreateTank(string tankName)
    {

        var content = new StringContent(JsonSerializer.Serialize(new
        {
            name = tankName
        }), Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/subscribe", content);

        // if (response.IsSuccessStatusCode)
        // {
        //     Console.WriteLine("Request succeeded.");
        //     var responseBody = await response.Content.ReadAsStringAsync();
        //     Console.WriteLine(responseBody);
        // }
        // else
        // {
        //     Console.WriteLine($"Request failed. Status code: {response.StatusCode}");
        // }

        return 12;

    }

    public async Task GetWorld()
    {
        var response = await client.GetAsync("/world");

    }

    public async Task Move(int tankId, string direction)
    {
        Console.WriteLine("Moving in direction " + direction);

        var data = new
        {
            tankid = tankId,
            command = direction
        };

        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/tank", content);

        // if (response.IsSuccessStatusCode)
        // {
        //     Console.WriteLine("Request succeeded.");
        //     var responseBody = await response.Content.ReadAsStringAsync();
        //     Console.WriteLine(responseBody);
        // }
        // else
        // {
        //     Console.WriteLine($"Request failed. Status code: {response.StatusCode}");
        // }

    }

}



