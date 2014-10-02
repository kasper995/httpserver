using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShutdownClient
{
    class Program
    {
        private static ShutDownClient _client;
        static void Main(string[] args)
        {
            Console.Title = "Shut Down Client";
            _client = new ShutDownClient();
            _client._ShutDownClient();
        }
    }
}
