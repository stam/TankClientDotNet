
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
        //     const world = await this.api.getWorld();
        //     const existingTank = world.tanks.find((tank) => tank.name === TANK_NAME);

        //     if (existingTank) {
        //       console.log(`Found tank with name ${TANK_NAME}`);
        //       return existingTank.id;
        //     }

        //     const newTank = await this.api.createTank(TANK_NAME);
        //     console.log(`Created tank with name "${TANK_NAME}"`);
        //     return newTank.id;

        await _api.GetWorld();
        return 1;
    }

    private async Task SendCommand(int tankId)
    {

        List<string> directions = ["north", "east", "south", "west"];

        Random random = new Random();

        if (true)
        {
            int randomIndex = random.Next(directions.Count);
            await _api.Move(tankId, directions[randomIndex]);
        }
        else
        {
            // await _api.Fire(tankId);

        }

        //     const directions = ["north", "east", "south", "west"];

        //     console.log();

        //     if (Math.random() > 0.5) {
        //       const randomDirection =
        //         directions[Math.floor(Math.random() * directions.length)];
        //       this.api.move(this.tankId, randomDirection);
        //     } else {
        //       this.api.fire(this.tankId);
        //     }


    }
}