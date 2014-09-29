using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace httpserver
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpServer h1 = new HttpServer(); //laver den nye server
            Parallel.Invoke(h1.StartServer); // starter forskellige tråde
        }
    }
}
