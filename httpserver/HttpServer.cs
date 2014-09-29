using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace httpserver
{
    public class HttpServer
    {
        public static readonly int DefaultPort = 8888;
        public void StartServer()
        {
            while (true)
            {


                TcpListener serverSocket = new TcpListener(DefaultPort);
                serverSocket.Start();


                TcpClient connectionSocket = serverSocket.AcceptTcpClient();
                Stream ns = connectionSocket.GetStream();
                StreamReader sr = new StreamReader(ns);
                
                StreamWriter sw = new StreamWriter(ns);

                sw.AutoFlush = true; // enable automatic flushing
                try
                {


                    string message = sr.ReadLine();
                    
                    sw.Write("HTTP/1.0 200 Ok\r\n");
                    sw.Write("\r\n");
                    sw.Write("you have requested file {0}", message);
                    
                }
                finally
                {
                    ns.Close();
                    connectionSocket.Close();
                    serverSocket.Stop();
                }
            }
        }
    }
}

