using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;




namespace httpserver
{
    public class HttpServer
    {
        private static readonly string Version = "HTTP/1.0";
        public static readonly int DefaultPort = 8888; // den valgte port
        private static readonly string RootCatalog = "C:\\Windows\\Temp"; //rootcaltalog som angiver stien til filen
        readonly EventLog Mylog = new EventLog();
        TcpListener serverSocket = new TcpListener(IPAddress.Any, DefaultPort); // laver serveren
        public void StartServer()
        {
            Mylog.Source = "Httpserver";
            
            while (true)
            {
                
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
                    string name = words[1].Replace("/", "\\"); // bytter alle / ud med \, 
                    string Path = RootCatalog + name; // tager rootcatalog og dog samler dem i en ny string doh2

                   
                    if (File.Exists(Path)) // checker om filen findes
                    {
                        sw.Write("{0} 200\r\n", Version); // sender header til browseren
                        sw.Write("\r\n"); // lineskift så den ved det er body der kommer som det næste
                        string ftext = File.ReadAllText(Path); // samler filnen i en string der hedder ftext hvis den kan læses
                        sw.Write(ftext); // sender stringen til browseren så den kan læses
                    }

                    if (words[2] != "HTTP/1.1") //checker om http versionen er 1.0
                    {
                        sw.Write("{0} 400\r\n", Version); // sender header til browserensw.Write("\r\n"); // lineskift så den ved det er body der kommer som det næste
                        sw.Write("\r\n"); // lineskift så den ved det er body der kommer som det næste
                        sw.Write("The Http protocol u have chosen are invalid"); // sender svar tilbage til browseren om at protokolen er forkert
                    }
                    if (words[0] != "GET" && words[0] != "POST") // hvis det først ord i din request ikke er post eller get så gør
                    {
                        sw.Write("{0} 400\r\n", Version); // sender header til browseren
                        sw.Write("\r\n"); // lineskift så den ved det er body der kommer som det næste
                        sw.Write("Bad Request did not send a Get or Post request"); // sender svar tilbage til browseren om at protokolen er forkert
                    }
                    if (words[1] == "") // hvis det din request er tom
                    {
                        sw.Write("{0} 204\r\n", Version); // sender header til browseren
                        sw.Write("\r\n"); // lineskift så den ved det er body der kommer som det næste
                        sw.Write("You have not chosen any content"); // sender svar tilbage til browseren om at serveren ikke har det de søger

                    }

                    if (RootCatalog == Path) // checker om din request er tom
                    {
                        sw.Write("{0} 204\r\n", Version); // sender header til browseren
                        sw.Write("\r\n"); // lineskift så den ved det er body der kommer som det næste
                        sw.Write("You have not chosen any content"); // sender svar tilbage til browseren om at serveren ikke har det de søger

                    }
                        
                    else
                    {
                        sw.Write("{0} 404\r\n", Version); // sender header til browseren
                        sw.Write("\r\n"); // lineskift så den ved det er body der kommer som det næste
                        sw.Write("the requested file or homepagde {0} do not exist", Path); // sender svar tilbage til browseren om at serveren ikke har det de søger
                    }
                }

                catch (Exception)
                {
                    throw new Exception();
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

