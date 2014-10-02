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
          //waits for the user input
          Console.ReadKey();
          //creates a clientSocket  that will be used in establishig a connection to the server
          //with servers address i.e ip and port number
          TcpClient clientSocket = new TcpClient("localhost", 8888);

            
          //Gets the network stream from the connection and use it communicate with the server
        
              Stream ns = clientSocket.GetStream();
              StreamWriter sw = new StreamWriter(ns);
              sw.AutoFlush = true;
              //reads input from user and sends it to the server
          Console.ReadKey();
          string message = "POST /close HTTP/1.0";
          Console.WriteLine(message);
              sw.WriteLine(message);
          Console.WriteLine("message send");
          Console.ReadKey();
              ns.Dispose();
          
          

            //ns.Close();
            clientSocket.Close();

        }
    }
}
