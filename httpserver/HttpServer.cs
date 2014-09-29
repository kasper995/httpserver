using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace httpserver
{
    public class HttpServer
    {
        public static readonly int DefaultPort = 8888;
        private static readonly string RootCatalog = "C:\\Windows\\Temp";


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
                    string[] words = message.Split(' ');
                    string doh = words[1];
                  
                    string doh2 = RootCatalog + doh;


                    if (File.Exists(doh2))
                    {
                        string dd = File.ReadAllText(doh2);
                        sw.Write(dd);
                    }

                    else
                    {
                        sw.Write("HTTP/1.0 200 Ok\r\n");
                        sw.Write("\r\n");
                        sw.Write("the requested file or homepagde do not exist");
                    }
                    
                    //sw.Write("you have requested file {0}", doh2);
                    
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

