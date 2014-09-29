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
        public static readonly int DefaultPort = 8888; // den valgte port
        private static readonly string RootCatalog = "C:\\Windows\\Temp"; //rootcaltalog som angiver stien til filen

       
        public void StartServer()
        {
            while (true)
            {
                TcpListener serverSocket = new TcpListener(DefaultPort); // laver serveren
                serverSocket.Start(); //starter serveren
                TcpClient connectionSocket = serverSocket.AcceptTcpClient(); //sætter sockets til at acceptere clienten
                Stream ns = connectionSocket.GetStream(); //laver en stream
                StreamReader sr = new StreamReader(ns); // laver en streamreader der hedder sr
                StreamWriter sw = new StreamWriter(ns); // laver en streamwriter der hedder sw

                sw.AutoFlush = true; // enable automatic flushing

                try
                {
                    string message = sr.ReadLine(); // læser htlm requesten
                    string[] words = message.Split(' '); //laver et array med ordene fra message så hvert ord 
                    string doh = words[1].Replace("/", "\\"); // bytter alle / ud med \, 
                    string doh2 = RootCatalog + doh; // tager rootcatalog og dog samler dem i en ny string doh2
                    if (doh2 == RootCatalog)
                    {
                        sw.Write("HTTP/1.0 404 not found \r\n"); // sender header til browseren
                        sw.Write("\r\n"); // lineskift så den ved det er body der kommer som det næste
                        sw.Write("Hello world");
                    }
                    if (File.Exists(doh2)) // checker om filen findes
                    {
                        sw.Write("HTTP/1.0 404 not found \r\n"); // sender header til browseren
                        sw.Write("\r\n"); // lineskift så den ved det er body der kommer som det næste
                        string ftext = File.ReadAllText(doh2); // samler filnen i en string der hedder ftext hvis den kan læses
                        sw.Write(ftext); // sender stringen til browseren så den kan læses
                    }
                    else
                    {
                        sw.Write("HTTP/1.0 404 not found \r\n"); // sender header til browseren
                        sw.Write("\r\n"); // lineskift så den ved det er body der kommer som det næste
                        sw.Write("the requested file or homepagde {0} do not exist", doh2); // sender svar tilbage til browseren om at serveren ikke har det de søger
                    }
                }
                finally
                {
                    ns.Close(); // lukker streamen ns ned
                    connectionSocket.Close(); // lukker connectionsocket ned
                    serverSocket.Stop(); // lukker serversocket ned
                }
            }
        }
    }
}

