
namespace TankClientDotNet.Client;

class Bot
{
    private Api _api;
    private string _tankName;
    private readonly int _taskDelay = 2001;

    public Bot(Api api, string tankName)
    {
        _api = api;
        _tankName = tankName;
    }

    public async Task Start()
    {
        var tankId = await InitializeTank();


        while (true)
        {
            await SendCommand(tankId);
            await Task.Delay(_taskDelay);
        }
    }

    private async Task<int> InitializeTank()
    {
        var world = await _api.GetWorld();
        var existingTank = world.Tanks?.Find(tank => tank.Name == _tankName);

        if (existingTank != null)
        {
            Console.WriteLine("Found tank with name " + _tankName);
            return existingTank.Id;
        }

        var newTank = await _api.CreateTank(_tankName);
        Console.WriteLine("Created tank with name " + _tankName);
        return newTank.Id;
    }

    private async Task SendCommand(int tankId)
    {

        List<string> directions = ["north", "east", "south", "west"];
        Random random = new Random();

        if (random.Next(100) > 50)
        {
            int randomIndex = random.Next(directions.Count);
            await _api.Move(tankId, directions[randomIndex]);
        }
        else
        {
            await _api.Fire(tankId);
        }
    }
}