using System.Text.Json;
using System.Text;

using TankClientDotNet.Client;

namespace TankClientDotNet
{
    public class Program
    {


        public static async Task Main()
        {
            var SERVER_URL = "http://localhost:3000";
            var TANK_NAME = "my_dotnet_tank";

            var api = new Api(SERVER_URL);
            var bot = new Bot(api, TANK_NAME);
            await bot.Start();


        }
    }
}

