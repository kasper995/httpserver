using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ShutdownClient
{
    class ShutDownClient
    {
        public void _ShutDownClient()
        {
            Console.WriteLine("Press any key to start shutdown process");
            Console.ReadKey();
            TcpClient clientSocket = new TcpClient("localhost", 8888);
            Stream ns = clientSocket.GetStream();
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;
            string message = "POST /close HTTP/1.0";
            Console.WriteLine(message);
            sw.WriteLine(message);
            Console.WriteLine("message send");
            Console.ReadKey();
            ns.Dispose();
            clientSocket.Close();
        }
    }
}
