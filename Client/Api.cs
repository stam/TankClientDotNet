using System.Text.Json;
using System.Text;

namespace TankClientDotNet.Client;

public class Tank
{
    public string? Orientation { get; set; }
    public string? Name { get; set; }
    public int Id { get; set; }
    public int Energy { get; set; }
}

public class TankCreationResponse
{
    public Tank? Subscribed { get; set; }

}

public class WorldObject
{
    public List<Tank>? Tanks { get; set; }
}


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

    public async Task<Tank> CreateTank(string tankName)
    {

        var content = new StringContent(JsonSerializer.Serialize(new
        {
            name = tankName
        }), Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/subscribe", content);
        response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Created tank");
        Console.WriteLine(responseBody);

        var creationResponse = JsonSerializer.Deserialize<TankCreationResponse>(responseBody, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (creationResponse?.Subscribed == null)
        {
            throw new Exception("Could not read tank creation response");
        }

        return creationResponse.Subscribed;

    }

    public async Task<WorldObject> GetWorld()
    {
        var response = await client.GetAsync("/world");
        response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();
        // Console.WriteLine(responseBody);

        var world = JsonSerializer.Deserialize<WorldObject>(responseBody, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (world == null)
        {
            throw new Exception("Could not deserialize world");
        }

        return world;
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

    }

    public async Task Fire(int tankId)
    {
        Console.WriteLine("Firing");

        var data = new
        {
            tankid = tankId,
            command = "fire"
        };

        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/tank", content);
    }

}



