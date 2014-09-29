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
            TcpListener serverSocket = new TcpListener(DefaultPort);
            serverSocket.Start();

            TcpClient connectionSocket = serverSocket.AcceptTcpClient();
            

            Stream ns = connectionSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            
            sw.AutoFlush = true; // enable automatic flushing

            string message = sr.ReadLine();
            Console.WriteLine("hello");
            sw.Write("HTTP/1.0 200 Ok\r\n");
            sw.Write("\r\n");
            sw.Write("hello");
            ns.Close();
            connectionSocket.Close();
            serverSocket.Stop();

        }
    }
}
