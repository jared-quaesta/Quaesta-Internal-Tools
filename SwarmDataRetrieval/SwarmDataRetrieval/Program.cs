using QIRestfulSwarm;
using System;
using System.Threading.Tasks;

namespace SwarmDataRetrieval
{
    class Program
    {
        static void Main(string[] args)
        {
            RestfulSwarm rs = new RestfulSwarm();
            bool login = Task.Run(async () =>
            {
                return await rs.ValidateLoginAsync("shamann@quaestainstruments.com", "quaesta2021");
            }).GetAwaiter().GetResult();

            if (!login)
            {
                Console.WriteLine("Incorrect Login.");
                return;
            }

            string devs = Task.Run(async () =>
            {
                return await rs.GetDevices();
            }).GetAwaiter().GetResult();

            Console.WriteLine(devs);

            rs.LogoutAsync();
            
            Console.WriteLine("Logged Out");
        }
    }
}
